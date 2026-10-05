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
        _menu.BackColor = Color.White;
        _menu.Renderer = Theme.ToolRenderer;
        _menu.Padding = new Padding(8, 2, 0, 2);

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
        _tool.Height = 40;
        _tool.Padding = new Padding(8, 4, 8, 4);
        _tool.GripStyle = ToolStripGripStyle.Hidden;
        _tool.BackColor = Theme.ToolbarBack;
        _tool.Renderer = Theme.ToolRenderer;
        _tool.Font = Theme.UiFont(8.8F);

        AddToolButton("Novo", (_, _) => NewSession());
        AddToolButton("Abrir", (_, _) => OpenSession());
        _tool.Items.Add(new ToolStripSeparator());
        AddToolButton("Importar XML", (_, _) => ImportFiles());
        AddToolButton("Processar", async (_, _) => await ProcessCompetenceAsync(), primary: true);
        AddToolButton("Apuração", (_, _) => _tabs.SelectedTab = _tabApuracao);
        _tool.Items.Add(new ToolStripSeparator());
        AddToolButton("Exportar", (_, _) => ExportCsv());
        AddToolButton("Imprimir", (_, _) => PrintSummary());

        _tool.Items.Add(new ToolStripLabel("Northfield Fiscal  •  Ambiente local")
        {
            Alignment = ToolStripItemAlignment.Right,
            ForeColor = Theme.Muted,
            Font = Theme.UiFont(8.3F),
            Padding = new Padding(8, 0, 4, 0)
        });
    }

    private void AddToolButton(string text, EventHandler handler, bool primary = false)
    {
        var button = new ToolStripButton(text)
        {
            AutoSize = true,
            DisplayStyle = ToolStripItemDisplayStyle.Text,
            Padding = new Padding(9, 2, 9, 2),
            Margin = new Padding(1, 0, 1, 0),
            ForeColor = primary ? Theme.PrimaryDark : Theme.Text,
            BackColor = primary ? Theme.PrimarySoft : Color.Transparent,
            Font = Theme.UiFont(8.8F, primary ? FontStyle.Bold : FontStyle.Regular)
        };
        button.Click += handler;
        _tool.Items.Add(button);
    }

    private void BuildTabs()
    {
        _tabs.Dock = DockStyle.Fill;
        _tabs.Font = Theme.UiFont(8.8F);
        _tabs.DrawMode = TabDrawMode.OwnerDrawFixed;
        _tabs.SizeMode = TabSizeMode.Fixed;
        _tabs.ItemSize = new Size(150, 31);
        _tabs.Padding = new Point(14, 5);
        _tabs.DrawItem += DrawMainTab;
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

    private void DrawMainTab(object? sender, DrawItemEventArgs e)
    {
        var selected = e.Index == _tabs.SelectedIndex;
        var rect = e.Bounds;
        var back = selected ? Color.White : Theme.ToolbarBack;

        using var background = new SolidBrush(back);
        e.Graphics.FillRectangle(background, rect);

        if (selected)
        {
            using var accent = new SolidBrush(Theme.Primary);
            e.Graphics.FillRectangle(accent, rect.Left + 8, rect.Bottom - 3, rect.Width - 16, 3);
        }

        var text = _tabs.TabPages[e.Index].Text;
        var color = selected ? Theme.PrimaryDark : Theme.Text;
        var font = Theme.UiFont(8.8F, selected ? FontStyle.Bold : FontStyle.Regular);
        TextRenderer.DrawText(
            e.Graphics,
            text,
            font,
            rect,
            color,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        font.Dispose();
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
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 128));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 63));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 37));
        _tabApuracao.Controls.Add(root);

        root.Controls.Add(BuildCompanyPanel(), 0, 0);

        var upper = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Vertical,
            SplitterWidth = 5,
            BackColor = Theme.AppBack
        };
        root.Controls.Add(upper, 0, 1);
        upper.Panel1.Controls.Add(BuildMainDocumentsPanel());
        upper.Panel2.Controls.Add(BuildSummaryPanel());
        ConfigurePreferredSplitter(upper, preferredDistance: 1010, trailingPanelMinimum: 340);

        root.Controls.Add(BuildMemoryPanel(), 0, 2);
    }

    private Control BuildCompanyPanel()
    {
        var panel = Theme.SectionPanel();
        panel.Dock = DockStyle.Fill;
        panel.Margin = new Padding(0, 0, 0, 7);
        panel.Padding = new Padding(10, 7, 10, 7);

        var table = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 6,
            RowCount = 2,
            BackColor = Theme.PanelBack,
            Margin = new Padding(0)
        };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 285));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
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

        var options = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(8, 7, 0, 0),
            Margin = new Padding(0)
        };
        _chkConsiderIss.Text = "Considerar ISS retido";
        _chkConsiderIss.Checked = true;
        _chkConsiderIss.AutoSize = true;
        _chkConsiderIss.Margin = new Padding(0, 0, 0, 5);
        _chkGroup.Text = "Agrupar por segregação";
        _chkGroup.AutoSize = true;
        options.Controls.Add(_chkConsiderIss);
        options.Controls.Add(_chkGroup);
        table.Controls.Add(options, 5, 0);

        AddField(table, 0, 1, "RBT12 (R$)", _numRbt12);
        AddField(table, 1, 1, "Folha 12m (R$)", _numPayroll);
        AddField(table, 2, 1, "Fator R (%)", _txtFactorR);

        var info = new Label
        {
            Text = "A interface só organiza os documentos e resultados. As regras tributárias ficam isoladas na biblioteca fiscal.",
            ForeColor = Theme.PrimaryDark,
            BackColor = Theme.PrimarySoft,
            AutoSize = false,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = Theme.UiFont(8.2F),
            Padding = new Padding(10, 0, 10, 0),
            Margin = new Padding(3, 6, 0, 5),
            BorderStyle = BorderStyle.FixedSingle
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
        var host = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1, Margin = new Padding(3, 0, 8, 2) };
        host.RowStyles.Add(new RowStyle(SizeType.Absolute, 21));
        host.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        host.Controls.Add(new Label { Text = label, AutoSize = true, ForeColor = Theme.Muted, Font = Theme.UiFont(8.1F), Dock = DockStyle.Fill, TextAlign = ContentAlignment.BottomLeft }, 0, 0);
        control.Dock = DockStyle.Fill;
        control.Font = Theme.UiFont(8.8F);
        control.Margin = new Padding(0, 2, 0, 1);
        host.Controls.Add(control, 0, 1);
        table.Controls.Add(host, column, row);
    }

    private Control BuildMainDocumentsPanel()
    {
        var panel = Theme.SectionPanel();
        panel.Dock = DockStyle.Fill;
        panel.Margin = new Padding(0, 0, 5, 7);
        panel.Padding = new Padding(0);

        _gridApuracao.Dock = DockStyle.Fill;
        Theme.StyleGrid(_gridApuracao);
        ConfigureDocumentGrid(_gridApuracao);
        panel.Controls.Add(_gridApuracao);
        panel.Controls.Add(CreateSectionHeader("Documentos da competência", "Notas e receitas carregadas para a apuração"));
        return panel;
    }

    private Control BuildSummaryPanel()
    {
        var panel = Theme.SectionPanel();
        panel.Dock = DockStyle.Fill;
        panel.Margin = new Padding(5, 0, 0, 7);
        panel.Padding = new Padding(0);

        var content = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 8,
            ColumnCount = 2,
            Padding = new Padding(12, 8, 12, 8),
            BackColor = Color.White
        };
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66));
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
        for (var i = 0; i < 8; i++) content.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));

        AddSummaryRow(content, 0, "Receita bruta do período", _lblSummaryRevenue, true);
        AddSummaryRow(content, 1, "Serviços sem retenção", _lblSummaryNoRetention);
        AddSummaryRow(content, 2, "Serviços com retenção", _lblSummaryRetention);
        AddSummaryRow(content, 3, "Receita pendente de classificação", _lblSummaryPending);
        AddSummaryRow(content, 4, "Fator R do período", _lblSummaryFactorR, true);
        AddSummaryRow(content, 5, "Total para segregação", _lblSummaryTotal, true);

        _lblAlerts.Text = "Sem alertas";
        _lblAlerts.Dock = DockStyle.Fill;
        _lblAlerts.TextAlign = ContentAlignment.MiddleRight;
        _lblAlerts.ForeColor = Theme.Warning;
        _lblAlerts.Font = Theme.UiFont(8.7F, FontStyle.Bold);
        content.Controls.Add(new Label
        {
            Text = "Alertas / Pendências",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = Theme.UiFont(8.6F),
            ForeColor = Theme.Text
        }, 0, 6);
        content.Controls.Add(_lblAlerts, 1, 6);

        panel.Controls.Add(content);
        panel.Controls.Add(CreateSectionHeader("Resumo da apuração", "Consolidação da competência"));
        return panel;
    }

    private static void AddSummaryRow(TableLayoutPanel table, int row, string caption, Label value, bool bold = false)
    {
        var label = new Label { Text = caption, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = bold ? Theme.PrimaryDark : Theme.Text, Font = Theme.UiFont(8.6F, bold ? FontStyle.Bold : FontStyle.Regular) };
        value.Text = "R$ 0,00";
        value.Dock = DockStyle.Fill;
        value.TextAlign = ContentAlignment.MiddleRight;
        value.ForeColor = Theme.Text;
        value.Font = Theme.UiFont(bold ? 9.2F : 8.8F, bold ? FontStyle.Bold : FontStyle.Regular);
        table.Controls.Add(label, 0, row);
        table.Controls.Add(value, 1, row);
    }

    private Control BuildMemoryPanel()
    {
        var panel = Theme.SectionPanel();
        panel.Dock = DockStyle.Fill;
        panel.Margin = new Padding(0);
        panel.Padding = new Padding(0);

        var split = new SplitContainer
        {
            Dock = DockStyle.Fill,
            SplitterWidth = 5,
            BackColor = Theme.BorderSoft
        };
        panel.Controls.Add(split);
        panel.Controls.Add(CreateSectionHeader("Memória de cálculo", "Documento selecionado e trilha da decisão"));
        ConfigurePreferredSplitter(split, preferredDistance: 390, trailingPanelMinimum: 360);

        var details = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 8,
            ColumnCount = 2,
            Padding = new Padding(12, 8, 8, 8),
            BackColor = Color.White
        };
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

        var right = new TabControl
        {
            Dock = DockStyle.Fill,
            Font = Theme.UiFont(8.6F),
            Padding = new Point(12, 5)
        };
        var fiscal = new TabPage("Análise fiscal") { BackColor = Color.White, Padding = new Padding(8) };
        var service = new TabPage("Itens da LC 116") { BackColor = Color.White, Padding = new Padding(8) };
        var segregation = new TabPage("Segregação") { BackColor = Color.White, Padding = new Padding(8) };
        var obs = new TabPage("Observações") { BackColor = Color.White, Padding = new Padding(8) };
        right.TabPages.AddRange([fiscal, service, segregation, obs]);
        split.Panel2.Controls.Add(right);

        _lstMemory.Dock = DockStyle.Fill;
        _lstMemory.BorderStyle = BorderStyle.None;
        _lstMemory.BackColor = Color.White;
        _lstMemory.ForeColor = Theme.Text;
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
        table.Controls.Add(new Label { Text = caption, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Theme.Muted, Font = Theme.UiFont(8.3F) }, 0, row);
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

        var commands = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, Padding = new Padding(6, 7, 6, 5), BackColor = Theme.ToolbarBack };
        var btnImport = new Button { Text = "Importar XML/ZIP", Width = 132, Height = 30 };
        Theme.StyleButton(btnImport, primary: true);
        btnImport.Click += (_, _) => ImportFiles();
        var btnEdit = new Button { Text = "Editar classificação", Width = 138, Height = 30 };
        Theme.StyleButton(btnEdit);
        btnEdit.Click += (_, _) => EditSelectedDocument();
        var btnRemove = new Button { Text = "Remover", Width = 92, Height = 30 };
        Theme.StyleButton(btnRemove);
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
        panel.Padding = new Padding(0);
        _tabSegregacao.Controls.Add(panel);

        Theme.StyleGrid(_gridSegregation);
        _gridSegregation.Dock = DockStyle.Fill;
        _gridSegregation.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Anexo", Width = 100 });
        _gridSegregation.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Segregação PGDAS-D", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        _gridSegregation.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Documentos", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
        _gridSegregation.Columns.Add(MoneyColumn("Valor (R$)", 150));
        panel.Controls.Add(_gridSegregation);
        panel.Controls.Add(CreateSectionHeader("Segregação da competência", "Receitas agrupadas conforme a classificação vigente"));
    }

    private void BuildMemoryTab()
    {
        var panel = Theme.SectionPanel();
        panel.Dock = DockStyle.Fill;
        panel.Padding = new Padding(0);
        _tabMemoria.Controls.Add(panel);

        _txtFullMemory.Dock = DockStyle.Fill;
        _txtFullMemory.Multiline = true;
        _txtFullMemory.ReadOnly = true;
        _txtFullMemory.ScrollBars = ScrollBars.Both;
        _txtFullMemory.WordWrap = false;
        _txtFullMemory.BackColor = Color.White;
        _txtFullMemory.ForeColor = Theme.Text;
        _txtFullMemory.BorderStyle = BorderStyle.None;
        _txtFullMemory.Font = new Font("Consolas", 9F);
        _txtFullMemory.Margin = new Padding(10);
        panel.Controls.Add(_txtFullMemory);
        panel.Controls.Add(CreateSectionHeader("Memória completa", "Rastreamento da competência e das classificações"));
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
            BackColor = Theme.PrimarySoft,
            ForeColor = Theme.PrimaryDark,
            BorderStyle = BorderStyle.FixedSingle,
            Font = Theme.UiFont(8.8F, FontStyle.Bold)
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

    private static Control CreateSectionHeader(string title, string subtitle)
    {
        var header = new Panel
        {
            Dock = DockStyle.Top,
            Height = 42,
            BackColor = Theme.HeaderBack,
            Padding = new Padding(12, 4, 8, 4)
        };

        var accent = new Panel
        {
            Dock = DockStyle.Left,
            Width = 4,
            BackColor = Theme.Primary,
            Margin = new Padding(0)
        };

        var titleLabel = new Label
        {
            Text = title,
            AutoSize = true,
            ForeColor = Theme.PrimaryDark,
            Font = Theme.UiFont(9.6F, FontStyle.Bold),
            Location = new Point(12, 5)
        };

        var subtitleLabel = new Label
        {
            Text = subtitle,
            AutoSize = true,
            ForeColor = Theme.Muted,
            Font = Theme.UiFont(7.8F),
            Location = new Point(12, 23)
        };

        header.Controls.Add(subtitleLabel);
        header.Controls.Add(titleLabel);
        header.Controls.Add(accent);
        return header;
    }

    private static void ConfigurePreferredSplitter(SplitContainer split, int preferredDistance, int trailingPanelMinimum)
    {
        void Apply()
        {
            var available = split.Orientation == Orientation.Vertical
                ? split.ClientSize.Width - split.SplitterWidth
                : split.ClientSize.Height - split.SplitterWidth;

            if (available <= 40)
                return;

            var trailing = Math.Min(trailingPanelMinimum, Math.Max(80, available / 2));
            var maximumDistance = Math.Max(20, available - trailing);
            var target = Math.Clamp(preferredDistance, 20, maximumDistance);

            if (target > 0 && target < available && split.SplitterDistance != target)
                split.SplitterDistance = target;
        }

        split.SizeChanged += (_, _) => Apply();
        split.HandleCreated += (_, _) => Apply();
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
        _status.BackColor = Theme.ToolbarBack;
        _status.Renderer = Theme.ToolRenderer;
        _status.SizingGrip = false;
        _status.Font = Theme.UiFont(8.1F);
        _status.Padding = new Padding(6, 1, 6, 1);

        _stRecords.Text = "Registros: 0";
        _stSelected.Text = "Selecionado: 0";
        _stCompany.Text = "Empresa: -";
        _stCompetence.Text = "Competência: -";
        _stEngine.Spring = true;
        _stEngine.TextAlign = ContentAlignment.MiddleRight;
        _stEngine.Text = "Motor: não conectado";
        _stEngine.ForeColor = Theme.Warning;
        _stClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        _stClock.ForeColor = Theme.Muted;

        var separator1 = new ToolStripStatusLabel("•") { ForeColor = Theme.Border };
        var separator2 = new ToolStripStatusLabel("•") { ForeColor = Theme.Border };
        var separator3 = new ToolStripStatusLabel("•") { ForeColor = Theme.Border };
        var separator4 = new ToolStripStatusLabel("•") { ForeColor = Theme.Border };

        _status.Items.AddRange([
            _stRecords, separator1,
            _stSelected, separator2,
            _stCompany, separator3,
            _stCompetence,
            _stEngine, separator4, _stClock
        ]);
    }

}
}
