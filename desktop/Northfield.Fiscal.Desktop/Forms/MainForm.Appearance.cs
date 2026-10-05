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

        _tool.BackColor = Theme.ToolbarBack;
        _tool.Renderer = Theme.ToolRenderer;
        _tool.Font = Theme.UiFont(8.8F);

        foreach (ToolStripItem item in _tool.Items)
        {
            if (item is ToolStripButton button)
            {
                button.Padding = new Padding(9, 2, 9, 2);
                button.Margin = new Padding(1, 0, 1, 0);
                button.ForeColor = Theme.Text;
                button.Font = Theme.UiFont(8.8F);
            }
        }

        _tbProcessar.BackColor = Color.Transparent;
        _tbProcessar.ForeColor = Theme.Text;
        _tbProcessar.Font = Theme.UiFont(8.8F);
        _tbEnvironment.ForeColor = Theme.Muted;
        _tbEnvironment.Font = Theme.UiFont(8.3F);

        _tabs.DrawMode = TabDrawMode.OwnerDrawFixed;
        _tabs.SizeMode = TabSizeMode.Fixed;
        _tabs.ItemSize = new Size(150, 31);
        _tabs.Font = Theme.UiFont(8.8F);

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

        StyleSection(_documentsHeader, _documentsAccent, _documentsTitle, _documentsSubtitle);
        StyleSection(_summaryHeader, _summaryAccent, _summaryTitle, _summarySubtitle);
        StyleSection(_memoryHeader, _memoryAccent, _memoryTitle, _memorySubtitle);
        StyleSection(_segregationHeader, _segregationAccent, _segregationTitle, _segregationSubtitle);
        StyleSection(_fullMemoryHeader, _fullMemoryAccent, _fullMemoryTitle, _fullMemorySubtitle);

        _companyPanel.BackColor = Color.White;
        _companyTable.BackColor = Color.White;
        _documentsSection.BackColor = Color.White;
        _summarySection.BackColor = Color.White;
        _memorySection.BackColor = Color.White;
        _segregationSection.BackColor = Color.White;
        _fullMemorySection.BackColor = Color.White;

        _lblCompanyInfo.BackColor = Color.White;
        _lblCompanyInfo.ForeColor = Theme.Muted;

        _pgdasNotice.BackColor = Color.White;
        _pgdasNotice.ForeColor = Theme.Muted;
        _pgdasNotice.Font = Theme.UiFont(8.7F);

        _txtFullMemory.BackColor = Color.White;
        _txtFullMemory.ForeColor = Theme.Text;
        _lstMemory.BackColor = Color.White;
        _lstMemory.ForeColor = Theme.Text;

        _status.BackColor = Theme.ToolbarBack;
        _status.Renderer = Theme.ToolRenderer;
        _status.Font = Theme.UiFont(8.1F);
        _stEngine.ForeColor = Theme.Muted;
        _stClock.ForeColor = Theme.Muted;
    }

    private static void StyleSection(Panel header, Panel accent, Label title, Label subtitle)
    {
        header.BackColor = Theme.HeaderBack;
        accent.BackColor = Theme.Border;
        title.ForeColor = Theme.Text;
        title.Font = Theme.UiFont(9.2F, FontStyle.Bold);
        subtitle.ForeColor = Theme.Muted;
        subtitle.Font = Theme.UiFont(7.7F);
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
            using var accent = new SolidBrush(Color.FromArgb(105, 126, 146));
            e.Graphics.FillRectangle(accent, rect.Left + 8, rect.Bottom - 3, Math.Max(1, rect.Width - 16), 3);
        }

        using var font = Theme.UiFont(8.8F, selected ? FontStyle.Bold : FontStyle.Regular);
        TextRenderer.DrawText(
            e.Graphics,
            _tabs.TabPages[e.Index].Text,
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
}
