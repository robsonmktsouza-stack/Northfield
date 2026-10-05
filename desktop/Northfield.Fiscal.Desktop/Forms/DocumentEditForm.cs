using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Forms;

[System.ComponentModel.DesignerCategory("Form")]
public sealed partial class DocumentEditForm : Form
{
    private readonly FiscalDocument _document;

    public DocumentEditForm(FiscalDocument document)
    {
        _document = document;

        InitializeComponent();
        ApplyRuntimeTheme();

        Text = $"Revisar documento - {document.DisplayDocument}";
        _lblDocumentValue.Text = document.DisplayDocument;
        _btnSave.Click += (_, _) => SaveAndClose();

        LoadData();
    }

    private void ApplyRuntimeTheme()
    {
        Font = Theme.UiFont();
        BackColor = Theme.AppBack;
        DoubleBuffered = true;

        _main.BackColor = Color.White;
        _buttons.BackColor = Theme.ToolbarBack;
        _buttons.BorderStyle = BorderStyle.FixedSingle;

        foreach (Control control in _main.Controls)
        {
            switch (control)
            {
                case Label label:
                    label.ForeColor = Theme.Muted;
                    label.Font = Theme.UiFont(8.4F);
                    break;
                case TextBox textBox:
                    textBox.Font = Theme.UiFont(8.8F);
                    break;
                case ComboBox comboBox:
                    comboBox.Font = Theme.UiFont(8.8F);
                    break;
                case NumericUpDown numeric:
                    numeric.Font = Theme.UiFont(8.8F);
                    break;
            }
        }

        _lblDocumentValue.ForeColor = Theme.Text;
        _lblDocumentValue.Font = Theme.UiFont(9F, FontStyle.Bold);

        _lblNotice.ForeColor = Theme.PrimaryDark;
        _lblNotice.BackColor = Theme.PrimarySoft;
        _lblNotice.Font = Theme.UiFont(8.4F);

        Theme.StyleButton(_btnSave, primary: true);
        Theme.StyleButton(_btnCancel);
    }

    private void LoadData()
    {
        _txtServiceCode.Text = _document.ServiceCode;
        _txtMunicipality.Text = _document.Municipality;
        _txtDescription.Text = _document.ServiceDescription;
        _chkIssWithheld.Checked = _document.IssWithheld;
        _numIssWithheld.Value = Math.Clamp(_document.IssWithheldValue, _numIssWithheld.Minimum, _numIssWithheld.Maximum);
        _cboAnnex.SelectedItem = _document.Annex;
        if (_cboAnnex.SelectedIndex < 0)
            _cboAnnex.SelectedIndex = 0;
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
        _document.Status = "Revisado";
        _document.Memory =
        [
            "Classificação revisada pelo usuário.",
            $"Item/código de serviço: {(_document.ServiceCode.Length == 0 ? "não informado" : _document.ServiceCode)}.",
            $"ISS retido: {(_document.IssWithheld ? "Sim" : "Não")}.",
            $"Anexo informado: {(_document.Annex.Length == 0 ? "pendente" : _document.Annex)}.",
            $"Segregação informada: {(_document.Segregation.Length == 0 ? "pendente" : _document.Segregation)}.",
            "Documento revisado e pronto para conferência."
        ];

        DialogResult = DialogResult.OK;
        Close();
    }
}
