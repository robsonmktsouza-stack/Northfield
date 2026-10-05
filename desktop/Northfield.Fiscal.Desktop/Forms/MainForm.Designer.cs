using System.ComponentModel;

namespace Northfield.Fiscal.Desktop.Forms;

partial class MainForm
{
    private IContainer? components = null;

    private MenuStrip _menu = null!;
    private ToolStrip _tool = null!;
    private TabControl _tabs = null!;
    private TabPage _tabApuracao = null!;
    private TabPage _tabDocumentos = null!;
    private TabPage _tabSegregacao = null!;
    private TabPage _tabMemoria = null!;
    private TabPage _tabPgdas = null!;

    private ToolStripMenuItem _miNovo = null!;
    private ToolStripMenuItem _miAbrir = null!;
    private ToolStripMenuItem _miSalvar = null!;
    private ToolStripMenuItem _miSalvarComo = null!;
    private ToolStripMenuItem _miImportar = null!;
    private ToolStripMenuItem _miImportarMovimentos = null!;
    private ToolStripMenuItem _miExportar = null!;
    private ToolStripMenuItem _miSair = null!;
    private ToolStripMenuItem _miDadosEmpresa = null!;
    private ToolStripMenuItem _miEditarDocumento = null!;
    private ToolStripMenuItem _miRemoverDocumento = null!;
    private ToolStripMenuItem _miProcessar = null!;
    private ToolStripMenuItem _miRecalcular = null!;
    private ToolStripMenuItem _miSegregacao = null!;
    private ToolStripMenuItem _miMemoria = null!;
    private ToolStripMenuItem _miPgdas = null!;
    private ToolStripMenuItem _miExportarCsv = null!;
    private ToolStripMenuItem _miImprimir = null!;
    private ToolStripMenuItem _miLimpar = null!;
    private ToolStripMenuItem _miPastaLocal = null!;
    private ToolStripMenuItem _miSobre = null!;

    private ToolStripButton _tbNovo = null!;
    private ToolStripButton _tbAbrir = null!;
    private ToolStripButton _tbImportar = null!;
    private ToolStripButton _tbProcessar = null!;
    private ToolStripButton _tbApuracao = null!;
    private ToolStripButton _tbExportar = null!;
    private ToolStripButton _tbImprimir = null!;
    private ToolStripLabel _tbEnvironment = null!;

    private DateTimePicker _dtCompetence = null!;
    private MaskedTextBox _txtCnpj = null!;
    private TextBox _txtCorporateName = null!;
    private ComboBox _cboTaxRegime = null!;
    private TextBox _txtMunicipality = null!;
    private NumericUpDown _numRbt12 = null!;
    private NumericUpDown _numPayroll = null!;
    private TextBox _txtFactorR = null!;
    private CheckBox _chkConsiderIss = null!;
    private CheckBox _chkGroup = null!;

    private DataGridView _gridApuracao = null!;
    private DataGridView _gridDocuments = null!;
    private DataGridView _gridSegregation = null!;
    private DataGridView _gridPgdas = null!;
    private TextBox _txtSearch = null!;
    private Button _btnImportDocs = null!;
    private Button _btnEditDoc = null!;
    private Button _btnRemoveDoc = null!;

    private Label _lblSummaryRevenue = null!;
    private Label _lblSummaryNoRetention = null!;
    private Label _lblSummaryRetention = null!;
    private Label _lblSummaryPending = null!;
    private Label _lblSummaryFactorR = null!;
    private Label _lblSummaryTotal = null!;
    private Label _lblAlerts = null!;

    private Label _lblMemDocument = null!;
    private Label _lblMemIssue = null!;
    private Label _lblMemRecipient = null!;
    private Label _lblMemTaxId = null!;
    private Label _lblMemMunicipality = null!;
    private Label _lblMemValue = null!;
    private Label _lblMemIssBase = null!;
    private Label _lblMemIss = null!;
    private ListBox _lstMemory = null!;
    private TextBox _txtFullMemory = null!;

    private StatusStrip _status = null!;
    private ToolStripStatusLabel _stRecords = null!;
    private ToolStripStatusLabel _stSelected = null!;
    private ToolStripStatusLabel _stCompany = null!;
    private ToolStripStatusLabel _stCompetence = null!;
    private ToolStripStatusLabel _stEngine = null!;
    private ToolStripStatusLabel _stClock = null!;

    private SplitContainer _splitDocuments = null!;
    private SplitContainer _splitMemory = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new Container();

        _menu = new MenuStrip();
        _tool = new ToolStrip();
        _tabs = new TabControl();
        _tabApuracao = new TabPage();
        _tabDocumentos = new TabPage();
        _tabSegregacao = new TabPage();
        _tabMemoria = new TabPage();
        _tabPgdas = new TabPage();

        _miNovo = new ToolStripMenuItem("Novo");
        _miAbrir = new ToolStripMenuItem("Abrir sessão...");
        _miSalvar = new ToolStripMenuItem("Guardar sessão");
        _miSalvarComo = new ToolStripMenuItem("Guardar sessão como...");
        _miImportar = new ToolStripMenuItem("Importar XML/ZIP...");
        _miImportarMovimentos = new ToolStripMenuItem("Importar documentos");
        _miExportar = new ToolStripMenuItem("Exportar CSV...");
        _miSair = new ToolStripMenuItem("Sair");
        _miDadosEmpresa = new ToolStripMenuItem("Dados da empresa");
        _miEditarDocumento = new ToolStripMenuItem("Editar documento selecionado");
        _miRemoverDocumento = new ToolStripMenuItem("Remover documento selecionado");
        _miProcessar = new ToolStripMenuItem("Processar competência");
        _miRecalcular = new ToolStripMenuItem("Recalcular resumo");
        _miSegregacao = new ToolStripMenuItem("Segregação");
        _miMemoria = new ToolStripMenuItem("Memória de cálculo");
        _miPgdas = new ToolStripMenuItem("PGDAS-D");
        _miExportarCsv = new ToolStripMenuItem("Exportar documentos em CSV");
        _miImprimir = new ToolStripMenuItem("Imprimir resumo da apuração");
        _miLimpar = new ToolStripMenuItem("Limpar documentos");
        _miPastaLocal = new ToolStripMenuItem("Abrir pasta de dados locais");
        _miSobre = new ToolStripMenuItem("Sobre");

        _tbNovo = new ToolStripButton("Novo");
        _tbAbrir = new ToolStripButton("Abrir");
        _tbImportar = new ToolStripButton("Importar XML");
        _tbProcessar = new ToolStripButton("Processar");
        _tbApuracao = new ToolStripButton("Apuração");
        _tbExportar = new ToolStripButton("Exportar");
        _tbImprimir = new ToolStripButton("Imprimir");
        _tbEnvironment = new ToolStripLabel("Northfield Fiscal  •  Ambiente local");

        _dtCompetence = new DateTimePicker();
        _txtCnpj = new MaskedTextBox();
        _txtCorporateName = new TextBox();
        _cboTaxRegime = new ComboBox();
        _txtMunicipality = new TextBox();
        _numRbt12 = new NumericUpDown();
        _numPayroll = new NumericUpDown();
        _txtFactorR = new TextBox();
        _chkConsiderIss = new CheckBox();
        _chkGroup = new CheckBox();

        _gridApuracao = new DataGridView();
        _gridDocuments = new DataGridView();
        _gridSegregation = new DataGridView();
        _gridPgdas = new DataGridView();
        _txtSearch = new TextBox();
        _btnImportDocs = new Button();
        _btnEditDoc = new Button();
        _btnRemoveDoc = new Button();

        _lblSummaryRevenue = new Label();
        _lblSummaryNoRetention = new Label();
        _lblSummaryRetention = new Label();
        _lblSummaryPending = new Label();
        _lblSummaryFactorR = new Label();
        _lblSummaryTotal = new Label();
        _lblAlerts = new Label();

        _lblMemDocument = new Label();
        _lblMemIssue = new Label();
        _lblMemRecipient = new Label();
        _lblMemTaxId = new Label();
        _lblMemMunicipality = new Label();
        _lblMemValue = new Label();
        _lblMemIssBase = new Label();
        _lblMemIss = new Label();
        _lstMemory = new ListBox();
        _txtFullMemory = new TextBox();

        _status = new StatusStrip();
        _stRecords = new ToolStripStatusLabel("Registros: 0");
        _stSelected = new ToolStripStatusLabel("Selecionado: 0");
        _stCompany = new ToolStripStatusLabel("Empresa: -");
        _stCompetence = new ToolStripStatusLabel("Competência: -");
        _stEngine = new ToolStripStatusLabel("Motor: não conectado");
        _stClock = new ToolStripStatusLabel();

        _splitDocuments = new SplitContainer();
        _splitMemory = new SplitContainer();

        SuspendLayout();

        // Menu
        var menuFile = new ToolStripMenuItem("Ficheiro");
        menuFile.DropDownItems.AddRange([
            _miNovo, _miAbrir, _miSalvar, _miSalvarComo,
            new ToolStripSeparator(), _miImportar, _miExportar,
            new ToolStripSeparator(), _miSair
        ]);
        var menuCadastros = new ToolStripMenuItem("Cadastros");
        menuCadastros.DropDownItems.Add(_miDadosEmpresa);
        var menuMovimentos = new ToolStripMenuItem("Movimentos");
        menuMovimentos.DropDownItems.AddRange([_miImportarMovimentos, _miEditarDocumento, _miRemoverDocumento]);
        var menuFiscal = new ToolStripMenuItem("Fiscal");
        menuFiscal.DropDownItems.AddRange([_miProcessar, _miRecalcular, new ToolStripSeparator(), _miSegregacao, _miMemoria, _miPgdas]);
        var menuRelatorios = new ToolStripMenuItem("Relatórios");
        menuRelatorios.DropDownItems.AddRange([_miExportarCsv, _miImprimir]);
        var menuFerramentas = new ToolStripMenuItem("Ferramentas");
        menuFerramentas.DropDownItems.AddRange([_miLimpar, _miPastaLocal]);
        var menuAjuda = new ToolStripMenuItem("Ajuda");
        menuAjuda.DropDownItems.Add(_miSobre);

        _menu.Dock = DockStyle.Top;
        _menu.Items.AddRange([menuFile, menuCadastros, menuMovimentos, menuFiscal, menuRelatorios, menuFerramentas, menuAjuda]);
        _menu.Name = "_menu";
        _menu.Size = new Size(1600, 28);

        // Toolbar
        _tool.Dock = DockStyle.Top;
        _tool.GripStyle = ToolStripGripStyle.Hidden;
        _tool.Height = 40;
        _tool.Items.AddRange([
            _tbNovo, _tbAbrir, new ToolStripSeparator(),
            _tbImportar, _tbProcessar, _tbApuracao,
            new ToolStripSeparator(), _tbExportar, _tbImprimir, _tbEnvironment
        ]);
        _tbEnvironment.Alignment = ToolStripItemAlignment.Right;
        _tool.Name = "_tool";

        // Main tabs
        _tabs.Dock = DockStyle.Fill;
        _tabs.Name = "_tabs";
        _tabs.Controls.AddRange([_tabApuracao, _tabDocumentos, _tabSegregacao, _tabMemoria, _tabPgdas]);
        _tabApuracao.Text = "Apuração";
        _tabDocumentos.Text = "Documentos";
        _tabSegregacao.Text = "Segregação";
        _tabMemoria.Text = "Memória de Cálculo";
        _tabPgdas.Text = "PGDAS-D";
        foreach (TabPage tab in _tabs.TabPages)
        {
            tab.Padding = new Padding(8);
            tab.UseVisualStyleBackColor = false;
        }

        // Apuração
        var apuracaoRoot = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(0)
        };
        apuracaoRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 128));
        apuracaoRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 63F));
        apuracaoRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 37F));
        _tabApuracao.Controls.Add(apuracaoRoot);

        var companyPanel = new Panel
        {
            Dock = DockStyle.Fill,
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(10, 7, 10, 7),
            Margin = new Padding(0, 0, 0, 7)
        };
        var companyTable = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 6,
            RowCount = 2,
            Margin = new Padding(0)
        };
        companyTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        companyTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
        companyTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
        companyTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175F));
        companyTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56F));
        companyTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 285F));
        companyTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
        companyTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
        companyPanel.Controls.Add(companyTable);
        apuracaoRoot.Controls.Add(companyPanel, 0, 0);

        AddDesignerField(companyTable, 0, 0, "Competência", _dtCompetence);
        AddDesignerField(companyTable, 1, 0, "CNPJ", _txtCnpj);
        AddDesignerField(companyTable, 2, 0, "Razão social", _txtCorporateName);
        AddDesignerField(companyTable, 3, 0, "Regime tributário", _cboTaxRegime);
        AddDesignerField(companyTable, 4, 0, "Município", _txtMunicipality);

        _dtCompetence.Format = DateTimePickerFormat.Custom;
        _dtCompetence.CustomFormat = "MM/yyyy";
        _dtCompetence.ShowUpDown = true;
        _txtCnpj.Mask = "00.000.000/0000-00";
        _cboTaxRegime.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboTaxRegime.Items.Add("Simples Nacional");
        _cboTaxRegime.SelectedIndex = 0;

        var optionsPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(8, 7, 0, 0)
        };
        _chkConsiderIss.Text = "Considerar ISS retido";
        _chkConsiderIss.AutoSize = true;
        _chkConsiderIss.Checked = true;
        _chkGroup.Text = "Agrupar por segregação";
        _chkGroup.AutoSize = true;
        optionsPanel.Controls.Add(_chkConsiderIss);
        optionsPanel.Controls.Add(_chkGroup);
        companyTable.Controls.Add(optionsPanel, 5, 0);

        AddDesignerField(companyTable, 0, 1, "RBT12 (R$)", _numRbt12);
        AddDesignerField(companyTable, 1, 1, "Folha 12m (R$)", _numPayroll);
        AddDesignerField(companyTable, 2, 1, "Fator R (%)", _txtFactorR);

        foreach (var n in new[] { _numRbt12, _numPayroll })
        {
            n.DecimalPlaces = 2;
            n.Maximum = 999999999999m;
            n.ThousandsSeparator = true;
            n.TextAlign = HorizontalAlignment.Right;
        }
        _txtFactorR.ReadOnly = true;
        _txtFactorR.TextAlign = HorizontalAlignment.Right;

        var companyInfo = new Label
        {
            Text = "A interface organiza os documentos e resultados. As regras tributárias ficam isoladas na biblioteca fiscal.",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(10, 0, 10, 0),
            BorderStyle = BorderStyle.FixedSingle,
            Margin = new Padding(3, 6, 0, 5)
        };
        companyTable.Controls.Add(companyInfo, 3, 1);
        companyTable.SetColumnSpan(companyInfo, 3);

        // Documents / summary split
        _splitDocuments.Dock = DockStyle.Fill;
        _splitDocuments.Orientation = Orientation.Vertical;
        _splitDocuments.SplitterWidth = 5;
        apuracaoRoot.Controls.Add(_splitDocuments, 0, 1);

        var docPanel = CreateDesignerSection("Documentos da competência", "Notas e receitas carregadas para a apuração");
        _gridApuracao.Dock = DockStyle.Fill;
        docPanel.Controls.Add(_gridApuracao);
        _gridApuracao.BringToFront();
        _splitDocuments.Panel1.Controls.Add(docPanel);

        var summaryPanel = CreateDesignerSection("Resumo da apuração", "Consolidação da competência");
        var summaryTable = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 7,
            Padding = new Padding(12, 8, 12, 8)
        };
        summaryTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66F));
        summaryTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
        for (var i = 0; i < 7; i++) summaryTable.RowStyles.Add(new RowStyle(SizeType.Percent, 14.285F));
        AddSummaryDesignerRow(summaryTable, 0, "Receita bruta do período", _lblSummaryRevenue);
        AddSummaryDesignerRow(summaryTable, 1, "Serviços sem retenção", _lblSummaryNoRetention);
        AddSummaryDesignerRow(summaryTable, 2, "Serviços com retenção", _lblSummaryRetention);
        AddSummaryDesignerRow(summaryTable, 3, "Receita pendente de classificação", _lblSummaryPending);
        AddSummaryDesignerRow(summaryTable, 4, "Fator R do período", _lblSummaryFactorR);
        AddSummaryDesignerRow(summaryTable, 5, "Total para segregação", _lblSummaryTotal);
        AddSummaryDesignerRow(summaryTable, 6, "Alertas / Pendências", _lblAlerts);
        summaryPanel.Controls.Add(summaryTable);
        summaryTable.BringToFront();
        _splitDocuments.Panel2.Controls.Add(summaryPanel);

        // Memory panel
        var memoryPanel = CreateDesignerSection("Memória de cálculo", "Documento selecionado e trilha da decisão");
        _splitMemory.Dock = DockStyle.Fill;
        _splitMemory.SplitterWidth = 5;
        memoryPanel.Controls.Add(_splitMemory);
        _splitMemory.BringToFront();
        apuracaoRoot.Controls.Add(memoryPanel, 0, 2);

        var details = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 8,
            Padding = new Padding(12, 8, 8, 8)
        };
        details.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        for (var i = 0; i < 8; i++) details.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
        AddMemoryDesignerRow(details, 0, "Documento", _lblMemDocument);
        AddMemoryDesignerRow(details, 1, "Emissão", _lblMemIssue);
        AddMemoryDesignerRow(details, 2, "Tomador", _lblMemRecipient);
        AddMemoryDesignerRow(details, 3, "CNPJ/CPF", _lblMemTaxId);
        AddMemoryDesignerRow(details, 4, "Município", _lblMemMunicipality);
        AddMemoryDesignerRow(details, 5, "Valor do serviço", _lblMemValue);
        AddMemoryDesignerRow(details, 6, "Base de cálculo do ISS", _lblMemIssBase);
        AddMemoryDesignerRow(details, 7, "ISS retido", _lblMemIss);
        _splitMemory.Panel1.Controls.Add(details);

        var memoryTabs = new TabControl { Dock = DockStyle.Fill };
        var memoryFiscal = new TabPage("Análise fiscal");
        var memoryLc = new TabPage("Itens da LC 116");
        var memorySeg = new TabPage("Segregação");
        var memoryObs = new TabPage("Observações");
        memoryTabs.TabPages.AddRange([memoryFiscal, memoryLc, memorySeg, memoryObs]);
        _lstMemory.Dock = DockStyle.Fill;
        memoryFiscal.Controls.Add(_lstMemory);
        memoryLc.Controls.Add(CreateDesignerInfoBox("O código do serviço e a descrição extraídos do XML aparecem na memória. O enquadramento legal será responsabilidade da biblioteca fiscal."));
        memorySeg.Controls.Add(CreateDesignerInfoBox("A segregação fica visível após classificação manual ou após a futura biblioteca retornar uma decisão."));
        memoryObs.Controls.Add(CreateDesignerInfoBox("Use duplo clique em um documento para informar uma classificação manual enquanto o motor ainda não estiver conectado."));
        _splitMemory.Panel2.Controls.Add(memoryTabs);

        // Documents tab
        var docsRoot = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2
        };
        docsRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        docsRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tabDocumentos.Controls.Add(docsRoot);

        var docsCommands = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            WrapContents = false,
            Padding = new Padding(6, 7, 6, 5)
        };
        _btnImportDocs.Text = "Importar XML/ZIP";
        _btnImportDocs.Size = new Size(132, 30);
        _btnEditDoc.Text = "Editar classificação";
        _btnEditDoc.Size = new Size(138, 30);
        _btnRemoveDoc.Text = "Remover";
        _btnRemoveDoc.Size = new Size(92, 30);
        _txtSearch.Width = 280;
        _txtSearch.PlaceholderText = "Pesquisar documento, tomador ou CNPJ...";
        docsCommands.Controls.AddRange([_btnImportDocs, _btnEditDoc, _btnRemoveDoc, new Label { Width = 16 }, _txtSearch]);
        docsRoot.Controls.Add(docsCommands, 0, 0);
        _gridDocuments.Dock = DockStyle.Fill;
        docsRoot.Controls.Add(_gridDocuments, 0, 1);

        // Segregation tab
        var segregationPanel = CreateDesignerSection("Segregação da competência", "Receitas agrupadas conforme a classificação vigente");
        _gridSegregation.Dock = DockStyle.Fill;
        segregationPanel.Controls.Add(_gridSegregation);
        _gridSegregation.BringToFront();
        _tabSegregacao.Controls.Add(segregationPanel);

        // Full memory tab
        var fullMemoryPanel = CreateDesignerSection("Memória completa", "Rastreamento da competência e das classificações");
        _txtFullMemory.Dock = DockStyle.Fill;
        _txtFullMemory.Multiline = true;
        _txtFullMemory.ReadOnly = true;
        _txtFullMemory.ScrollBars = ScrollBars.Both;
        _txtFullMemory.WordWrap = false;
        _txtFullMemory.Font = new Font("Consolas", 9F);
        fullMemoryPanel.Controls.Add(_txtFullMemory);
        _txtFullMemory.BringToFront();
        _tabMemoria.Controls.Add(fullMemoryPanel);

        // PGDAS-D tab
        var pgdasRoot = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2
        };
        pgdasRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
        pgdasRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        var pgdasNotice = new Label
        {
            Text = "Espelho de preparação para o PGDAS-D. O programa consolida as classificações existentes; a biblioteca será responsável pelas decisões fiscais automáticas.",
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            BorderStyle = BorderStyle.FixedSingle,
            TextAlign = ContentAlignment.MiddleLeft
        };
        pgdasRoot.Controls.Add(pgdasNotice, 0, 0);
        _gridPgdas.Dock = DockStyle.Fill;
        pgdasRoot.Controls.Add(_gridPgdas, 0, 1);
        _tabPgdas.Controls.Add(pgdasRoot);

        // Status
        _status.Dock = DockStyle.Bottom;
        _stEngine.Spring = true;
        _stEngine.TextAlign = ContentAlignment.MiddleRight;
        _stClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        _status.Items.AddRange([
            _stRecords, new ToolStripStatusLabel("•"),
            _stSelected, new ToolStripStatusLabel("•"),
            _stCompany, new ToolStripStatusLabel("•"),
            _stCompetence, _stEngine, new ToolStripStatusLabel("•"), _stClock
        ]);

        // Form
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(1600, 900);
        MinimumSize = new Size(1180, 720);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Northfield Fiscal - Simples Nacional";
        WindowState = FormWindowState.Maximized;
        KeyPreview = true;
        AllowDrop = true;

        Controls.Add(_tabs);
        Controls.Add(_tool);
        Controls.Add(_menu);
        Controls.Add(_status);
        MainMenuStrip = _menu;

        ResumeLayout(false);
        PerformLayout();
    }

    private static void AddDesignerField(TableLayoutPanel table, int column, int row, string caption, Control control)
    {
        var host = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Margin = new Padding(3, 0, 8, 2)
        };
        host.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
        host.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        host.Controls.Add(new Label
        {
            Text = caption,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.BottomLeft,
            AutoSize = false
        }, 0, 0);
        control.Dock = DockStyle.Fill;
        control.Margin = new Padding(0, 2, 0, 1);
        host.Controls.Add(control, 0, 1);
        table.Controls.Add(host, column, row);
    }

    private static Panel CreateDesignerSection(string title, string subtitle)
    {
        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(0)
        };
        var header = new Panel
        {
            Dock = DockStyle.Top,
            Height = 42,
            Padding = new Padding(12, 4, 8, 4)
        };
        var accent = new Panel { Dock = DockStyle.Left, Width = 4 };
        var titleLabel = new Label
        {
            Text = title,
            AutoSize = true,
            Location = new Point(12, 5)
        };
        var subtitleLabel = new Label
        {
            Text = subtitle,
            AutoSize = true,
            Location = new Point(12, 23)
        };
        header.Controls.Add(subtitleLabel);
        header.Controls.Add(titleLabel);
        header.Controls.Add(accent);
        panel.Controls.Add(header);
        return panel;
    }

    private static TextBox CreateDesignerInfoBox(string text) => new()
    {
        Text = text,
        Dock = DockStyle.Fill,
        Multiline = true,
        ReadOnly = true,
        BorderStyle = BorderStyle.None
    };

    private static void AddSummaryDesignerRow(TableLayoutPanel table, int row, string caption, Label value)
    {
        table.Controls.Add(new Label
        {
            Text = caption,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, row);
        value.Text = row == 4 ? "0,00 %" : "R$ 0,00";
        value.Dock = DockStyle.Fill;
        value.TextAlign = ContentAlignment.MiddleRight;
        table.Controls.Add(value, 1, row);
    }

    private static void AddMemoryDesignerRow(TableLayoutPanel table, int row, string caption, Label value)
    {
        table.Controls.Add(new Label
        {
            Text = caption,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, row);
        value.Text = "-";
        value.Dock = DockStyle.Fill;
        value.TextAlign = ContentAlignment.MiddleLeft;
        table.Controls.Add(value, 1, row);
    }
}
