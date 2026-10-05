#nullable disable
using System.ComponentModel;

namespace Northfield.Fiscal.Desktop.Forms;

partial class DocumentEditForm
{
    private IContainer components = null;
    private TableLayoutPanel _main;
    private Label _lblDocumentCaption;
    private Label _lblServiceCodeCaption;
    private Label _lblMunicipalityCaption;
    private Label _lblDescriptionCaption;
    private Label _lblIssCaption;
    private Label _lblAnnexCaption;
    private Label _lblSegregationCaption;
    private Label _lblDocumentValue;
    private TextBox _txtServiceCode;
    private TextBox _txtMunicipality;
    private TextBox _txtDescription;
    private FlowLayoutPanel _issPanel;
    private CheckBox _chkIssWithheld;
    private Label _lblIssValueCaption;
    private NumericUpDown _numIssWithheld;
    private ComboBox _cboAnnex;
    private TextBox _txtSegregation;
    private Label _lblNotice;
    private FlowLayoutPanel _buttons;
    private Button _btnSave;
    private Button _btnCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new Container();
        _main = new TableLayoutPanel();
        _lblDocumentCaption = new Label();
        _lblServiceCodeCaption = new Label();
        _lblMunicipalityCaption = new Label();
        _lblDescriptionCaption = new Label();
        _lblIssCaption = new Label();
        _lblAnnexCaption = new Label();
        _lblSegregationCaption = new Label();
        _lblDocumentValue = new Label();
        _txtServiceCode = new TextBox();
        _txtMunicipality = new TextBox();
        _txtDescription = new TextBox();
        _issPanel = new FlowLayoutPanel();
        _chkIssWithheld = new CheckBox();
        _lblIssValueCaption = new Label();
        _numIssWithheld = new NumericUpDown();
        _cboAnnex = new ComboBox();
        _txtSegregation = new TextBox();
        _lblNotice = new Label();
        _buttons = new FlowLayoutPanel();
        _btnSave = new Button();
        _btnCancel = new Button();
        _main.SuspendLayout();
        _issPanel.SuspendLayout();
        ((ISupportInitialize)_numIssWithheld).BeginInit();
        _buttons.SuspendLayout();
        SuspendLayout();

        _main.ColumnCount = 2;
        _main.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 155F));
        _main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _main.Controls.Add(_lblDocumentCaption, 0, 0);
        _main.Controls.Add(_lblDocumentValue, 1, 0);
        _main.Controls.Add(_lblServiceCodeCaption, 0, 1);
        _main.Controls.Add(_txtServiceCode, 1, 1);
        _main.Controls.Add(_lblMunicipalityCaption, 0, 2);
        _main.Controls.Add(_txtMunicipality, 1, 2);
        _main.Controls.Add(_lblDescriptionCaption, 0, 3);
        _main.Controls.Add(_txtDescription, 1, 3);
        _main.Controls.Add(_lblIssCaption, 0, 4);
        _main.Controls.Add(_issPanel, 1, 4);
        _main.Controls.Add(_lblAnnexCaption, 0, 5);
        _main.Controls.Add(_cboAnnex, 1, 5);
        _main.Controls.Add(_lblSegregationCaption, 0, 6);
        _main.Controls.Add(_txtSegregation, 1, 6);
        _main.Controls.Add(_lblNotice, 1, 7);
        _main.Dock = DockStyle.Fill;
        _main.Location = new Point(0, 0);
        _main.Name = "_main";
        _main.Padding = new Padding(18, 16, 18, 12);
        _main.RowCount = 8;
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 105F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        _main.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
        _main.Size = new Size(720, 466);

        _lblDocumentCaption.Anchor = AnchorStyles.Left;
        _lblDocumentCaption.AutoSize = true;
        _lblDocumentCaption.Text = "Documento";
        _lblServiceCodeCaption.Anchor = AnchorStyles.Left;
        _lblServiceCodeCaption.AutoSize = true;
        _lblServiceCodeCaption.Text = "Código do serviço";
        _lblMunicipalityCaption.Anchor = AnchorStyles.Left;
        _lblMunicipalityCaption.AutoSize = true;
        _lblMunicipalityCaption.Text = "Município";
        _lblDescriptionCaption.Anchor = AnchorStyles.Left;
        _lblDescriptionCaption.AutoSize = true;
        _lblDescriptionCaption.Text = "Descrição";
        _lblIssCaption.Anchor = AnchorStyles.Left;
        _lblIssCaption.AutoSize = true;
        _lblIssCaption.Text = "ISS";
        _lblAnnexCaption.Anchor = AnchorStyles.Left;
        _lblAnnexCaption.AutoSize = true;
        _lblAnnexCaption.Text = "Anexo";
        _lblSegregationCaption.Anchor = AnchorStyles.Left;
        _lblSegregationCaption.AutoSize = true;
        _lblSegregationCaption.Text = "Segregação PGDAS-D";

        _lblDocumentValue.Anchor = AnchorStyles.Left;
        _lblDocumentValue.AutoSize = true;
        _lblDocumentValue.Text = "-";
        _txtServiceCode.Dock = DockStyle.Fill;
        _txtServiceCode.Margin = new Padding(0, 5, 0, 5);
        _txtMunicipality.Dock = DockStyle.Fill;
        _txtMunicipality.Margin = new Padding(0, 5, 0, 5);
        _txtDescription.Dock = DockStyle.Fill;
        _txtDescription.Margin = new Padding(0, 5, 0, 5);
        _txtDescription.Multiline = true;
        _txtDescription.ScrollBars = ScrollBars.Vertical;

        _issPanel.Controls.Add(_chkIssWithheld);
        _issPanel.Controls.Add(_lblIssValueCaption);
        _issPanel.Controls.Add(_numIssWithheld);
        _issPanel.Dock = DockStyle.Fill;
        _issPanel.Location = new Point(173, 247);
        _issPanel.Name = "_issPanel";
        _issPanel.Padding = new Padding(0, 4, 0, 0);
        _issPanel.WrapContents = false;
        _chkIssWithheld.AutoSize = true;
        _chkIssWithheld.Margin = new Padding(0, 5, 15, 0);
        _chkIssWithheld.Text = "ISS retido";
        _lblIssValueCaption.AutoSize = true;
        _lblIssValueCaption.Margin = new Padding(0, 7, 6, 0);
        _lblIssValueCaption.Text = "Valor retido (R$)";
        _numIssWithheld.DecimalPlaces = 2;
        _numIssWithheld.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
        _numIssWithheld.Size = new Size(140, 23);
        _numIssWithheld.ThousandsSeparator = true;

        _cboAnnex.Dock = DockStyle.Fill;
        _cboAnnex.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboAnnex.Items.AddRange(new object[] { "", "I", "II", "III", "IV", "V" });
        _cboAnnex.Margin = new Padding(0, 5, 0, 5);
        _txtSegregation.Dock = DockStyle.Fill;
        _txtSegregation.Margin = new Padding(0, 5, 0, 5);

        _lblNotice.BorderStyle = BorderStyle.FixedSingle;
        _lblNotice.Dock = DockStyle.Fill;
        _lblNotice.Padding = new Padding(10, 6, 10, 6);
        _lblNotice.Text = "Revise os dados abaixo e informe o anexo e a segregação aplicável.";
        _lblNotice.TextAlign = ContentAlignment.MiddleLeft;

        _buttons.Controls.Add(_btnSave);
        _buttons.Controls.Add(_btnCancel);
        _buttons.Dock = DockStyle.Bottom;
        _buttons.FlowDirection = FlowDirection.RightToLeft;
        _buttons.Height = 54;
        _buttons.Location = new Point(0, 466);
        _buttons.Name = "_buttons";
        _buttons.Padding = new Padding(10);

        _btnSave.Size = new Size(138, 30);
        _btnSave.Text = "Salvar classificação";
        _btnCancel.DialogResult = DialogResult.Cancel;
        _btnCancel.Size = new Size(100, 30);
        _btnCancel.Text = "Cancelar";

        AcceptButton = _btnSave;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _btnCancel;
        ClientSize = new Size(720, 520);
        Controls.Add(_main);
        Controls.Add(_buttons);
        MinimumSize = new Size(650, 460);
        Name = "DocumentEditForm";
        ShowIcon = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Revisar documento";

        _main.ResumeLayout(false);
        _main.PerformLayout();
        _issPanel.ResumeLayout(false);
        _issPanel.PerformLayout();
        ((ISupportInitialize)_numIssWithheld).EndInit();
        _buttons.ResumeLayout(false);
        ResumeLayout(false);
    }
}
