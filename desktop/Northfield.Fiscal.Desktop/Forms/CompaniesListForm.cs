using Northfield.Fiscal.Desktop.Models;

namespace Northfield.Fiscal.Desktop.Forms;

[System.ComponentModel.DesignerCategory("Form")]
public sealed partial class CompaniesListForm : Form
{
    private readonly List<CompanyListItem> _companies;

    public CompanyContext? SelectedCompany { get; private set; }

    public CompaniesListForm(IEnumerable<CompanyListItem> companies)
    {
        _companies = companies.ToList();

        InitializeComponent();
        ApplyRuntimeTheme();
        HookEvents();
        RefreshGrid();
    }

    private void ApplyRuntimeTheme()
    {
        BackColor = Theme.AppBack;
        ForeColor = Theme.Text;
        Font = Theme.UiFont(8.5F);

        _commands.BackColor = Theme.ToolbarBack;
        _commands.BorderStyle = BorderStyle.FixedSingle;
        _bottom.BackColor = Theme.ToolbarBack;
        _bottom.BorderStyle = BorderStyle.FixedSingle;

        _lblTitle.ForeColor = Theme.PrimaryDark;
        _lblTitle.Font = Theme.UiFont(9.2F, FontStyle.Bold);
        _lblCount.ForeColor = Theme.Muted;
        _txtSearch.Font = Theme.UiFont(8.4F);

        Theme.StyleButton(_btnSelect, primary: true);
        Theme.StyleButton(_btnClose);
        Theme.StyleGrid(_grid);

        _grid.Columns.Clear();
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "CNPJ",
            Width = 135
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Razão social",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            MinimumWidth = 240
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Município",
            Width = 180
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Regime tributário",
            Width = 135
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Última competência",
            Width = 120
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Última alteração",
            Width = 135
        });
    }

    private void HookEvents()
    {
        _txtSearch.TextChanged += (_, _) => RefreshGrid();
        _btnClose.Click += (_, _) => Close();
        _btnSelect.Click += (_, _) => SelectCurrentCompany();

        _grid.CellDoubleClick += (_, e) =>
        {
            if (e.RowIndex >= 0)
                SelectCurrentCompany();
        };

        _grid.SelectionChanged += (_, _) =>
            _btnSelect.Enabled = _grid.SelectedRows.Count > 0;
    }

    private void RefreshGrid()
    {
        var query = _txtSearch.Text.Trim();

        IEnumerable<CompanyListItem> filtered = _companies;

        if (query.Length > 0)
        {
            filtered = filtered.Where(x =>
                x.Company.Cnpj.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                x.Company.CorporateName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                x.Company.Municipality.Contains(query, StringComparison.OrdinalIgnoreCase));
        }

        var rows = filtered.ToList();

        _grid.SuspendLayout();
        try
        {
            _grid.Rows.Clear();

            foreach (var item in rows)
            {
                var index = _grid.Rows.Add(
                    item.Company.Cnpj,
                    item.Company.CorporateName,
                    item.Company.Municipality,
                    item.Company.TaxRegime,
                    item.LastCompetence.ToString("MM/yyyy"),
                    item.LastUpdatedAt.ToString("dd/MM/yyyy HH:mm"));

                _grid.Rows[index].Tag = item.Company;
            }
        }
        finally
        {
            _grid.ResumeLayout();
        }

        _lblCount.Text = $"{rows.Count} empresa(s)";
        _btnSelect.Enabled = _grid.SelectedRows.Count > 0;
    }

    private void SelectCurrentCompany()
    {
        if (_grid.SelectedRows.Count == 0)
            return;

        if (_grid.SelectedRows[0].Tag is not CompanyContext company)
            return;

        SelectedCompany = company;
        DialogResult = DialogResult.OK;
        Close();
    }
}
