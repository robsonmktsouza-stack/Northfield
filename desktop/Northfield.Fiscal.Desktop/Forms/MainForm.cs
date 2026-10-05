using System.Diagnostics;
using System.Drawing.Printing;
using System.Text;
using Northfield.Fiscal.Desktop.Models;
using Northfield.Fiscal.Desktop.Services;
using Northfield.Fiscal.Desktop.Services.Engine;

namespace Northfield.Fiscal.Desktop.Forms;

[System.ComponentModel.DesignerCategory("Form")]
public sealed partial class MainForm : Form
{
    private readonly List<FiscalDocument> _documents = [];
    private readonly ImportService _importService = new();
    private readonly SessionService _sessionService = new();
    private readonly CsvExportService _csvExportService = new();
    private readonly IEngineGateway _engine = new ManualEngineGateway();
    private Guid? _selectedDocumentId;
    private string? _currentSessionPath;
    private bool _loadingForm;
    private string _currentCompanyUf = string.Empty;
    private bool _currentCompanyIsActive = true;
    private System.Windows.Forms.Timer? _clockTimer;

    public MainForm()
    {
        InitializeComponent();
        InitializeHomeWorkspace();
        ApplyRuntimeTheme();
        HookEvents();
        LoadLastSession();
        RefreshAll();
    }

    private void HookEvents()
    {
        _dtCompetence.ValueChanged += (_, _) => CompanyFormChanged();
        _txtCnpj.TextChanged += (_, _) => CompanyFormChanged();
        _txtCorporateName.TextChanged += (_, _) => CompanyFormChanged();
        _txtMunicipality.TextChanged += (_, _) => CompanyFormChanged();
        _numRbt12.ValueChanged += (_, _) => CompanyFormChanged();
        _numPayroll.ValueChanged += (_, _) => CompanyFormChanged();
        _chkConsiderIss.CheckedChanged += (_, _) => CompanyFormChanged();
        _chkGroup.CheckedChanged += (_, _) => { CompanyFormChanged(); RefreshSegregation(); };
        _txtSearch.TextChanged += (_, _) => RefreshDocumentGrids();
        _tabs.SelectedIndexChanged += (_, _) => RefreshAll();
        _tabs.DrawItem += DrawMainTab;

        _gridApuracao.CellDoubleClick += (_, e) => { if (e.RowIndex >= 0) EditSelectedDocument(_gridApuracao); };
        _gridDocuments.CellDoubleClick += (_, e) => { if (e.RowIndex >= 0) EditSelectedDocument(_gridDocuments); };
        _gridApuracao.SelectionChanged += (_, _) => GridSelectionChanged(_gridApuracao);
        _gridDocuments.SelectionChanged += (_, _) => GridSelectionChanged(_gridDocuments);

        _btnImportDocs.Click += (_, _) => ImportFiles();
        _btnEditDoc.Click += (_, _) => EditSelectedDocument(_gridDocuments);
        _btnRemoveDoc.Click += (_, _) => RemoveSelectedDocument();

        _miNovo.Click += (_, _) => NewSession();
        _miAbrir.Click += (_, _) => OpenSession();
        _miSalvar.Click += (_, _) => SaveSession(false);
        _miSalvarComo.Click += (_, _) => SaveSession(true);
        _miImportar.Click += (_, _) => ImportFiles();
        _miImportarMovimentos.Click += (_, _) => ImportFiles();
        _miExportar.Click += (_, _) => ExportCsv();
        _miSair.Click += (_, _) => Close();
        _miDadosEmpresa.Click += (_, _) => ShowCompanies();
        _miEditarDocumento.Click += (_, _) => EditSelectedDocument();
        _miRemoverDocumento.Click += (_, _) => RemoveSelectedDocument();
        _miProcessar.Click += async (_, _) => await ProcessCompetenceAsync();
        _miRecalcular.Click += (_, _) => RefreshAll();
        _miSegregacao.Click += (_, _) => ShowRoutineTab(_tabSegregacao);
        _miMemoria.Click += (_, _) => ShowRoutineTab(_tabMemoria);
        _miPgdas.Click += (_, _) => ShowRoutineTab(_tabPgdas);
        _miExportarCsv.Click += (_, _) => ExportCsv();
        _miImprimir.Click += (_, _) => PrintSummary();
        _miLimpar.Click += (_, _) => ClearDocuments();
        _miPastaLocal.Click += (_, _) => OpenLocalDataFolder();
        _miSobre.Click += (_, _) => ShowAbout();

        _tbNovo.Click += (_, _) => NewSession();
        _tbAbrir.Click += (_, _) => OpenSession();
        _tbImportar.Click += (_, _) => ImportFiles();
        _tbProcessar.Click += async (_, _) => await ProcessCompetenceAsync();
        _tbApuracao.Click += (_, _) => ShowRoutineTab(_tabApuracao);
        _tbExportar.Click += (_, _) => ExportCsv();
        _tbImprimir.Click += (_, _) => PrintSummary();

        FormClosing += (_, _) => SaveLastSessionSilently();
        FormClosed += (_, _) =>
        {
            _clockTimer?.Stop();
            _clockTimer?.Dispose();
            _clockTimer = null;
        };
        DragEnter += MainForm_DragEnter;
        DragDrop += MainForm_DragDrop;

        _clockTimer = new System.Windows.Forms.Timer
        {
            Interval = 30000
        };
        _clockTimer.Tick += (_, _) => _stClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        _clockTimer.Start();
    }

    private void CompanyFormChanged()
    {
        if (_loadingForm) return;
        UpdateFactorR();
        RefreshSummary();
        RefreshStatus();
        RefreshHomeWorkspace();
    }

    private CompanyContext ReadCompanyFromForm() => new()
    {
        Competence = new DateTime(_dtCompetence.Value.Year, _dtCompetence.Value.Month, 1),
        Cnpj = _txtCnpj.Text.Trim(),
        CorporateName = _txtCorporateName.Text.Trim(),
        TaxRegime = _cboTaxRegime.SelectedItem?.ToString() ?? "Simples Nacional",
        Municipality = _txtMunicipality.Text.Trim(),
        Uf = _currentCompanyUf,
        IsActive = _currentCompanyIsActive,
        Rbt12 = _numRbt12.Value,
        Payroll12m = _numPayroll.Value,
        ConsiderIssWithheld = _chkConsiderIss.Checked,
        GroupBySegregation = _chkGroup.Checked
    };

    private void LoadCompanyToForm(CompanyContext company)
    {
        _loadingForm = true;
        try
        {
            _dtCompetence.Value = company.Competence.Year is >= 2000 and <= 2100 ? company.Competence : DateTime.Today;
            _txtCnpj.Text = company.Cnpj;
            _txtCorporateName.Text = company.CorporateName;
            _cboTaxRegime.SelectedItem = "Simples Nacional";
            _txtMunicipality.Text = company.Municipality;
            _currentCompanyUf = company.Uf;
            _currentCompanyIsActive = company.IsActive;
            _numRbt12.Value = ClampNumeric(_numRbt12, company.Rbt12);
            _numPayroll.Value = ClampNumeric(_numPayroll, company.Payroll12m);
            _chkConsiderIss.Checked = company.ConsiderIssWithheld;
            _chkGroup.Checked = company.GroupBySegregation;
            UpdateFactorR();
        }
        finally
        {
            _loadingForm = false;
        }
    }

    private static decimal ClampNumeric(NumericUpDown control, decimal value) => Math.Min(control.Maximum, Math.Max(control.Minimum, value));

    private void UpdateFactorR()
    {
        var factor = _numRbt12.Value <= 0m ? 0m : Math.Round((_numPayroll.Value / _numRbt12.Value) * 100m, 2);
        _txtFactorR.Text = factor.ToString("N2") + " %";
    }

    private void RefreshAll()
    {
        RefreshDocumentGrids();
        RefreshSummary();
        RefreshSegregation();
        RefreshPgdas();
        RefreshMemory();
        RefreshFullMemory();
        RefreshStatus();
        RefreshHomeWorkspace();
    }

    private void RefreshDocumentGrids()
    {
        FillDocumentGrid(_gridApuracao, _documents);

        var query = _txtSearch.Text.Trim();
        IEnumerable<FiscalDocument> filtered = _documents;
        if (query.Length > 0)
        {
            filtered = filtered.Where(d =>
                d.DisplayDocument.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                d.RecipientName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                d.RecipientTaxId.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                d.ServiceCode.Contains(query, StringComparison.OrdinalIgnoreCase));
        }
        FillDocumentGrid(_gridDocuments, filtered);
    }

    private void FillDocumentGrid(DataGridView grid, IEnumerable<FiscalDocument> documents)
    {
        var currentId = _selectedDocumentId;
        grid.SuspendLayout();
        try
        {
            grid.Rows.Clear();
            foreach (var d in documents)
            {
                var index = grid.Rows.Add(
                    currentId == d.Id,
                    d.DisplayDocument,
                    d.IssueDate?.ToString("dd/MM/yyyy") ?? string.Empty,
                    d.RecipientName,
                    d.RecipientTaxId,
                    d.Municipality,
                    d.ServiceCode,
                    d.ServiceValue,
                    d.IssBase,
                    d.IssWithheldValue,
                    d.Annex,
                    d.Segregation,
                    d.Status);
                grid.Rows[index].Tag = d;
                if (currentId == d.Id)
                    grid.Rows[index].Selected = true;
            }
        }
        finally
        {
            grid.ResumeLayout();
        }
    }

    private void GridSelectionChanged(DataGridView grid)
    {
        if (grid.SelectedRows.Count == 0) return;
        if (grid.SelectedRows[0].Tag is not FiscalDocument doc) return;
        _selectedDocumentId = doc.Id;
        RefreshMemory(doc);
        RefreshStatus();
    }

    private void RefreshSummary()
    {
        var company = ReadCompanyFromForm();
        var total = AnalysisService.TotalRevenue(_documents);
        var retained = _documents.Where(d => d.IssWithheld).Sum(d => d.ServiceValue);
        var notRetained = _documents.Where(d => !d.IssWithheld).Sum(d => d.ServiceValue);
        var pending = AnalysisService.TotalPending(_documents);
        var pendingCount = _documents.Count(d => string.IsNullOrWhiteSpace(d.Segregation));

        _lblSummaryRevenue.Text = total.ToString("C2");
        _lblSummaryNoRetention.Text = notRetained.ToString("C2");
        _lblSummaryRetention.Text = retained.ToString("C2");
        _lblSummaryPending.Text = pending.ToString("C2");
        _lblSummaryFactorR.Text = company.FactorRPercent.ToString("N2") + " %";
        _lblSummaryTotal.Text = total.ToString("C2");
        _lblAlerts.Text = pendingCount == 0 ? "Sem pendências" : $"{pendingCount} pendência(s)";
        _lblAlerts.ForeColor = pendingCount == 0 ? Theme.Success : Theme.Warning;
    }

    private void RefreshSegregation()
    {
        _gridSegregation.Rows.Clear();
        foreach (var row in AnalysisService.BuildSegregation(_documents))
            _gridSegregation.Rows.Add(row.Annex, row.Segregation, row.Documents, row.Amount);
    }

    private void RefreshPgdas()
    {
        _gridPgdas.Rows.Clear();
        foreach (var group in _documents.GroupBy(d => new
        {
            Annex = string.IsNullOrWhiteSpace(d.Annex) ? "Pendente" : d.Annex,
            Segregation = string.IsNullOrWhiteSpace(d.Segregation) ? "Sem classificação" : d.Segregation
        }).OrderBy(g => g.Key.Annex).ThenBy(g => g.Key.Segregation))
        {
            var allRetained = group.All(d => d.IssWithheld);
            var noneRetained = group.All(d => !d.IssWithheld);
            var iss = allRetained ? "Retido" : noneRetained ? "Sem retenção" : "Misto";
            _gridPgdas.Rows.Add("Mercado interno", group.Key.Annex, group.Key.Segregation, iss, group.Sum(d => d.ServiceValue));
        }
    }

    private void RefreshMemory()
    {
        var doc = GetSelectedDocument();
        RefreshMemory(doc);
    }

    private void RefreshMemory(FiscalDocument? doc)
    {
        if (doc is null)
        {
            _lblMemDocument.Text = "-";
            _lblMemIssue.Text = "-";
            _lblMemRecipient.Text = "-";
            _lblMemTaxId.Text = "-";
            _lblMemMunicipality.Text = "-";
            _lblMemValue.Text = "-";
            _lblMemIssBase.Text = "-";
            _lblMemIss.Text = "-";
            _lstMemory.Items.Clear();
            return;
        }

        _lblMemDocument.Text = doc.DisplayDocument;
        _lblMemIssue.Text = doc.IssueDate?.ToString("dd/MM/yyyy") ?? "-";
        _lblMemRecipient.Text = doc.RecipientName;
        _lblMemTaxId.Text = doc.RecipientTaxId;
        _lblMemMunicipality.Text = doc.Municipality;
        _lblMemValue.Text = doc.ServiceValue.ToString("C2");
        _lblMemIssBase.Text = doc.IssBase.ToString("C2");
        _lblMemIss.Text = doc.IssWithheld ? $"Sim ({doc.IssWithheldValue:C2})" : "Não";

        _lstMemory.Items.Clear();
        foreach (var item in doc.Memory)
            _lstMemory.Items.Add("✓  " + item);
        if (doc.Memory.Count == 0)
            _lstMemory.Items.Add("•  Sem memória registrada para este documento.");
    }

    private void RefreshFullMemory()
    {
        var sb = new StringBuilder();
        var company = ReadCompanyFromForm();
        sb.AppendLine("NORTHFIELD FISCAL - MEMÓRIA DE CÁLCULO");
        sb.AppendLine(new string('=', 72));
        sb.AppendLine($"Competência: {company.Competence:MM/yyyy}");
        sb.AppendLine($"Empresa: {company.CorporateName}");
        sb.AppendLine($"CNPJ: {company.Cnpj}");
        sb.AppendLine($"RBT12: {company.Rbt12:C2}");
        sb.AppendLine($"Folha 12m: {company.Payroll12m:C2}");
        sb.AppendLine($"Fator R: {company.FactorRPercent:N2}%");
        sb.AppendLine();

        foreach (var doc in _documents)
        {
            sb.AppendLine($"{doc.DisplayDocument} | {doc.ServiceValue:C2} | Anexo: {(doc.Annex.Length == 0 ? "Pendente" : doc.Annex)}");
            sb.AppendLine($"Tomador: {doc.RecipientName} | Serviço: {doc.ServiceCode} | ISS retido: {(doc.IssWithheld ? "Sim" : "Não")}");
            sb.AppendLine($"Segregação: {(doc.Segregation.Length == 0 ? "Pendente" : doc.Segregation)}");
            foreach (var memory in doc.Memory) sb.AppendLine($"  - {memory}");
            sb.AppendLine(new string('-', 72));
        }
        _txtFullMemory.Text = sb.ToString();
    }

    private void RefreshStatus()
    {
        var company = ReadCompanyFromForm();
        _stRecords.Text = $"Registros: {_documents.Count}";
        _stSelected.Text = $"Selecionado: {(_selectedDocumentId.HasValue ? 1 : 0)}";
        _stCompany.Text = $"Empresa: {(company.CorporateName.Length == 0 ? "-" : company.CorporateName)}";
        _stCompetence.Text = $"Competência: {company.Competence:MM/yyyy}";
        _stEngine.Text = _engine.IsAvailable ? $"Análise: {_engine.Name}" : "Classificação: manual";
        _stEngine.ForeColor = _engine.IsAvailable ? Theme.Success : Theme.Warning;
    }

    private FiscalDocument? GetSelectedDocument()
    {
        if (_selectedDocumentId is null) return null;
        return _documents.FirstOrDefault(d => d.Id == _selectedDocumentId.Value);
    }

    private void ImportFiles()
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Importar documentos fiscais",
            Filter = "XML e ZIP (*.xml;*.zip)|*.xml;*.zip|XML (*.xml)|*.xml|ZIP (*.zip)|*.zip",
            Multiselect = true,
            CheckFileExists = true
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        ImportPaths(dialog.FileNames);
    }

    private void ImportPaths(IEnumerable<string> paths)
    {
        var imported = _importService.Import(paths, out var errors);
        var added = 0;
        foreach (var doc in imported)
        {
            var duplicate = _documents.Any(d =>
                string.Equals(d.DocumentType, doc.DocumentType, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(d.Number, doc.Number, StringComparison.OrdinalIgnoreCase) &&
                d.IssueDate?.Date == doc.IssueDate?.Date &&
                d.ServiceValue == doc.ServiceValue &&
                string.Equals(d.RecipientTaxId, doc.RecipientTaxId, StringComparison.OrdinalIgnoreCase));
            if (duplicate) continue;
            _documents.Add(doc);
            added++;
        }

        if (_selectedDocumentId is null && _documents.Count > 0)
            _selectedDocumentId = _documents[0].Id;

        RefreshAll();
        SaveLastSessionSilently();

        var message = $"{added} documento(s) importado(s).";
        if (errors.Count > 0)
            message += $"\n\nOcorreram {errors.Count} aviso(s):\n" + string.Join("\n", errors.Take(8));
        MessageBox.Show(this, message, "Importação", MessageBoxButtons.OK, errors.Count == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
    }

    private async Task ProcessCompetenceAsync()
    {
        if (_documents.Count == 0)
        {
            MessageBox.Show(this, "Importe ao menos um XML antes de processar.", "Northfield Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        UseWaitCursor = true;
        try
        {
            var result = await _engine.AnalyzeAsync(ReadCompanyFromForm(), _documents);
            foreach (var decision in result.Decisions)
            {
                var doc = _documents.FirstOrDefault(d => d.Id == decision.DocumentId);
                if (doc is null) continue;
                if (!string.IsNullOrWhiteSpace(decision.Annex)) doc.Annex = decision.Annex;
                if (!string.IsNullOrWhiteSpace(decision.Segregation)) doc.Segregation = decision.Segregation;
                doc.Status = decision.Status;
                if (decision.Memory.Count > 0)
                {
                    var importMemory = doc.Memory.Where(m => m.StartsWith("Documento importado", StringComparison.OrdinalIgnoreCase)).ToList();
                    doc.Memory = importMemory.Concat(decision.Memory).Distinct().ToList();
                }
            }
            RefreshAll();
            SaveLastSessionSilently();
            MessageBox.Show(this, result.Message, result.EngineName, MessageBoxButtons.OK, result.EngineAvailable ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }
        finally
        {
            UseWaitCursor = false;
        }
    }

    private void EditSelectedDocument() => EditSelectedDocument(_tabs.SelectedTab == _tabDocumentos ? _gridDocuments : _gridApuracao);

    private void EditSelectedDocument(DataGridView grid)
    {
        FiscalDocument? doc = null;
        if (grid.SelectedRows.Count > 0) doc = grid.SelectedRows[0].Tag as FiscalDocument;
        doc ??= GetSelectedDocument();
        if (doc is null)
        {
            MessageBox.Show(this, "Selecione um documento.", "Northfield Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var form = new DocumentEditForm(doc);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            _selectedDocumentId = doc.Id;
            RefreshAll();
            SaveLastSessionSilently();
        }
    }

    private void RemoveSelectedDocument()
    {
        var doc = GetSelectedDocument();
        if (doc is null) return;
        if (MessageBox.Show(this, $"Remover {doc.DisplayDocument}?", "Remover documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        _documents.Remove(doc);
        _selectedDocumentId = _documents.FirstOrDefault()?.Id;
        RefreshAll();
        SaveLastSessionSilently();
    }

    private void ClearDocuments()
    {
        if (_documents.Count == 0) return;
        if (MessageBox.Show(this, "Remover todos os documentos da competência?", "Limpar documentos", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        _documents.Clear();
        _selectedDocumentId = null;
        RefreshAll();
        SaveLastSessionSilently();
    }

    private void NewSession()
    {
        if (_documents.Count > 0 && MessageBox.Show(this, "Iniciar uma nova sessão? Os documentos atuais serão removidos da tela.", "Nova sessão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        _documents.Clear();
        _selectedDocumentId = null;
        _currentSessionPath = null;
        LoadCompanyToForm(new CompanyContext());
        _txtSearch.Clear();
        RefreshAll();
        ShowRoutineTab(_tabApuracao);
    }

    private SessionData CreateSessionData() => new()
    {
        Company = ReadCompanyFromForm(),
        Documents = _documents.ToList()
    };

    private void OpenSession()
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Abrir sessão Northfield",
            Filter = "Sessão Northfield (*.northfield.json)|*.northfield.json|JSON (*.json)|*.json",
            CheckFileExists = true
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            var data = _sessionService.Load(dialog.FileName);
            LoadSessionData(data);
            _currentSessionPath = dialog.FileName;
            ShowRoutineTab(_tabApuracao);
            RefreshAll();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Erro ao abrir sessão", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SaveSession(bool saveAs)
    {
        var path = _currentSessionPath;
        if (saveAs || string.IsNullOrWhiteSpace(path))
        {
            using var dialog = new SaveFileDialog
            {
                Title = "Guardar sessão Northfield",
                Filter = "Sessão Northfield (*.northfield.json)|*.northfield.json",
                FileName = $"apuracao-{_dtCompetence.Value:yyyy-MM}.northfield.json"
            };
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            path = dialog.FileName;
        }

        try
        {
            _sessionService.Save(CreateSessionData(), path!);
            _currentSessionPath = path;
            RefreshHomeWorkspace();
            MessageBox.Show(this, "Sessão guardada.", "Northfield Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Erro ao guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadSessionData(SessionData data)
    {
        LoadCompanyToForm(data.Company);
        _documents.Clear();
        _documents.AddRange(data.Documents ?? []);
        _selectedDocumentId = _documents.FirstOrDefault()?.Id;
        RefreshAll();
    }

    private void LoadLastSession()
    {
        var data = _sessionService.TryLoadLast();
        if (data is null) return;
        LoadSessionData(data);
    }

    private void SaveLastSessionSilently()
    {
        try { _sessionService.SaveLast(CreateSessionData()); }
        catch { }
    }

    private void ExportCsv()
    {
        if (_documents.Count == 0)
        {
            MessageBox.Show(this, "Não há documentos para exportar.", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        using var dialog = new SaveFileDialog
        {
            Title = "Exportar documentos",
            Filter = "CSV (*.csv)|*.csv",
            FileName = $"northfield-{_dtCompetence.Value:yyyy-MM}.csv"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            _csvExportService.Export(_documents, dialog.FileName);
            MessageBox.Show(this, "CSV exportado com sucesso.", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Erro ao exportar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void PrintSummary()
    {
        var company = ReadCompanyFromForm();
        var rows = AnalysisService.BuildSegregation(_documents);
        var print = new PrintDocument { DocumentName = $"Northfield - Apuração {company.Competence:MM/yyyy}" };
        print.PrintPage += (_, e) =>
        {
            var g = e.Graphics;
            if (g is null)
                return;

            var y = 50f;
            using var titleFont = new Font("Segoe UI", 16, FontStyle.Bold);
            using var headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
            using var font = new Font("Segoe UI", 9);
            g.DrawString("Northfield Fiscal - Resumo da apuração", titleFont, Brushes.Black, 55, y);
            y += 38;
            g.DrawString($"Competência: {company.Competence:MM/yyyy}", font, Brushes.Black, 55, y); y += 20;
            g.DrawString($"Empresa: {company.CorporateName}  CNPJ: {company.Cnpj}", font, Brushes.Black, 55, y); y += 20;
            g.DrawString($"RBT12: {company.Rbt12:C2}  Folha 12m: {company.Payroll12m:C2}  Fator R: {company.FactorRPercent:N2}%", font, Brushes.Black, 55, y); y += 34;
            g.DrawString("Segregação", headerFont, Brushes.Black, 55, y); y += 24;
            foreach (var row in rows)
            {
                g.DrawString($"Anexo {row.Annex} | {row.Segregation} | {row.Documents} doc(s)", font, Brushes.Black, 65, y);
                g.DrawString(row.Amount.ToString("C2"), font, Brushes.Black, 620, y);
                y += 20;
                if (y > e.MarginBounds.Bottom - 30) { e.HasMorePages = true; return; }
            }
            y += 18;
            g.DrawString($"Receita total: {AnalysisService.TotalRevenue(_documents):C2}", headerFont, Brushes.Black, 55, y);
            e.HasMorePages = false;
        };

        using var preview = new PrintPreviewDialog
        {
            Document = print,
            Width = 1100,
            Height = 760,
            StartPosition = FormStartPosition.CenterParent
        };
        preview.ShowDialog(this);
    }

    private void OpenLocalDataFolder()
    {
        Directory.CreateDirectory(_sessionService.AppDataDirectory);
        Process.Start(new ProcessStartInfo { FileName = _sessionService.AppDataDirectory, UseShellExecute = true });
    }

    private void FocusCompanyData()
    {
        ShowRoutineTab(_tabApuracao);
        _txtCnpj.Focus();
    }

    private void ShowAbout()
    {
        MessageBox.Show(this,
            "Northfield Fiscal - Simples Nacional\n\nPrograma para importar, conferir e segregar receitas do Simples Nacional.",
            "Sobre o Northfield Fiscal",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void MainForm_DragEnter(object? sender, DragEventArgs e)
    {
        if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            e.Effect = DragDropEffects.Copy;
    }

    private void MainForm_DragDrop(object? sender, DragEventArgs e)
    {
        if (e.Data?.GetData(DataFormats.FileDrop) is string[] files)
            ImportPaths(files.Where(f => f.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)));
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == (Keys.Control | Keys.I)) { ImportFiles(); return true; }
        if (keyData == (Keys.Control | Keys.S)) { SaveSession(false); return true; }
        if (keyData == Keys.F5) { _ = ProcessCompetenceAsync(); return true; }
        if (keyData == Keys.Delete && (_tabs.SelectedTab == _tabDocumentos || _tabs.SelectedTab == _tabApuracao)) { RemoveSelectedDocument(); return true; }
        return base.ProcessCmdKey(ref msg, keyData);
    }
}
