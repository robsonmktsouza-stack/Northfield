namespace Northfield.Fiscal.Desktop.Controls;

public class NorthfieldTextBox : TextBox
{
    public NorthfieldTextBox()
    {
        BackColor = Color.White;
        BorderStyle = BorderStyle.FixedSingle;
        Font = Theme.UiFont(8.4F);
        ForeColor = Theme.Text;
    }

    protected override void OnEnter(EventArgs e)
    {
        base.OnEnter(e);
        BackColor = Color.FromArgb(255, 255, 252);
    }

    protected override void OnLeave(EventArgs e)
    {
        base.OnLeave(e);
        BackColor = Color.White;
    }
}
