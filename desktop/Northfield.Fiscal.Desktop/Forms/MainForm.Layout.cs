using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Forms;

public sealed partial class MainForm
{
    private MenuStrip _menu = new();
    private ToolStrip _tool = new();
    private TabControl _tabs = new();
    private TabPage _tabApuracao = new("Apuração");
    private TabPage _tabDocumentos = new("Documentos");
    private TabPage _tabSegregacao = new("Segregação");
    private TabPage _tabMemoria = new("Memória de Cálculo");
    private TabPage _tabPgdas = new("PGDAS-D");

    private DateTimePicker _dtCompetence = new();
    private MaskedTextBox _txtCnpj = new();
    private TextBox _txtCorporateName = new();
    private ComboBox _cboTaxRegime = new();
    private TextBox _txtMunicipality = new();
    private NumericUpDown _numRbt12 = new();
    private NumericUpDown _numPayroll = new();
    private TextBox _txtFactorR = new();
    private CheckBox _chkConsiderIss = new();
    private CheckBox _chkGroup = new();

    private DataGridView _gridApuracao = new();
    private DataGridView _gridDocuments = new();
    private DataGridView _gridSegregation = new();
    private DataGridView _gridPgdas = new();
    private TextBox _txtSearch = new();
    private Label _lblSummaryRevenue = new();
    private Label _lblSummaryNoRetention = new();
    private Label _lblSummaryRetention = new();
    private Label _lblSummaryPending = new();
    private Label _lblSummaryFactorR = new();
    private Label _lblSummaryTotal = new();
    private Label _lblAlerts = new();

    private Label _lblMemDocument = new();
    private Label _lblMemIssue = new();
    private Label _lblMemRecipient = new();
    private Label _lblMemTaxId = new();
    private Label _lblMemMunicipality = new();
    private Label _lblMemValue = new();
    private Label _lblMemIssBase = new();
    private Label _lblMemIss = new();
    private ListBox _lstMemory = new();
    private TextBox _txtFullMemory = new();

    private StatusStrip _status = new();
    private ToolStripStatusLabel _stRecords = new();
    private ToolStripStatusLabel _stSelected = new();
    private ToolStripStatusLabel _stCompany = new();
    private ToolStripStatusLabel _stCompetence = new();
    private ToolStripStatusLabel _stEngine = new();
    private ToolStripStatusLabel _stClock = new();

    private void BuildUi()
    {
        SuspendLayout();
        BuildMenu();
        BuildToolbar();
        BuildTabs();
        BuildStatusBar();

        Controls.Add(_tabs);
        Controls.Add(_tool);
        Controls.Add(_menu);
        Controls.Add(_status);
        MainMenuStrip = _menu;
        ResumeLayout();
    }

    private void BuildMenu()
    {
        _menu.Dock = DockStyle.Top;
        _menu.Font = Theme.UiFont(9F);
        _menu.BackColor = Color.FromArgb(246, 249, 252);
        _menu.RenderMode = ToolStripRenderMode.System;

        var file = new ToolStripMenuItem("Ficheiro");
        file.DropDownItems.Add("Novo", null, (_, _) => NewSession());
        file.DropDownItems.Add("Abrir sessão...", null, (_, _) => OpenSession());
        file.DropDownItems.Add("Guardar sessão", null, (_, _) => SaveSession(false));
        file.DropDownItems.Add("Guardar sessão como...", null, (_, _) => SaveSession(true));
        file.DropDownItems.Add(new ToolStripSeparator());
        file.DropDownItems.Add("Importar XML/ZIP...", null, (_, _) => ImportFiles());
        file.DropDownItems.Add("Exportar CSV...", null, (_, _) => ExportCsv());
        file.DropDownItems.Add(new ToolStripSeparator());
        file.DropDownItems.Add("Sair", null, (_, _) => Close());

        var cadastros = new ToolStripMenuItem("Cadastros");
        cadastros.DropDownItems.Add("Dados da empresa", null, (_, _) => FocusCompanyData());

        var moves = new ToolStripMenuItem("Movimentos");
        moves.DropDownItems.Add("Importar documentos", null, (_, _) => ImportFiles());
        moves.DropDownItems.Add("Editar documento selecionado", null, (_, _) => EditSelectedDocument());
        moves.DropDownItems.Add("Remover documento selecionado", null, (_, _) => RemoveSelectedDocument());

        var fiscal = new ToolStripMenuItem("Fiscal");
        fiscal.DropDownItems.Add("Processar competência", null, async (_, _) => await ProcessCompetenceAsync());
        fiscal.DropDownItems.Add("Recalcular resumo", null, (_, _) => RefreshAll());
        fiscal.DropDownItems.Add("Segregação", null, (_, _) => _tabs.SelectedTab = _tabSegregacao);
        fiscal.DropDownItems.Add("Memória de cálculo", null, (_, _) => _tabs.SelectedTab = _tabMemoria);
        fiscal.DropDownItems.Add("PGDAS-D", null, (_, _) => _tabs.SelectedTab = _tabPgdas);

        var reports = new ToolStripMenuItem("Relatórios");
        reports.DropDownItems.Add("Exportar documentos em CSV", null, (_, _) => ExportCsv());
        reports.DropDownItems.Add("Imprimir resumo da apuração", null, (_, _) => PrintSummary());

        var tools = new ToolStripMenuItem("Ferramentas");
        tools.DropDownItems.Add("Limpar documentos", null, (_, _) => ClearDocuments());
        tools.DropDownItems.Add("Abrir pasta de dados locais", null, (_, _) => OpenLocalDataFolder());

        var help = new ToolStripMenuItem("Ajuda");
        help.DropDownItems.Add("Sobre", null, (_, _) => ShowAbout());

        _menu.Items.AddRange([file, cadastros, moves, fiscal, reports, tools, help]);
    }

    private void BuildToolbar()
    {
        _tool.Dock = DockStyle.Top;
        _tool.Height = 42;
        _tool.Padding = new Padding(8, 4, 8, 4);
        _tool.GripStyle = ToolStripGripStyle.Hidden;
        _tool.BackColor = Color.White;
        _tool.RenderMode = ToolStripRenderMode.System;
        _tool.Font = Theme.UiFont(9F);

        AddToolButton("Novo", (_, _) => NewSession());
        AddToolButton("Abrir", (_, _) => OpenSession());
        _tool.Items.Add(new ToolStripSeparator());
        AddToolButton("Importar XML", (_, _) => ImportFiles());
        AddToolButton("Processar", async (_, _) => await ProcessCompetenceAsync());
        AddToolButton("Apuração", (_, _) => _tabs.SelectedTab = _tabApuracao);
        _tool.Items.Add(new ToolStripSeparator());
        AddToolButton("Exportar", (_, _) => ExportCsv());
        AddToolButton("Imprimir", (_, _) => PrintSummary());
    }

    private void AddToolButton(string text, EventHandler handler)
    {
        var button = new ToolStripButton(text)
        {
            AutoSize = true,
            DisplayStyle = ToolStripItemDisplayStyle.Text,
            Padding = new Padding(7, 2, 7, 2)
        };
        button.Click += handler;
        _tool.Items.Add(button);
    }

    private void BuildTabs()
    {
        _tabs.Dock = DockStyle.Fill;
        _tabs.Font = Theme.UiFont(9F);
        _tabs.Padding = new Point(16, 6);
        _tabs.Controls.AddRange([_tabApuracao, _tabDocumentos, _tabSegregacao, _tabMemoria, _tabPgdas]);

        foreach (TabPage tab in _tabs.TabPages)
        {
            tab.BackColor = Theme.AppBack;
            tab.Padding = new Padding(8);
        }

        BuildApuracaoTab();
        BuildDocumentsTab();
        BuildSegregationTab();
        BuildMemoryTab();
        BuildPgdasTab();
    }

    private void BuildApuracaoTab()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 3,
            ColumnCount = 1,
            BackColor = Theme.AppBack,
            Padding = new Padding(0)
        };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 142));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 61));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 39));
        _tabApuracao.Controls.Add(root);

        root.Controls.Add(BuildCompanyPanel(), 0, 0);

        var upper = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Vertical,
            SplitterDistance = 980,
            SplitterWidth = 5,
            BackColor = Theme.AppBack,
            Panel1MinSize = 620,
            Panel2MinSize = 280
        };
        root.Controls.Add(upper, 0, 1);
        upper.Panel1.Controls.Add(BuildMainDocumentsPanel());
        upper.Panel2.Controls.Add(BuildSummaryPanel());

        root.Controls.Add(BuildMemoryPanel(), 0, 2);
    }

    private Control BuildCompanyPanel()
    {
        var panel = Theme.SectionPanel();
        panel.Dock = DockStyle.Fill;
        panel.Margin = new Padding(0, 0, 0, 7);

        var table = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 6,
            RowCount = 2,
            BackColor = Theme.PanelBack
        };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 185));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 195));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        panel.Controls.Add(table);

        _dtCompetence.Format = DateTimePickerFormat.Custom;
        _dtCompetence.CustomFormat = "MM/yyyy";
        _dtCompetence.ShowUpDown = true;
        _txtCnpj.Mask = "00.000.000/0000-00";
        _cboTaxRegime.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboTaxRegime.Items.AddRange(["Simples Nacional"]);
        _cboTaxRegime.SelectedIndex = 0;

        ConfigureMoney(_numRbt12);
        ConfigureMoney(_numPayroll);
        _txtFactorR.ReadOnly = true;
        _txtFactorR.TextAlign = HorizontalAlignment.Right;
        _txtFactorR.BackColor = Color.White;

        AddField(table, 0, 0, "Competência", _dtCompetence);
        AddField(table, 1, 0, "CNPJ", _txtCnpj);
        AddField(table, 2, 0, "Razão social", _txtCorporateName);
        AddField(table, 3, 0, "Regime tributário", _cboTaxRegime);
        AddField(table, 4, 0, "Município", _txtMunicipality);

        var financial = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true, Padding = new Padding(0, 16, 0, 0) };
        _chkConsiderIss.Text = "Considerar ISS retido";
        _chkConsiderIss.Checked = true;
        _chkConsiderIss.AutoSize = true;
        _chkGroup.Text = "Agrupar por segregação";
        _chkGroup.AutoSize = true;
        financial.Controls.Add(_chkConsiderIss);
        financial.Controls.Add(_chkGroup);
        table.Controls.Add(financial, 5, 0);

        AddField(table, 0, 1, "RBT12 (R$)", _numRbt12);
        AddField(table, 1, 1, "Folha 12m (R$)", _numPayroll);
        AddField(table, 2, 1, "Fator R (%)", _txtFactorR);

        var info = new Label
        {
            Text = "O programa hospedeiro importa, confere e apresenta os documentos. A decisão fiscal automática será fornecida pela biblioteca do Simples.",
            ForeColor = Theme.Muted,
            AutoSize = false,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(8, 12, 8, 0)
        };
        table.Controls.Add(info, 3, 1);
        table.SetColumnSpan(info, 3);
        return panel;
    }

    private static void ConfigureMoney(NumericUpDown control)
    {
        control.DecimalPlaces = 2;
        control.Maximum = 999999999999m;
        control.ThousandsSeparator = true;
        control.TextAlign = HorizontalAlignment.Right;
    }

    private static void AddField(TableLayoutPanel table, int column, int row, string label, Control control)
    {
        var host = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1, Margin = new Padding(3, 1, 8, 3) };
        host.RowStyles.Add(new RowStyle(SizeType.Absolute, 23));
        host.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        host.Controls.Add(new Label { Text = label, AutoSize = true, ForeColor = Theme.Text, Font = Theme.UiFont(8.3F), Dock = DockStyle.Fill, TextAlign = ContentAlignment.BottomLeft }, 0, 0);
        control.Dock = DockStyle.Fill;
        control.Margin = new Padding(0, 2, 0, 0);
        host.Controls.Add(control, 0, 1);
        table.Controls.Add(host, column, row);
    }

    private Control BuildMainDocumentsPanel()
    {
        var panel = Theme.SectionPanel();
        panel.Dock = DockStyle.Fill;
        panel.Margin = new Padding(0, 0, 5, 7);

        var title = new Label
        {
            Text = "Documentos da competência",
            Dock = DockStyle.Top,
            Height = 30,
            Font = Theme.UiFont(10F, FontStyle.Bold),
            ForeColor = Theme.PrimaryDark,
            TextAlign = ContentAlignment.MiddleLeft
        };
        panel.Controls.Add(_gridApuracao);
        panel.Controls.Add(title);
        _gridApuracao.Dock = DockStyle.Fill;
        Theme.StyleGrid(_gridApuracao);
        ConfigureDocumentGrid(_gridApuracao);
        return panel;
    }

    private Control BuildSummaryPanel()
    {
        var panel = Theme.SectionPanel();
        panel.Dock = DockStyle.Fill;
        panel.Margin = new Padding(5, 0, 0, 7);

        var title = new Label
        {
            Text = "Resumo da apuração",
            Dock = DockStyle.Top,
            Height = 32,
            Font = Theme.UiFont(10F, FontStyle.Bold),
            ForeColor = Theme.PrimaryDark,
            TextAlign = ContentAlignment.MiddleLeft
        };
        panel.Controls.Add(title);

        var content = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 8,
            ColumnCount = 2,
            Padding = new Padding(0, 36, 0, 0),
            BackColor = Theme.PanelBack
        };
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67));
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
        for (var i = 0; i < 8; i++) content.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
        panel.Controls.Add(content);

        AddSummaryRow(content, 0, "Receita bruta do período", _lblSummaryRevenue, true);
        AddSummaryRow(content, 1, "Serviços sem retenção", _lblSummaryNoRetention);
        AddSummaryRow(content, 2, "Serviços com retenção", _lblSummaryRetention);
        AddSummaryRow(content, 3, "Receita pendente de classificação", _lblSummaryPending);
        AddSummaryRow(content, 4, "Fator R do período", _lblSummaryFactorR, true);
        AddSummaryRow(content, 5, "Total para segregação", _lblSummaryTotal, true);

        _lblAlerts.Text = "Sem alertas";
        _lblAlerts.Dock = DockStyle.Fill;
        _lblAlerts.TextAlign = ContentAlignment.MiddleLeft;
        _lblAlerts.ForeColor = Theme.Warning;
        _lblAlerts.Font = Theme.UiFont(8.7F, FontStyle.Bold);
        content.Controls.Add(new Label { Text = "Alertas / Pendências", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, Font = Theme.UiFont(8.7F), ForeColor = Theme.Text }, 0, 6);
        content.Controls.Add(_lblAlerts, 1, 6);
        return panel;
    }

    private static void AddSummaryRow(TableLayoutPanel table, int row, string caption, Label value, bool bold = false)
    {
        var label = new Label { Text = caption, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Theme.Text, Font = Theme.UiFont(8.7F, bold ? FontStyle.Bold : FontStyle.Regular) };
        value.Text = "R$ 0,00";
        value.Dock = DockStyle.Fill;
        value.TextAlign = ContentAlignment.MiddleRight;
        value.ForeColor = Theme.Text;
        value.Font = Theme.UiFont(8.8F, bold ? FontStyle.Bold : FontStyle.Regular);
        table.Controls.Add(label, 0, row);
        table.Controls.Add(value, 1, row);
    }

    private Control BuildMemoryPanel()
    {
        var panel = Theme.SectionPanel();
        panel.Dock = DockStyle.Fill;
        panel.Margin = new Padding(0);

        var title = new Label
        {
            Text = "Memória de cálculo (documento selecionado)",
            Dock = DockStyle.Top,
            Height = 30,
            Font = Theme.UiFont(10F, FontStyle.Bold),
            ForeColor = Theme.PrimaryDark,
            TextAlign = ContentAlignment.MiddleLeft
        };
        panel.Controls.Add(title);

        var split = new SplitContainer { Dock = DockStyle.Fill, SplitterDistance = 390, SplitterWidth = 5, BackColor = Theme.PanelBack, Padding = new Padding(0, 32, 0, 0) };
        panel.Controls.Add(split);

        var details = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 8, ColumnCount = 2, Padding = new Padding(4) };
        details.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
        details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (var i = 0; i < 8; i++) details.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
        split.Panel1.Controls.Add(details);
        AddDetailRow(details, 0, "Documento", _lblMemDocument, true);
        AddDetailRow(details, 1, "Emissão", _lblMemIssue);
        AddDetailRow(details, 2, "Tomador", _lblMemRecipient);
        AddDetailRow(details, 3, "CNPJ/CPF", _lblMemTaxId);
        AddDetailRow(details, 4, "Município", _lblMemMunicipality);
        AddDetailRow(details, 5, "Valor do serviço", _lblMemValue);
        AddDetailRow(details, 6, "Base de cálculo do ISS", _lblMemIssBase);
        AddDetailRow(details, 7, "ISS retido", _lblMemIss);

        var right = new TabControl { Dock = DockStyle.Fill, Font = Theme.UiFont(8.7F) };
        var fiscal = new TabPage("Análise fiscal") { BackColor = Color.White };
        var service = new TabPage("Itens da LC 116") { BackColor = Color.White };
        var segregation = new TabPage("Segregação") { BackColor = Color.White };
        var obs = new TabPage("Observações") { BackColor = Color.White };
        right.TabPages.AddRange([fiscal, service, segregation, obs]);
        split.Panel2.Controls.Add(right);

        _lstMemory.Dock = DockStyle.Fill;
        _lstMemory.BorderStyle = BorderStyle.None;
        _lstMemory.Font = Theme.UiFont(8.8F);
        fiscal.Controls.Add(_lstMemory);
        service.Controls.Add(CreateInfoBox("O código do serviço e a descrição extraídos do XML aparecem na memória. O enquadramento legal será responsabilidade da biblioteca fiscal."));
        segregation.Controls.Add(CreateInfoBox("A segregação fica visível após classificação manual ou após a futura biblioteca retornar uma decisão."));
        obs.Controls.Add(CreateInfoBox("Use duplo clique em um documento para informar uma classificação manual enquanto o motor ainda não estiver conectado."));
        return panel;
    }

    private static Control CreateInfoBox(string text) => new TextBox
    {
        Text = text,
        ReadOnly = true,
        Multiline = true,
        BorderStyle = BorderStyle.None,
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        ForeColor = Theme.Muted,
        Font = Theme.UiFont(9F),
        Padding = new Padding(8)
    };

    private static void AddDetailRow(TableLayoutPanel table, int row, string caption, Label value, bool bold = false)
    {
        table.Controls.Add(new Label { Text = caption, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Theme.Text, Font = Theme.UiFont(8.5F) }, 0, row);
        value.Dock = DockStyle.Fill;
        value.TextAlign = ContentAlignment.MiddleLeft;
        value.ForeColor = Theme.Text;
        value.Font = Theme.UiFont(8.6F, bold ? FontStyle.Bold : FontStyle.Regular);
        table.Controls.Add(value, 1, row);
    }

    private void BuildDocumentsTab()
    {
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1, BackColor = Theme.AppBack };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        _tabDocumentos.Controls.Add(root);

        var commands = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, Padding = new Padding(4, 5, 4, 5) };
        var btnImport = new Button { Text = "Importar XML/ZIP", Width = 130, Height = 30 };
        btnImport.Click += (_, _) => ImportFiles();
        var btnEdit = new Button { Text = "Editar classificação", Width = 135, Height = 30 };
        btnEdit.Click += (_, _) => EditSelectedDocument();
        var btnRemove = new Button { Text = "Remover", Width = 90, Height = 30 };
        btnRemove.Click += (_, _) => RemoveSelectedDocument();
        _txtSearch.Width = 260;
        _txtSearch.Height = 30;
        _txtSearch.PlaceholderText = "Pesquisar documento, tomador ou CNPJ...";
        commands.Controls.AddRange([btnImport, btnEdit, btnRemove, new Label { Width = 20 }, _txtSearch]);
        root.Controls.Add(commands, 0, 0);

        Theme.StyleGrid(_gridDocuments);
        ConfigureDocumentGrid(_gridDocuments);
        _gridDocuments.Dock = DockStyle.Fill;
        root.Controls.Add(_gridDocuments, 0, 1);
    }

    private void BuildSegregationTab()
    {
        var panel = Theme.SectionPanel();
        panel.Dock = DockStyle.Fill;
        _tabSegregacao.Controls.Add(panel);

        var title = new Label
        {
            Text = "Segregação da competência",
            Dock = DockStyle.Top,
            Height = 38,
            Font = Theme.UiFont(11F, FontStyle.Bold),
            ForeColor = Theme.PrimaryDark,
            TextAlign = ContentAlignment.MiddleLeft
        };
        panel.Controls.Add(title);

        Theme.StyleGrid(_gridSegregation);
        _gridSegregation.Dock = DockStyle.Fill;
        _gridSegregation.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Anexo", Width = 100 });
        _gridSegregation.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Segregação PGDAS-D", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        _gridSegregation.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Documentos", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
        _gridSegregation.Columns.Add(MoneyColumn("Valor (R$)", 150));
        panel.Controls.Add(_gridSegregation);
    }

    private void BuildMemoryTab()
    {
        var panel = Theme.SectionPanel();
        panel.Dock = DockStyle.Fill;
        _tabMemoria.Controls.Add(panel);
        _txtFullMemory.Dock = DockStyle.Fill;
        _txtFullMemory.Multiline = true;
        _txtFullMemory.ReadOnly = true;
        _txtFullMemory.ScrollBars = ScrollBars.Both;
        _txtFullMemory.WordWrap = false;
        _txtFullMemory.BackColor = Color.White;
        _txtFullMemory.Font = new Font("Consolas", 9F);
        panel.Controls.Add(_txtFullMemory);
    }

    private void BuildPgdasTab()
    {
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1, BackColor = Theme.AppBack };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        _tabPgdas.Controls.Add(root);

        var notice = new Label
        {
            Text = "Espelho de preparação para o PGDAS-D. O programa apenas consolida as classificações existentes; a futura biblioteca será responsável por determinar automaticamente cada segregação.",
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            BackColor = Theme.HeaderBack,
            ForeColor = Theme.PrimaryDark,
            BorderStyle = BorderStyle.FixedSingle,
            Font = Theme.UiFont(9F)
        };
        root.Controls.Add(notice, 0, 0);

        Theme.StyleGrid(_gridPgdas);
        _gridPgdas.Dock = DockStyle.Fill;
        _gridPgdas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mercado", Width = 130 });
        _gridPgdas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Anexo", Width = 90 });
        _gridPgdas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Segregação", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        _gridPgdas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ISS", Width = 120 });
        _gridPgdas.Columns.Add(MoneyColumn("Receita (R$)", 160));
        root.Controls.Add(_gridPgdas, 0, 1);
    }

    private void ConfigureDocumentGrid(DataGridView grid)
    {
        grid.Columns.Clear();
        grid.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Sel.", Width = 44, ReadOnly = true });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Documento", Width = 105 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Emissão", Width = 90 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tomador", Width = 210 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "CNPJ/CPF", Width = 135 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Município", Width = 150 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cód. Serv.", Width = 90 });
        grid.Columns.Add(MoneyColumn("Valor Serviço (R$)", 125));
        grid.Columns.Add(MoneyColumn("Base ISS (R$)", 115));
        grid.Columns.Add(MoneyColumn("ISS Retido (R$)", 120));
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Anexo", Width = 65 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Segregação", Width = 220 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Situação", Width = 130 });
        grid.CellDoubleClick += (_, e) => { if (e.RowIndex >= 0) EditSelectedDocument(grid); };
        grid.SelectionChanged += (_, _) => GridSelectionChanged(grid);
    }

    private static DataGridViewTextBoxColumn MoneyColumn(string title, int width) => new()
    {
        HeaderText = title,
        Width = width,
        DefaultCellStyle = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleRight,
            Format = "N2"
        }
    };

    private void BuildStatusBar()
    {
        _status.Dock = DockStyle.Bottom;
        _status.BackColor = Color.FromArgb(244, 247, 250);
        _status.SizingGrip = false;
        _status.Font = Theme.UiFont(8.3F);
        _stRecords.Text = "Registros: 0";
        _stSelected.Text = "Selecionado: 0";
        _stCompany.Text = "Empresa: -";
        _stCompetence.Text = "Competência: -";
        _stEngine.Spring = true;
        _stEngine.TextAlign = ContentAlignment.MiddleRight;
        _stEngine.Text = "Motor: não conectado";
        _stClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        _status.Items.AddRange([_stRecords, new ToolStripStatusLabel("|"), _stSelected, new ToolStripStatusLabel("|"), _stCompany, new ToolStripStatusLabel("|"), _stCompetence, _stEngine, new ToolStripStatusLabel("|"), _stClock]);
    }
}
