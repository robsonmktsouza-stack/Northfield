namespace Northfield.Fiscal.Desktop.Controls;

public class NorthfieldGrid : DataGridView
{
    public NorthfieldGrid()
    {
        ApplyNorthfieldStyle();
    }

    public void ApplyNorthfieldStyle()
    {
        BackgroundColor = Color.White;
        BorderStyle = BorderStyle.None;
        GridColor = Color.FromArgb(216, 220, 224);
        CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        RowHeadersVisible = false;
        AllowUserToAddRows = false;
        AllowUserToDeleteRows = false;
        AllowUserToResizeRows = false;
        SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        MultiSelect = false;
        ReadOnly = true;
        AutoGenerateColumns = false;
        ColumnHeadersHeight = 31;
        ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        RowTemplate.Height = 29;
        EnableHeadersVisualStyles = false;

        ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(228, 232, 236);
        ColumnHeadersDefaultCellStyle.ForeColor = Theme.Text;
        ColumnHeadersDefaultCellStyle.Font = Theme.UiFont(8.2F, FontStyle.Bold);
        ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(228, 232, 236);
        ColumnHeadersDefaultCellStyle.SelectionForeColor = Theme.Text;
        ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 6, 0);

        DefaultCellStyle.BackColor = Color.White;
        DefaultCellStyle.ForeColor = Theme.Text;
        DefaultCellStyle.Font = Theme.UiFont(8.25F);
        DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 225, 235);
        DefaultCellStyle.SelectionForeColor = Theme.Text;
        DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);

        AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 249, 249);
    }
}
