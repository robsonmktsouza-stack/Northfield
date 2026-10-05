using Northfield.Fiscal.Desktop.Controls;
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

        _header.BackColor = Color.FromArgb(236, 237, 238);
        _header.BorderColor = Theme.MenuBorder;
        _searchPanel.BackColor = Color.FromArgb(246, 246, 246);
        _footer.BackColor = Color.FromArgb(236, 237, 238);

        _lblTitle.ForeColor = Theme.PrimaryDark;
        _lblTitle.Font = Theme.UiFont(9.2F, FontStyle.Bold);
        _lblSearch.ForeColor = Theme.Text;
        _lblSearch.Font = Theme.UiFont(8.2F);
        _lblCount.ForeColor = Theme.Muted;
        _lblCount.Font = Theme.UiFont(8F);

        _btnNew.Primary = false;
        _btnEdit.Primary = false;
        _btnDelete.Primary = false;
        _btnSelect.Primary = true;
        _btnClose.Primary = false;

        _grid.ApplyNorthfieldStyle();
        _grid.RowTemplate.Height = 27;
        _grid.ColumnHeadersHeight = 29;

        _grid.Columns.Clear();
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Código",
            Width = 72
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "CNPJ",
            Width = 150
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Razão social",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            MinimumWidth = 280
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Município",
            Width = 190
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "UF",
            Width = 56
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Situação",
            Width = 92
        });
    }

    private void HookEvents()
    {
        _txtSearch.TextChanged += (_, _) => RefreshGrid();
        _btnClose.Click += (_, _) => Close();
        _btnNew.Click += (_, _) => CreateNewCompany();
        _btnEdit.Click += (_, _) => EditCurrentCompany();
        _btnSelect.Click += (_, _) => SelectCurrentCompany();

        _grid.CellDoubleClick += (_, e) =>
        {
            if (e.RowIndex >= 0)
                SelectCurrentCompany();
        };

        _grid.SelectionChanged += (_, _) => UpdateActionState();
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
                x.Company.Municipality.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                x.Company.Uf.Contains(query, StringComparison.OrdinalIgnoreCase));
        }

        var rows = filtered
            .OrderBy(x => x.Company.CorporateName)
            .ThenBy(x => x.Company.Cnpj)
            .ToList();

        _grid.SuspendLayout();
        try
        {
            _grid.Rows.Clear();

            for (var i = 0; i < rows.Count; i++)
            {
                var item = rows[i];
                var index = _grid.Rows.Add(
                    (i + 1).ToString("0000"),
                    item.Company.Cnpj,
                    item.Company.CorporateName,
                    item.Company.Municipality,
                    item.Company.Uf,
                    item.Company.IsActive ? "Ativa" : "Inativa");

                _grid.Rows[index].Tag = item.Company;
            }
        }
        finally
        {
            _grid.ResumeLayout();
        }

        _lblCount.Text = rows.Count == 1 ? "1 registro" : $"{rows.Count} registros";
        UpdateActionState();
    }

    private void UpdateActionState()
    {
        var hasSelection = _grid.SelectedRows.Count > 0;
        _btnEdit.Enabled = hasSelection;
        _btnSelect.Enabled = hasSelection;

        // A exclusão ficará ligada quando o cadastro de empresas deixar de ser derivado
        // das sessões de apuração. Mantemos o comando visível sem apagar dados fiscais.
        _btnDelete.Enabled = false;
    }

    private void CreateNewCompany()
    {
        SelectedCompany = new CompanyContext();
        DialogResult = DialogResult.OK;
        Close();
    }

    private void EditCurrentCompany() => SelectCurrentCompany();

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
