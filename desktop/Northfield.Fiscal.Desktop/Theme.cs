namespace Northfield.Fiscal.Desktop;

internal static class Theme
{
    public static readonly Color AppBack = Color.FromArgb(247, 248, 250);
    public static readonly Color PanelBack = Color.White;
    public static readonly Color HeaderBack = Color.FromArgb(250, 251, 252);
    public static readonly Color ToolbarBack = Color.FromArgb(252, 252, 253);
    public static readonly Color Border = Color.FromArgb(214, 219, 225);
    public static readonly Color BorderSoft = Color.FromArgb(232, 235, 239);
    public static readonly Color Primary = Color.FromArgb(72, 103, 132);
    public static readonly Color PrimaryDark = Color.FromArgb(51, 68, 84);
    public static readonly Color PrimarySoft = Color.FromArgb(245, 247, 249);
    public static readonly Color Text = Color.FromArgb(31, 41, 55);
    public static readonly Color Muted = Color.FromArgb(112, 120, 130);
    public static readonly Color GridHeader = Color.FromArgb(245, 247, 249);
    public static readonly Color GridAlternate = Color.FromArgb(251, 252, 253);
    public static readonly Color Selected = Color.FromArgb(233, 238, 243);
    public static readonly Color Success = Color.FromArgb(30, 132, 73);
    public static readonly Color SuccessSoft = Color.FromArgb(232, 246, 238);
    public static readonly Color Warning = Color.FromArgb(186, 112, 0);
    public static readonly Color WarningSoft = Color.FromArgb(255, 247, 224);
    public static readonly Color Error = Color.FromArgb(184, 54, 54);

    public static Font UiFont(float size = 9F, FontStyle style = FontStyle.Regular) =>
        new("Segoe UI", size, style, GraphicsUnit.Point);

    public static ToolStripRenderer ToolRenderer { get; } =
        new ToolStripProfessionalRenderer(new AccountingColorTable())
        {
            RoundedEdges = false
        };

    public static void StyleGrid(DataGridView grid)
    {
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.GridColor = BorderSoft;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        grid.RowHeadersVisible = false;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.ReadOnly = true;
        grid.AutoGenerateColumns = false;
        grid.ColumnHeadersHeight = 32;
        grid.RowTemplate.Height = 29;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle.BackColor = GridHeader;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Text;
        grid.ColumnHeadersDefaultCellStyle.Font = UiFont(8.4F, FontStyle.Regular);
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = GridHeader;
        grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        grid.DefaultCellStyle.Font = UiFont(8.5F);
        grid.DefaultCellStyle.ForeColor = Text;
        grid.DefaultCellStyle.BackColor = Color.White;
        grid.DefaultCellStyle.SelectionBackColor = Selected;
        grid.DefaultCellStyle.SelectionForeColor = Text;
        grid.DefaultCellStyle.Padding = new Padding(4, 0, 4, 0);
        grid.AlternatingRowsDefaultCellStyle.BackColor = GridAlternate;
    }

    public static Panel SectionPanel() => new()
    {
        BackColor = PanelBack,
        BorderStyle = BorderStyle.FixedSingle,
        Padding = new Padding(9)
    };

    public static void StyleButton(Button button, bool primary = false)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.Height = 30;
        button.Font = UiFont(8.7F, primary ? FontStyle.Bold : FontStyle.Regular);
        button.Cursor = Cursors.Hand;
        button.Padding = new Padding(8, 0, 8, 0);

        if (primary)
        {
            button.BackColor = Primary;
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderColor = PrimaryDark;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(38, 115, 188);
            button.FlatAppearance.MouseDownBackColor = PrimaryDark;
        }
        else
        {
            button.BackColor = Color.White;
            button.ForeColor = Text;
            button.FlatAppearance.BorderColor = Border;
            button.FlatAppearance.MouseOverBackColor = PrimarySoft;
            button.FlatAppearance.MouseDownBackColor = HeaderBack;
        }
    }

    private sealed class AccountingColorTable : ProfessionalColorTable
    {
        public override Color ToolStripGradientBegin => ToolbarBack;
        public override Color ToolStripGradientMiddle => ToolbarBack;
        public override Color ToolStripGradientEnd => ToolbarBack;
        public override Color MenuStripGradientBegin => Color.White;
        public override Color MenuStripGradientEnd => Color.White;
        public override Color MenuItemSelected => PrimarySoft;
        public override Color MenuItemBorder => Border;
        public override Color MenuItemPressedGradientBegin => HeaderBack;
        public override Color MenuItemPressedGradientMiddle => HeaderBack;
        public override Color MenuItemPressedGradientEnd => HeaderBack;
        public override Color ButtonSelectedGradientBegin => PrimarySoft;
        public override Color ButtonSelectedGradientMiddle => PrimarySoft;
        public override Color ButtonSelectedGradientEnd => PrimarySoft;
        public override Color ButtonSelectedBorder => Border;
        public override Color SeparatorDark => Border;
        public override Color SeparatorLight => Color.White;
        public override Color ToolStripBorder => BorderSoft;
        public override Color ImageMarginGradientBegin => ToolbarBack;
        public override Color ImageMarginGradientMiddle => ToolbarBack;
        public override Color ImageMarginGradientEnd => ToolbarBack;
    }
}
