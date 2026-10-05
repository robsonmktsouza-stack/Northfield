namespace Northfield.Fiscal.Desktop.Controls;

public class NorthfieldComboBox : ComboBox
{
    public NorthfieldComboBox()
    {
        BackColor = Color.White;
        FlatStyle = FlatStyle.Flat;
        Font = Theme.UiFont(8.4F);
        ForeColor = Theme.Text;
        IntegralHeight = false;
        ItemHeight = 18;
    }
}
