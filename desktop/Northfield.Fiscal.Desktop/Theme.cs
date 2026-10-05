namespace Northfield.Fiscal.Desktop;

internal static class Theme
{
    // Visual inspirado em sistemas contábeis desktop clássicos:
    // área de trabalho em cinza neutro, painéis claros, cabeçalhos azulados e bordas firmes.
    public static readonly Color AppBack = Color.FromArgb(171, 171, 171);
    public static readonly Color PanelBack = Color.FromArgb(241, 242, 243);
    public static readonly Color HeaderBack = Color.FromArgb(205, 221, 237);
    public static readonly Color HeaderBackStrong = Color.FromArgb(188, 210, 231);
    public static readonly Color ToolbarBack = Color.FromArgb(236, 237, 238);
    public static readonly Color Border = Color.FromArgb(148, 156, 164);
    public static readonly Color BorderSoft = Color.FromArgb(190, 196, 202);
    public static readonly Color Primary = Color.FromArgb(58, 101, 142);
    public static readonly Color PrimaryDark = Color.FromArgb(31, 65, 96);
    public static readonly Color PrimarySoft = Color.FromArgb(220, 231, 241);
    public static readonly Color Text = Color.FromArgb(28, 31, 34);
    public static readonly Color Muted = Color.FromArgb(83, 89, 95);
    public static readonly Color GridHeader = Color.FromArgb(218, 229, 240);
    public static readonly Color GridAlternate = Color.FromArgb(247, 248, 249);
    public static readonly Color Selected = Color.FromArgb(201, 219, 237);
    public static readonly Color Success = Color.FromArgb(28, 126, 62);
    public static readonly Color SuccessSoft = Color.FromArgb(225, 241, 230);
    public static readonly Color Warning = Color.FromArgb(157, 105, 0);
    public static readonly Color WarningSoft = Color.FromArgb(247, 238, 205);
    public static readonly Color Error = Color.FromArgb(168, 45, 45);
    public static readonly Color AccentGold = Color.FromArgb(220, 177, 72);

    public static Font UiFont(float size = 9F, FontStyle style = FontStyle.Regular) =>
        new("Segoe UI", size, style, GraphicsUnit.Point);

    public static ToolStripRenderer ToolRenderer { get; } =
        new ToolStripProfessionalRenderer(new AccountingColorTable())
        {
            RoundedEdges = false
        };

    public static void StyleGrid(DataGridView grid)
    {
        grid.BackgroundColor = Color.FromArgb(226, 228, 230);
        grid.BorderStyle = BorderStyle.Fixed3D;
        grid.GridColor = Color.FromArgb(186, 194, 202);
        grid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        grid.RowHeadersVisible = false;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.ReadOnly = true;
        grid.AutoGenerateColumns = false;
        grid.ColumnHeadersHeight = 28;
        grid.RowTemplate.Height = 25;
        grid.EnableHeadersVisualStyles = false;

        grid.ColumnHeadersDefaultCellStyle.BackColor = GridHeader;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = PrimaryDark;
        grid.ColumnHeadersDefaultCellStyle.Font = UiFont(8.2F, FontStyle.Bold);
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = GridHeader;
        grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = PrimaryDark;
        grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(3, 0, 3, 0);

        grid.DefaultCellStyle.Font = UiFont(8.2F);
        grid.DefaultCellStyle.ForeColor = Text;
        grid.DefaultCellStyle.BackColor = Color.White;
        grid.DefaultCellStyle.SelectionBackColor = Selected;
        grid.DefaultCellStyle.SelectionForeColor = Text;
        grid.DefaultCellStyle.Padding = new Padding(3, 0, 3, 0);

        grid.AlternatingRowsDefaultCellStyle.BackColor = GridAlternate;
    }

    public static Panel SectionPanel() => new()
    {
        BackColor = PanelBack,
        BorderStyle = BorderStyle.Fixed3D,
        Padding = new Padding(6)
    };

    public static void StyleButton(Button button, bool primary = false)
    {
        button.FlatStyle = FlatStyle.Standard;
        button.UseVisualStyleBackColor = false;
        button.Height = 28;
        button.Font = UiFont(8.3F, primary ? FontStyle.Bold : FontStyle.Regular);
        button.Cursor = Cursors.Hand;
        button.Padding = new Padding(5, 0, 5, 0);
        button.BackColor = primary ? Color.FromArgb(218, 228, 237) : Color.FromArgb(235, 236, 237);
        button.ForeColor = Text;
    }

    private sealed class AccountingColorTable : ProfessionalColorTable
    {
        public override Color ToolStripGradientBegin => ToolbarBack;
        public override Color ToolStripGradientMiddle => ToolbarBack;
        public override Color ToolStripGradientEnd => ToolbarBack;
        public override Color MenuStripGradientBegin => Color.FromArgb(239, 240, 241);
        public override Color MenuStripGradientEnd => Color.FromArgb(229, 230, 231);
        public override Color MenuItemSelected => HeaderBack;
        public override Color MenuItemBorder => Border;
        public override Color MenuItemPressedGradientBegin => HeaderBackStrong;
        public override Color MenuItemPressedGradientMiddle => HeaderBackStrong;
        public override Color MenuItemPressedGradientEnd => HeaderBackStrong;
        public override Color ButtonSelectedGradientBegin => HeaderBack;
        public override Color ButtonSelectedGradientMiddle => HeaderBack;
        public override Color ButtonSelectedGradientEnd => HeaderBack;
        public override Color ButtonSelectedBorder => Border;
        public override Color ButtonPressedGradientBegin => HeaderBackStrong;
        public override Color ButtonPressedGradientMiddle => HeaderBackStrong;
        public override Color ButtonPressedGradientEnd => HeaderBackStrong;
        public override Color SeparatorDark => Border;
        public override Color SeparatorLight => Color.White;
        public override Color ToolStripBorder => Border;
        public override Color ImageMarginGradientBegin => ToolbarBack;
        public override Color ImageMarginGradientMiddle => ToolbarBack;
        public override Color ImageMarginGradientEnd => ToolbarBack;
    }
}
