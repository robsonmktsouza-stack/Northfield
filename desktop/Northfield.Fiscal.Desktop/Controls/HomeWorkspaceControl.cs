using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Controls;

[System.ComponentModel.DesignerCategory("Code")]
public sealed partial class HomeWorkspaceControl : UserControl
{
    public event EventHandler? NewAssessmentRequested;
    public event EventHandler? ImportRequested;
    public event EventHandler? OpenAssessmentRequested;
    public event EventHandler? SegregationRequested;
    public event EventHandler? PgdasRequested;
    public event EventHandler? ReviewRequested;
    public event EventHandler? CompanyRequested;
    public event EventHandler<string>? RecentAssessmentOpenRequested;

    public HomeWorkspaceControl()
    {
        InitializeComponent();
        ApplyTheme();
        HookEvents();
    }

    public void UpdateData(
        CompanyContext company,
        IReadOnlyList<FiscalDocument> documents,
        IReadOnlyList<RecentSessionInfo> recentSessions)
    {
        var revenue = documents.Sum(d => d.ServiceValue);
        var classified = documents.Count(d => !string.IsNullOrWhiteSpace(d.Segregation));
        var pending = documents.Count - classified;

        _lblCompanyName.Text = string.IsNullOrWhiteSpace(company.CorporateName) ? "Nenhuma empresa informada" : company.CorporateName;
        _lblCompanyCnpj.Text = string.IsNullOrWhiteSpace(company.Cnpj) ? "CNPJ: -" : $"CNPJ: {company.Cnpj}";
        _lblCompetence.Text = $"Competência: {company.Competence:MM/yyyy}";

        _lblRevenueValue.Text = revenue.ToString("C2");
        _lblRbt12Value.Text = company.Rbt12.ToString("C2");
        _lblFactorRValue.Text = company.Rbt12 <= 0m ? "-" : company.FactorRPercent.ToString("N2") + "%";
        _lblDocumentsValue.Text = documents.Count.ToString();
        _lblClassifiedValue.Text = classified.ToString();
        _lblPendingValue.Text = pending.ToString();

        FillPending(company, documents, pending);
        FillRecent(recentSessions);
        FillNotices(company, documents, pending);
    }

    private void FillPending(CompanyContext company, IReadOnlyList<FiscalDocument> documents, int pending)
    {
        _lstPending.Items.Clear();

        if (string.IsNullOrWhiteSpace(company.CorporateName))
            _lstPending.Items.Add("Empresa não informada.");

        if (string.IsNullOrWhiteSpace(company.Cnpj))
            _lstPending.Items.Add("CNPJ não informado.");

        if (documents.Count == 0)
            _lstPending.Items.Add("Nenhum documento importado.");
        else if (pending > 0)
            _lstPending.Items.Add($"{pending} documento(s) aguardando classificação.");

        if (company.Rbt12 <= 0m)
            _lstPending.Items.Add("RBT12 não informado.");

        var withoutServiceCode = documents.Count(d => string.IsNullOrWhiteSpace(d.ServiceCode));
        if (withoutServiceCode > 0)
            _lstPending.Items.Add($"{withoutServiceCode} documento(s) sem código de serviço.");

        if (_lstPending.Items.Count == 0)
            _lstPending.Items.Add("Nenhuma pendência para a competência.");
    }

    private void FillRecent(IReadOnlyList<RecentSessionInfo> recentSessions)
    {
        _gridRecent.Rows.Clear();

        foreach (var item in recentSessions)
        {
            var index = _gridRecent.Rows.Add(
                item.Competence.ToString("MM/yyyy"),
                item.CompanyName,
                item.Revenue,
                item.Status,
                item.SavedAt.ToString("dd/MM/yyyy HH:mm"));

            _gridRecent.Rows[index].Tag = item;
        }
    }

    private void FillNotices(CompanyContext company, IReadOnlyList<FiscalDocument> documents, int pending)
    {
        _lstNotices.Items.Clear();

        _lstNotices.Items.Add($"Competência {company.Competence:MM/yyyy} aberta para conferência.");

        if (documents.Count > 0)
            _lstNotices.Items.Add($"{documents.Count} documento(s) carregado(s), totalizando {documents.Sum(d => d.ServiceValue):C2}.");

        if (pending > 0)
            _lstNotices.Items.Add("Existem documentos que ainda precisam ser revisados.");
        else if (documents.Count > 0)
            _lstNotices.Items.Add("Todos os documentos carregados estão classificados.");
    }

    private void HookEvents()
    {
        _btnNew.Click += (_, _) => NewAssessmentRequested?.Invoke(this, EventArgs.Empty);
        _btnImport.Click += (_, _) => ImportRequested?.Invoke(this, EventArgs.Empty);
        _btnOpen.Click += (_, _) => OpenAssessmentRequested?.Invoke(this, EventArgs.Empty);
        _btnSegregation.Click += (_, _) => SegregationRequested?.Invoke(this, EventArgs.Empty);
        _btnPgdas.Click += (_, _) => PgdasRequested?.Invoke(this, EventArgs.Empty);
        _btnReview.Click += (_, _) => ReviewRequested?.Invoke(this, EventArgs.Empty);
        _btnCompany.Click += (_, _) => CompanyRequested?.Invoke(this, EventArgs.Empty);

        _gridRecent.CellDoubleClick += (_, e) =>
        {
            if (e.RowIndex < 0)
                return;

            if (_gridRecent.Rows[e.RowIndex].Tag is RecentSessionInfo item &&
                !string.IsNullOrWhiteSpace(item.FilePath))
            {
                RecentAssessmentOpenRequested?.Invoke(this, item.FilePath);
            }
        };
    }

    private void ApplyTheme()
    {
        BackColor = Theme.AppBack;
        Font = Theme.UiFont(8.5F);

        StyleGroup(_grpCompany);
        StyleGroup(_grpActions);
        StyleGroup(_grpRecent);
        StyleGroup(_grpPending);
        StyleGroup(_grpSummary);
        StyleGroup(_grpNotices);

        Theme.StyleButton(_btnNew, primary: true);
        Theme.StyleButton(_btnImport);
        Theme.StyleButton(_btnOpen);
        Theme.StyleButton(_btnSegregation);
        Theme.StyleButton(_btnPgdas);
        Theme.StyleButton(_btnReview);
        Theme.StyleButton(_btnCompany);

        _lblCompanyName.ForeColor = Theme.PrimaryDark;
        _lblCompanyName.Font = Theme.UiFont(10F, FontStyle.Bold);
        _lblCompanyCnpj.ForeColor = Theme.Text;
        _lblCompetence.ForeColor = Theme.Text;

        Theme.StyleGrid(_gridRecent);
        _gridRecent.Columns.Clear();
        _gridRecent.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Competência", Width = 88 });
        _gridRecent.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Empresa", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        _gridRecent.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Receita",
            Width = 120,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                Format = "N2"
            }
        });
        _gridRecent.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Situação", Width = 110 });
        _gridRecent.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Última alteração", Width = 132 });

        _lstPending.BackColor = Color.White;
        _lstPending.ForeColor = Theme.Text;
        _lstPending.BorderStyle = BorderStyle.FixedSingle;

        _lstNotices.BackColor = Color.White;
        _lstNotices.ForeColor = Theme.Text;
        _lstNotices.BorderStyle = BorderStyle.FixedSingle;

        foreach (var value in new[]
                 {
                     _lblRevenueValue, _lblRbt12Value, _lblFactorRValue,
                     _lblDocumentsValue, _lblClassifiedValue, _lblPendingValue
                 })
        {
            value.ForeColor = Theme.PrimaryDark;
            value.Font = Theme.UiFont(9.3F, FontStyle.Bold);
        }
    }

    private static void StyleGroup(GroupBox group)
    {
        group.BackColor = Theme.PanelBack;
        group.ForeColor = Theme.PrimaryDark;
        group.Font = Theme.UiFont(8.4F, FontStyle.Bold);
    }
}
