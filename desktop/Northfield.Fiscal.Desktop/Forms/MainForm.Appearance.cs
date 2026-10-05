namespace Northfield.Fiscal.Desktop.Forms;

public sealed partial class MainForm
{
    private void ApplyRuntimeTheme()
    {
        BackColor = Theme.AppBack;
        ForeColor = Theme.Text;
        Font = Theme.UiFont();
        DoubleBuffered = true;

        _menu.BackColor = Color.White;
        _menu.Renderer = Theme.ToolRenderer;
        _menu.Font = Theme.UiFont(9F);
        _menu.Padding = new Padding(8, 2, 0, 2);

        _tool.BackColor = Theme.ToolbarBack;
        _tool.Renderer = Theme.ToolRenderer;
        _tool.Font = Theme.UiFont(8.8F);
        _tool.Padding = new Padding(8, 4, 8, 4);

        foreach (ToolStripItem item in _tool.Items)
        {
            if (item is ToolStripButton button)
            {
                button.DisplayStyle = ToolStripItemDisplayStyle.Text;
                button.Padding = new Padding(9, 2, 9, 2);
                button.Margin = new Padding(1, 0, 1, 0);
                button.ForeColor = Theme.Text;
                button.Font = Theme.UiFont(8.8F);
            }
        }

        _tbProcessar.BackColor = Theme.PrimarySoft;
        _tbProcessar.ForeColor = Theme.PrimaryDark;
        _tbProcessar.Font = Theme.UiFont(8.8F, FontStyle.Bold);
        _tbEnvironment.ForeColor = Theme.Muted;
        _tbEnvironment.Font = Theme.UiFont(8.3F);

        _tabs.DrawMode = TabDrawMode.OwnerDrawFixed;
        _tabs.SizeMode = TabSizeMode.Fixed;
        _tabs.ItemSize = new Size(150, 31);
        _tabs.Padding = new Point(14, 5);
        _tabs.Font = Theme.UiFont(8.8F);

        foreach (TabPage tab in _tabs.TabPages)
            tab.BackColor = Theme.AppBack;

        Theme.StyleButton(_btnImportDocs, primary: true);
        Theme.StyleButton(_btnEditDoc);
        Theme.StyleButton(_btnRemoveDoc);

        Theme.StyleGrid(_gridApuracao);
        Theme.StyleGrid(_gridDocuments);
        Theme.StyleGrid(_gridSegregation);
        Theme.StyleGrid(_gridPgdas);

        ConfigureDocumentGrid(_gridApuracao);
        ConfigureDocumentGrid(_gridDocuments);
        ConfigureSegregationGrid();
        ConfigurePgdasGrid();

        _txtFullMemory.BackColor = Color.White;
        _txtFullMemory.ForeColor = Theme.Text;
        _lstMemory.BackColor = Color.White;
        _lstMemory.ForeColor = Theme.Text;

        _status.BackColor = Theme.ToolbarBack;
        _status.Renderer = Theme.ToolRenderer;
        _status.Font = Theme.UiFont(8.1F);
        _status.Padding = new Padding(6, 1, 6, 1);
        _stEngine.ForeColor = Theme.Warning;
        _stClock.ForeColor = Theme.Muted;

        ApplyThemeRecursive(this);
        ConfigurePreferredSplitter(_splitDocuments, preferredDistance: 1010, trailingPanelMinimum: 340);
        ConfigurePreferredSplitter(_splitMemory, preferredDistance: 390, trailingPanelMinimum: 360);
    }

    private static void ApplyThemeRecursive(Control parent)
    {
        foreach (Control control in parent.Controls)
        {
            switch (control)
            {
                case FlowLayoutPanel flow:
                    flow.BackColor = flow.Parent is TabPage ? Theme.ToolbarBack : Color.White;
                    break;

                case TableLayoutPanel table:
                    table.BackColor = Color.White;
                    break;

                case Panel panel when panel.Dock == DockStyle.Top && panel.Height == 42:
                    panel.BackColor = Theme.HeaderBack;
                    foreach (Control child in panel.Controls)
                    {
                        if (child is Panel accent && accent.Dock == DockStyle.Left && accent.Width <= 6)
                            accent.BackColor = Theme.Primary;
                        else if (child is Label label && label.Top < 15)
                        {
                            label.ForeColor = Theme.PrimaryDark;
                            label.Font = Theme.UiFont(9.6F, FontStyle.Bold);
                        }
                        else if (child is Label subLabel)
                        {
                            subLabel.ForeColor = Theme.Muted;
                            subLabel.Font = Theme.UiFont(7.8F);
                        }
                    }
                    break;

                case Panel panel:
                    if (panel.BorderStyle == BorderStyle.FixedSingle)
                        panel.BackColor = Color.White;
                    break;

                case Label label when label.Text.StartsWith("A interface organiza", StringComparison.Ordinal):
                    label.BackColor = Theme.PrimarySoft;
                    label.ForeColor = Theme.PrimaryDark;
                    label.Font = Theme.UiFont(8.2F);
                    break;

                case Label label:
                    if (label.ForeColor == SystemColors.ControlText)
                        label.ForeColor = Theme.Text;
                    break;
            }

            if (control.HasChildren)
                ApplyThemeRecursive(control);
        }
    }

    private void DrawMainTab(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= _tabs.TabPages.Count)
            return;

        var selected = e.Index == _tabs.SelectedIndex;
        var rect = e.Bounds;
        using var background = new SolidBrush(selected ? Color.White : Theme.ToolbarBack);
        e.Graphics.FillRectangle(background, rect);

        if (selected)
        {
            using var accent = new SolidBrush(Theme.Primary);
            e.Graphics.FillRectangle(accent, rect.Left + 8, rect.Bottom - 3, Math.Max(1, rect.Width - 16), 3);
        }

        var text = _tabs.TabPages[e.Index].Text;
        using var font = Theme.UiFont(8.8F, selected ? FontStyle.Bold : FontStyle.Regular);
        TextRenderer.DrawText(
            e.Graphics,
            text,
            font,
            rect,
            selected ? Theme.PrimaryDark : Theme.Text,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
    }

    private void ConfigureDocumentGrid(DataGridView grid)
    {
        grid.Columns.Clear();
        grid.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Sel.", Width = 44, ReadOnly = true });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Documento", Width = 105 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Emissão", Width = 90 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tomador", Width = 210 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "CNPJ/CPF", Width = 135 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Município", Width = 150 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cód. Serv.", Width = 90 });
        grid.Columns.Add(MoneyColumn("Valor Serviço (R$)", 125));
        grid.Columns.Add(MoneyColumn("Base ISS (R$)", 115));
        grid.Columns.Add(MoneyColumn("ISS Retido (R$)", 120));
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Anexo", Width = 65 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Segregação", Width = 220 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Situação", Width = 130 });
    }

    private void ConfigureSegregationGrid()
    {
        _gridSegregation.Columns.Clear();
        _gridSegregation.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Anexo", Width = 100 });
        _gridSegregation.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Segregação PGDAS-D", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        _gridSegregation.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Documentos",
            Width = 110,
            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        _gridSegregation.Columns.Add(MoneyColumn("Valor (R$)", 150));
    }

    private void ConfigurePgdasGrid()
    {
        _gridPgdas.Columns.Clear();
        _gridPgdas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mercado", Width = 130 });
        _gridPgdas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Anexo", Width = 90 });
        _gridPgdas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Segregação", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        _gridPgdas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ISS", Width = 120 });
        _gridPgdas.Columns.Add(MoneyColumn("Receita (R$)", 160));
    }

    private static DataGridViewTextBoxColumn MoneyColumn(string title, int width) => new()
    {
        HeaderText = title,
        Width = width,
        DefaultCellStyle = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleRight,
            Format = "N2"
        }
    };

    private static void ConfigurePreferredSplitter(SplitContainer split, int preferredDistance, int trailingPanelMinimum)
    {
        void Apply()
        {
            var available = split.Orientation == Orientation.Vertical
                ? split.ClientSize.Width - split.SplitterWidth
                : split.ClientSize.Height - split.SplitterWidth;

            if (available <= 40)
                return;

            var trailing = Math.Min(trailingPanelMinimum, Math.Max(80, available / 2));
            var maximumDistance = Math.Max(20, available - trailing);
            var target = Math.Clamp(preferredDistance, 20, maximumDistance);

            if (target > 0 && target < available && split.SplitterDistance != target)
                split.SplitterDistance = target;
        }

        split.SizeChanged += (_, _) => Apply();
        split.HandleCreated += (_, _) => Apply();
        Apply();
    }
}
