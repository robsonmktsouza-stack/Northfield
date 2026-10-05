using Northfield.Fiscal.Desktop.Controls;
using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Forms;

public sealed partial class MainForm
{
    private TabPage _tabHome = null!;
    private HomeWorkspaceControl _homeWorkspace = null!;

    private void InitializeHomeWorkspace()
    {
        _tabHome = new TabPage
        {
            Text = "Início",
            Name = "_tabHome",
            Padding = new Padding(0),
            BackColor = Theme.AppBack
        };

        _homeWorkspace = new HomeWorkspaceControl
        {
            Dock = DockStyle.Fill
        };

        _tabHome.Controls.Add(_homeWorkspace);
        _tabs.TabPages.Insert(0, _tabHome);
        _tabs.SelectedTab = _tabHome;

        _homeWorkspace.NewAssessmentRequested += (_, _) =>
        {
            NewSession();
            _tabs.SelectedTab = _tabApuracao;
        };

        _homeWorkspace.ImportRequested += (_, _) =>
        {
            _tabs.SelectedTab = _tabApuracao;
            ImportFiles();
        };

        _homeWorkspace.OpenAssessmentRequested += (_, _) =>
        {
            OpenSession();
            if (_currentSessionPath is not null)
                _tabs.SelectedTab = _tabApuracao;
        };

        _homeWorkspace.ReviewRequested += (_, _) => _tabs.SelectedTab = _tabDocumentos;
        _homeWorkspace.SegregationRequested += (_, _) => _tabs.SelectedTab = _tabSegregacao;
        _homeWorkspace.PgdasRequested += (_, _) => _tabs.SelectedTab = _tabPgdas;
        _homeWorkspace.CompanyRequested += (_, _) => FocusCompanyData();
        _homeWorkspace.RecentAssessmentOpenRequested += (_, path) => OpenSessionPath(path);
    }

    private void RefreshHomeWorkspace()
    {
        if (_homeWorkspace is null)
            return;

        var company = ReadCompanyFromForm();
        var recent = _sessionService.GetRecentSessions(_currentSessionPath).ToList();

        if (_documents.Count > 0 || !string.IsNullOrWhiteSpace(company.CorporateName))
        {
            var current = new RecentSessionInfo
            {
                Competence = company.Competence,
                CompanyName = string.IsNullOrWhiteSpace(company.CorporateName)
                    ? "Empresa não informada"
                    : company.CorporateName,
                Cnpj = company.Cnpj,
                Revenue = _documents.Sum(d => d.ServiceValue),
                Status = _documents.Count == 0
                    ? "Sem documentos"
                    : _documents.All(d => !string.IsNullOrWhiteSpace(d.Segregation))
                        ? "Conferida"
                        : "Em andamento",
                SavedAt = DateTime.Now,
                FilePath = _currentSessionPath ?? string.Empty
            };

            recent.RemoveAll(x =>
                (!string.IsNullOrWhiteSpace(current.FilePath) &&
                 string.Equals(x.FilePath, current.FilePath, StringComparison.OrdinalIgnoreCase)) ||
                (x.Competence.Year == current.Competence.Year &&
                 x.Competence.Month == current.Competence.Month &&
                 string.Equals(x.Cnpj, current.Cnpj, StringComparison.OrdinalIgnoreCase)));

            recent.Insert(0, current);
        }

        _homeWorkspace.UpdateData(company, _documents, recent.Take(10).ToList());
    }

    private void OpenSessionPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            return;

        try
        {
            var data = _sessionService.Load(path);
            LoadSessionData(data);
            _currentSessionPath = path;
            _tabs.SelectedTab = _tabApuracao;
            RefreshAll();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Abrir apuração", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
