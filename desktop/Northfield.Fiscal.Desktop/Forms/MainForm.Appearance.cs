namespace Northfield.Fiscal.Desktop.Forms;

public sealed partial class MainForm
{
    private void ApplyRuntimeTheme()
    {
        BackColor = Theme.AppBack;
        ForeColor = Theme.Text;
        Font = Theme.UiFont(8.6F);
        DoubleBuffered = true;

        _menu.BackColor = Theme.MenuBack;
        _menu.Renderer = Theme.ToolRenderer;
        _menu.Font = Theme.UiFont(8.4F);
        _menu.Padding = new Padding(6, 2, 6, 2);
        _menu.AutoSize = false;
        _menu.Height = 30;

        _tool.Visible = false;

        foreach (ToolStripItem item in _menu.Items)
        {
            switch (item)
            {
                case ToolStripMenuItem menuItem:
                    menuItem.Padding = new Padding(7, 0, 7, 0);
                    menuItem.Margin = Padding.Empty;
                    break;

                case ToolStripButton button:
                    button.Padding = new Padding(8, 0, 8, 0);
                    button.Margin = Padding.Empty;
                    button.ForeColor = Theme.Text;
                    button.Font = Theme.UiFont(8.3F);
                    button.BackColor = Color.Transparent;
                    break;

                case ToolStripSeparator separator:
                    separator.Margin = new Padding(5, 4, 5, 4);
                    break;
            }
        }

        _tbProcessar.Font = Theme.UiFont(8.3F, FontStyle.Bold);
        _tbEnvironment.ForeColor = Theme.Muted;
        _tbEnvironment.Font = Theme.UiFont(8F);
        _tbEnvironment.Margin = new Padding(12, 0, 8, 0);

        // Deixa o próprio Windows desenhar as abas, dando aparência de software desktop clássico.
        _tabs.DrawMode = TabDrawMode.Normal;
        _tabs.SizeMode = TabSizeMode.Normal;
        _tabs.Appearance = TabAppearance.Normal;
        _tabs.Font = Theme.UiFont(8.4F);
        _tabs.Padding = new Point(14, 4);

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

        StyleSection(_documentsHeader, _documentsAccent, _documentsTitle, _documentsSubtitle);
        StyleSection(_summaryHeader, _summaryAccent, _summaryTitle, _summarySubtitle);
        StyleSection(_memoryHeader, _memoryAccent, _memoryTitle, _memorySubtitle);
        StyleSection(_segregationHeader, _segregationAccent, _segregationTitle, _segregationSubtitle);
        StyleSection(_fullMemoryHeader, _fullMemoryAccent, _fullMemoryTitle, _fullMemorySubtitle);

        StyleWorkPanel(_companyPanel);
        StyleWorkPanel(_documentsSection);
        StyleWorkPanel(_summarySection);
        StyleWorkPanel(_memorySection);
        StyleWorkPanel(_segregationSection);
        StyleWorkPanel(_fullMemorySection);

        _companyTable.BackColor = Theme.PanelBack;
        _summaryTable.BackColor = Theme.PanelBack;
        _memoryDetails.BackColor = Theme.PanelBack;
        _documentsCommands.BackColor = Theme.PanelBack;

        _lblCompanyInfo.BackColor = Theme.HeaderBack;
        _lblCompanyInfo.ForeColor = Theme.Text;
        _lblCompanyInfo.BorderStyle = BorderStyle.FixedSingle;
        _lblCompanyInfo.Font = Theme.UiFont(8.1F);

        _pgdasNotice.BackColor = Theme.HeaderBack;
        _pgdasNotice.ForeColor = Theme.Text;
        _pgdasNotice.BorderStyle = BorderStyle.FixedSingle;
        _pgdasNotice.Font = Theme.UiFont(8.2F);

        _txtFullMemory.BackColor = Color.White;
        _txtFullMemory.ForeColor = Theme.Text;
        _lstMemory.BackColor = Color.White;
        _lstMemory.ForeColor = Theme.Text;

        _memoryTabs.Font = Theme.UiFont(8.2F);

        _status.BackColor = Color.FromArgb(247, 239, 207);
        _status.Renderer = Theme.ToolRenderer;
        _status.Font = Theme.UiFont(8F);
        _status.SizingGrip = false;
        _stEngine.ForeColor = Theme.Text;
        _stClock.ForeColor = Theme.Text;

        StyleInput(_txtCorporateName);
        StyleInput(_txtMunicipality);
        StyleInput(_txtFactorR);
        StyleInput(_txtSearch);
        StyleInput(_txtCnpj);
        StyleInput(_cboTaxRegime);
        StyleInput(_dtCompetence);
        StyleInput(_numRbt12);
        StyleInput(_numPayroll);

        _splitDocuments.BackColor = Theme.AppBack;
        _splitMemory.BackColor = Theme.Border;
    }

    private static void StyleWorkPanel(Panel panel)
    {
        panel.BackColor = Theme.PanelBack;
        panel.BorderStyle = BorderStyle.Fixed3D;
    }

    private static void StyleInput(Control control)
    {
        control.Font = Theme.UiFont(8.3F);
        control.BackColor = Color.White;
        control.ForeColor = Theme.Text;
    }

    private static void StyleSection(Panel header, Panel accent, Label title, Label subtitle)
    {
        header.BackColor = Theme.HeaderBack;
        header.BorderStyle = BorderStyle.None;
        accent.BackColor = Theme.Primary;
        title.ForeColor = Theme.PrimaryDark;
        title.Font = Theme.UiFont(8.8F, FontStyle.Bold);
        subtitle.ForeColor = Theme.Muted;
        subtitle.Font = Theme.UiFont(7.5F);
    }

    // Mantido para compatibilidade caso o DrawMode seja alterado manualmente no Designer.
    private void DrawMainTab(object? sender, DrawItemEventArgs e)
    {
        if (_tabs.DrawMode != TabDrawMode.OwnerDrawFixed)
            return;

        if (e.Index < 0 || e.Index >= _tabs.TabPages.Count)
            return;

        var selected = e.Index == _tabs.SelectedIndex;
        var rect = e.Bounds;

        using var background = new SolidBrush(selected ? Theme.HeaderBack : Theme.ToolbarBack);
        e.Graphics.FillRectangle(background, rect);
        e.Graphics.DrawRectangle(Pens.Gray, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

        using var font = Theme.UiFont(8.3F, selected ? FontStyle.Bold : FontStyle.Regular);
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
        grid.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Sel.", Width = 38, ReadOnly = true });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Documento", Width = 100 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Emissão", Width = 80 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tomador", Width = 190 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "CNPJ/CPF", Width = 125 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Município", Width = 135 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cód. Serv.", Width = 82 });
        grid.Columns.Add(MoneyColumn("Valor Serviço (R$)", 118));
        grid.Columns.Add(MoneyColumn("Base ISS (R$)", 108));
        grid.Columns.Add(MoneyColumn("ISS Retido (R$)", 112));
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Anexo", Width = 58 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Segregação", Width = 205 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Situação", Width = 120 });
    }

    private void ConfigureSegregationGrid()
    {
        _gridSegregation.Columns.Clear();
        _gridSegregation.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Anexo", Width = 90 });
        _gridSegregation.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Segregação PGDAS-D", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        _gridSegregation.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Documentos",
            Width = 105,
            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        _gridSegregation.Columns.Add(MoneyColumn("Valor (R$)", 145));
    }

    private void ConfigurePgdasGrid()
    {
        _gridPgdas.Columns.Clear();
        _gridPgdas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mercado", Width = 125 });
        _gridPgdas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Anexo", Width = 80 });
        _gridPgdas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Segregação", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        _gridPgdas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ISS", Width = 110 });
        _gridPgdas.Columns.Add(MoneyColumn("Receita (R$)", 150));
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
