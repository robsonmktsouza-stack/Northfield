using System.ComponentModel;

namespace Northfield.Fiscal.Desktop.Forms;

partial class DocumentEditForm
{
    private IContainer? components = null;

    private TableLayoutPanel _main = null!;
    private Label _lblDocumentValue = null!;
    private TextBox _txtServiceCode = null!;
    private TextBox _txtMunicipality = null!;
    private TextBox _txtDescription = null!;
    private CheckBox _chkIssWithheld = null!;
    private NumericUpDown _numIssWithheld = null!;
    private ComboBox _cboAnnex = null!;
    private TextBox _txtSegregation = null!;
    private Label _lblNotice = null!;
    private FlowLayoutPanel _buttons = null!;
    private Button _btnSave = null!;
    private Button _btnCancel = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new Container();
        _main = new TableLayoutPanel();
        _lblDocumentValue = new Label();
        _txtServiceCode = new TextBox();
        _txtMunicipality = new TextBox();
        _txtDescription = new TextBox();
        _chkIssWithheld = new CheckBox();
        _numIssWithheld = new NumericUpDown();
        _cboAnnex = new ComboBox();
        _txtSegregation = new TextBox();
        _lblNotice = new Label();
        _buttons = new FlowLayoutPanel();
        _btnSave = new Button();
        _btnCancel = new Button();

        SuspendLayout();

        _main.Dock = DockStyle.Fill;
        _main.Padding = new Padding(18, 16, 18, 12);
        _main.ColumnCount = 2;
        _main.RowCount = 8;
        _main.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
        _main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 105F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));

        AddDesignerRow(_main, 0, "Documento", _lblDocumentValue);
        AddDesignerRow(_main, 1, "Código do serviço", _txtServiceCode);
        AddDesignerRow(_main, 2, "Município", _txtMunicipality);
        AddDesignerRow(_main, 3, "Descrição", _txtDescription);
        AddDesignerRow(_main, 5, "Anexo", _cboAnnex);
        AddDesignerRow(_main, 6, "Segregação PGDAS-D", _txtSegregation);

        _lblDocumentValue.AutoSize = true;
        _lblDocumentValue.Anchor = AnchorStyles.Left;

        _txtDescription.Multiline = true;
        _txtDescription.ScrollBars = ScrollBars.Vertical;
        _txtDescription.Dock = DockStyle.Fill;

        var issPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            WrapContents = false,
            Padding = new Padding(0, 4, 0, 0)
        };
        _chkIssWithheld.Text = "ISS retido";
        _chkIssWithheld.AutoSize = true;
        _chkIssWithheld.Margin = new Padding(0, 5, 15, 0);
        _numIssWithheld.DecimalPlaces = 2;
        _numIssWithheld.Maximum = 999999999m;
        _numIssWithheld.Width = 140;
        _numIssWithheld.ThousandsSeparator = true;
        issPanel.Controls.Add(_chkIssWithheld);
        issPanel.Controls.Add(new Label
        {
            Text = "Valor retido (R$)",
            AutoSize = true,
            Margin = new Padding(0, 7, 6, 0)
        });
        issPanel.Controls.Add(_numIssWithheld);
        _main.Controls.Add(new Label
        {
            Text = "ISS",
            AutoSize = true,
            Anchor = AnchorStyles.Left
        }, 0, 4);
        _main.Controls.Add(issPanel, 1, 4);

        _cboAnnex.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboAnnex.Items.AddRange(["", "I", "II", "III", "IV", "V"]);

        _lblNotice.Text = "Esta edição é manual e serve para uso do programa antes da biblioteca fiscal. O aplicativo não cria nem infere regras tributárias.";
        _lblNotice.Dock = DockStyle.Fill;
        _lblNotice.TextAlign = ContentAlignment.MiddleLeft;
        _lblNotice.Padding = new Padding(10, 6, 10, 6);
        _lblNotice.BorderStyle = BorderStyle.FixedSingle;
        _main.Controls.Add(_lblNotice, 1, 7);

        _buttons.Dock = DockStyle.Bottom;
        _buttons.Height = 54;
        _buttons.FlowDirection = FlowDirection.RightToLeft;
        _buttons.Padding = new Padding(10);

        _btnSave.Text = "Salvar classificação";
        _btnSave.Size = new Size(138, 30);
        _btnCancel.Text = "Cancelar";
        _btnCancel.Size = new Size(100, 30);
        _btnCancel.DialogResult = DialogResult.Cancel;

        _buttons.Controls.Add(_btnSave);
        _buttons.Controls.Add(_btnCancel);

        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(720, 520);
        MinimumSize = new Size(650, 460);
        StartPosition = FormStartPosition.CenterParent;
        ShowIcon = false;
        Text = "Classificação manual";

        Controls.Add(_main);
        Controls.Add(_buttons);

        AcceptButton = _btnSave;
        CancelButton = _btnCancel;

        ResumeLayout(false);
    }

    private static void AddDesignerRow(TableLayoutPanel table, int row, string caption, Control control)
    {
        table.Controls.Add(new Label
        {
            Text = caption,
            AutoSize = true,
            Anchor = AnchorStyles.Left
        }, 0, row);

        control.Dock = control is Label ? DockStyle.None : DockStyle.Fill;
        control.Margin = new Padding(0, 5, 0, 5);
        table.Controls.Add(control, 1, row);
    }
}
