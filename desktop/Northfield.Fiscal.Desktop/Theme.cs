namespace Northfield.Fiscal.Desktop;

internal static class Theme
{
    // Visual inspirado em sistemas contábeis desktop clássicos:
    // área de trabalho em cinza neutro, painéis claros, cabeçalhos azulados e bordas firmes.
    public static readonly Color AppBack = Color.FromArgb(240, 240, 240);
    public static readonly Color PanelBack = Color.FromArgb(241, 242, 243);
    public static readonly Color HeaderBack = Color.FromArgb(205, 221, 237);
    public static readonly Color HeaderBackStrong = Color.FromArgb(188, 210, 231);
    public static readonly Color ToolbarBack = Color.FromArgb(236, 237, 238);
    public static readonly Color MenuBack = Color.FromArgb(232, 232, 232);
    public static readonly Color MenuHover = Color.FromArgb(222, 222, 222);
    public static readonly Color MenuPressed = Color.FromArgb(214, 214, 214);
    public static readonly Color MenuBorder = Color.FromArgb(184, 184, 184);
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
        new AccountingToolStripRenderer(new AccountingColorTable());

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

    private sealed class AccountingToolStripRenderer : ToolStripProfessionalRenderer
    {
        public AccountingToolStripRenderer(ProfessionalColorTable colorTable)
            : base(colorTable)
        {
            RoundedEdges = false;
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is MenuStrip)
            {
                using var pen = new Pen(MenuBorder);
                e.Graphics.DrawLine(pen, 0, e.ToolStrip.Height - 1, e.ToolStrip.Width, e.ToolStrip.Height - 1);
                return;
            }

            base.OnRenderToolStripBorder(e);
        }
    }

    private sealed class AccountingColorTable : ProfessionalColorTable
    {
        public override Color ToolStripGradientBegin => ToolbarBack;
        public override Color ToolStripGradientMiddle => ToolbarBack;
        public override Color ToolStripGradientEnd => ToolbarBack;
        public override Color MenuStripGradientBegin => MenuBack;
        public override Color MenuStripGradientEnd => MenuBack;
        public override Color MenuItemSelected => MenuHover;
        public override Color MenuItemBorder => Color.FromArgb(198, 198, 198);
        public override Color MenuItemPressedGradientBegin => MenuPressed;
        public override Color MenuItemPressedGradientMiddle => MenuPressed;
        public override Color MenuItemPressedGradientEnd => MenuPressed;
        public override Color ButtonSelectedGradientBegin => MenuHover;
        public override Color ButtonSelectedGradientMiddle => MenuHover;
        public override Color ButtonSelectedGradientEnd => MenuHover;
        public override Color ButtonSelectedBorder => Color.FromArgb(198, 198, 198);
        public override Color ButtonPressedGradientBegin => MenuPressed;
        public override Color ButtonPressedGradientMiddle => MenuPressed;
        public override Color ButtonPressedGradientEnd => MenuPressed;
        public override Color SeparatorDark => MenuBorder;
        public override Color SeparatorLight => Color.FromArgb(245, 245, 245);
        public override Color ToolStripBorder => MenuBorder;
        public override Color ImageMarginGradientBegin => MenuBack;
        public override Color ImageMarginGradientMiddle => MenuBack;
        public override Color ImageMarginGradientEnd => MenuBack;
    }
}
