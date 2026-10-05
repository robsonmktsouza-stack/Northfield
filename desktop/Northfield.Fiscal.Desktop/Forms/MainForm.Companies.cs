namespace Northfield.Fiscal.Desktop.Forms;

public sealed partial class MainForm
{
    private void ShowCompanies()
    {
        var companies = _sessionService.GetCompanies(ReadCompanyFromForm());

        using var form = new CompaniesListForm(companies);
        if (form.ShowDialog(this) != DialogResult.OK || form.SelectedCompany is null)
            return;

        LoadCompanyToForm(form.SelectedCompany);
        RefreshAll();
        SaveLastSessionSilently();
        ShowRoutineTab(_tabApuracao);
    }
}
