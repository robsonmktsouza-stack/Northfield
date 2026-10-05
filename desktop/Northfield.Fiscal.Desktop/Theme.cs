namespace Northfield.Fiscal.Desktop;

internal static class Theme
{
    public static readonly Color AppBack = Color.FromArgb(245, 247, 250);
    public static readonly Color PanelBack = Color.FromArgb(251, 252, 254);
    public static readonly Color HeaderBack = Color.FromArgb(233, 241, 249);
    public static readonly Color Border = Color.FromArgb(201, 211, 222);
    public static readonly Color Primary = Color.FromArgb(31, 111, 190);
    public static readonly Color PrimaryDark = Color.FromArgb(24, 78, 132);
    public static readonly Color Text = Color.FromArgb(35, 43, 52);
    public static readonly Color Muted = Color.FromArgb(100, 111, 123);
    public static readonly Color GridHeader = Color.FromArgb(237, 243, 249);
    public static readonly Color Selected = Color.FromArgb(218, 235, 251);
    public static readonly Color Success = Color.FromArgb(33, 139, 74);
    public static readonly Color Warning = Color.FromArgb(200, 126, 0);
    public static readonly Color Error = Color.FromArgb(189, 54, 54);

    public static Font UiFont(float size = 9F, FontStyle style = FontStyle.Regular) =>
        new("Segoe UI", size, style, GraphicsUnit.Point);

    public static void StyleGrid(DataGridView grid)
    {
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.GridColor = Border;
        grid.RowHeadersVisible = false;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.ReadOnly = true;
        grid.AutoGenerateColumns = false;
        grid.ColumnHeadersHeight = 30;
        grid.RowTemplate.Height = 27;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle.BackColor = GridHeader;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Text;
        grid.ColumnHeadersDefaultCellStyle.Font = UiFont(8.5F, FontStyle.Regular);
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = GridHeader;
        grid.DefaultCellStyle.Font = UiFont(8.5F);
        grid.DefaultCellStyle.ForeColor = Text;
        grid.DefaultCellStyle.BackColor = Color.White;
        grid.DefaultCellStyle.SelectionBackColor = Selected;
        grid.DefaultCellStyle.SelectionForeColor = Text;
        grid.DefaultCellStyle.Padding = new Padding(3, 0, 3, 0);
    }

    public static Panel SectionPanel() => new()
    {
        BackColor = PanelBack,
        BorderStyle = BorderStyle.FixedSingle,
        Padding = new Padding(10)
    };
}
