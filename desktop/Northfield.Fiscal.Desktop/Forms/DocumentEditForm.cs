using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Forms;

public sealed class DocumentEditForm : Form
{
    private readonly FiscalDocument _document;
    private readonly TextBox _txtServiceCode = new();
    private readonly TextBox _txtMunicipality = new();
    private readonly TextBox _txtDescription = new();
    private readonly CheckBox _chkIssWithheld = new();
    private readonly NumericUpDown _numIssWithheld = new();
    private readonly ComboBox _cboAnnex = new();
    private readonly TextBox _txtSegregation = new();

    public DocumentEditForm(FiscalDocument document)
    {
        _document = document;
        Text = $"Classificação manual - {document.DisplayDocument}";
        StartPosition = FormStartPosition.CenterParent;
        Size = new Size(720, 520);
        MinimumSize = new Size(650, 460);
        Font = Theme.UiFont();
        BackColor = Theme.AppBack;
        ShowIcon = false;

        BuildUi();
        LoadData();
    }

    private void BuildUi()
    {
        var main = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(14),
            ColumnCount = 2,
            RowCount = 8,
            BackColor = Theme.AppBack
        };
        main.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170));
        main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        Controls.Add(main);

        AddRow(main, 0, "Documento", new Label { Text = _document.DisplayDocument, AutoSize = true, Font = Theme.UiFont(9F, FontStyle.Bold), Anchor = AnchorStyles.Left });
        AddRow(main, 1, "Código do serviço", _txtServiceCode);
        AddRow(main, 2, "Município", _txtMunicipality);

        _txtDescription.Multiline = true;
        _txtDescription.ScrollBars = ScrollBars.Vertical;
        _txtDescription.Height = 90;
        AddRow(main, 3, "Descrição", _txtDescription, 105);

        var issPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, WrapContents = false };
        _chkIssWithheld.Text = "ISS retido";
        _chkIssWithheld.AutoSize = true;
        _chkIssWithheld.Margin = new Padding(0, 5, 15, 0);
        _numIssWithheld.DecimalPlaces = 2;
        _numIssWithheld.Maximum = 999999999m;
        _numIssWithheld.Width = 140;
        _numIssWithheld.ThousandsSeparator = true;
        issPanel.Controls.Add(_chkIssWithheld);
        issPanel.Controls.Add(new Label { Text = "Valor retido (R$)", AutoSize = true, Margin = new Padding(0, 7, 6, 0) });
        issPanel.Controls.Add(_numIssWithheld);
        AddRow(main, 4, "ISS", issPanel);

        _cboAnnex.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboAnnex.Items.AddRange(["", "I", "II", "III", "IV", "V"]);
        AddRow(main, 5, "Anexo", _cboAnnex);
        AddRow(main, 6, "Segregação PGDAS-D", _txtSegregation);

        var notice = new Label
        {
            Text = "Esta edição é manual e serve para uso do programa antes da biblioteca fiscal. O aplicativo não cria nem infere regras tributárias.",
            ForeColor = Theme.Muted,
            AutoSize = true,
            MaximumSize = new Size(500, 0),
            Padding = new Padding(0, 6, 0, 6)
        };
        AddRow(main, 7, "", notice, 55);

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 54,
            FlowDirection = FlowDirection.RightToLeft,
            Padding = new Padding(10),
            BackColor = Color.White
        };
        Controls.Add(buttons);

        var btnSave = new Button { Text = "Salvar", Width = 100, Height = 30, BackColor = Theme.Primary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        btnSave.FlatAppearance.BorderColor = Theme.Primary;
        btnSave.Click += (_, _) => SaveAndClose();
        var btnCancel = new Button { Text = "Cancelar", Width = 100, Height = 30, DialogResult = DialogResult.Cancel };
        buttons.Controls.Add(btnSave);
        buttons.Controls.Add(btnCancel);
        AcceptButton = btnSave;
        CancelButton = btnCancel;
    }

    private static void AddRow(TableLayoutPanel table, int row, string label, Control control, int height = 42)
    {
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
        if (!string.IsNullOrEmpty(label))
        {
            table.Controls.Add(new Label
            {
                Text = label,
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                ForeColor = Theme.Text,
                Font = Theme.UiFont(8.7F)
            }, 0, row);
        }
        control.Dock = control is Label ? DockStyle.None : DockStyle.Fill;
        control.Margin = new Padding(0, 4, 0, 4);
        table.Controls.Add(control, 1, row);
    }

    private void LoadData()
    {
        _txtServiceCode.Text = _document.ServiceCode;
        _txtMunicipality.Text = _document.Municipality;
        _txtDescription.Text = _document.ServiceDescription;
        _chkIssWithheld.Checked = _document.IssWithheld;
        _numIssWithheld.Value = Math.Clamp(_document.IssWithheldValue, _numIssWithheld.Minimum, _numIssWithheld.Maximum);
        _cboAnnex.SelectedItem = _document.Annex;
        if (_cboAnnex.SelectedIndex < 0) _cboAnnex.SelectedIndex = 0;
        _txtSegregation.Text = _document.Segregation;
    }

    private void SaveAndClose()
    {
        _document.ServiceCode = _txtServiceCode.Text.Trim();
        _document.Municipality = _txtMunicipality.Text.Trim();
        _document.ServiceDescription = _txtDescription.Text.Trim();
        _document.IssWithheld = _chkIssWithheld.Checked;
        _document.IssWithheldValue = _numIssWithheld.Value;
        _document.Annex = _cboAnnex.SelectedItem?.ToString() ?? string.Empty;
        _document.Segregation = _txtSegregation.Text.Trim();
        _document.ManualClassification = true;
        _document.Status = "Classificação manual";
        _document.Memory =
        [
            "Classificação informada manualmente.",
            $"Item/código de serviço: {(_document.ServiceCode.Length == 0 ? "não informado" : _document.ServiceCode)}.",
            $"ISS retido: {(_document.IssWithheld ? "Sim" : "Não")}.",
            $"Anexo informado: {(_document.Annex.Length == 0 ? "pendente" : _document.Annex)}.",
            $"Segregação informada: {(_document.Segregation.Length == 0 ? "pendente" : _document.Segregation)}.",
            "Nenhuma regra fiscal automática foi executada pelo programa hospedeiro."
        ];
        DialogResult = DialogResult.OK;
        Close();
    }
}
