using System.ComponentModel;

namespace Northfield.Fiscal.Desktop.Controls;

public class NorthfieldPanel : Panel
{
    private Color _borderColor = Color.FromArgb(190, 196, 202);

    public NorthfieldPanel()
    {
        BackColor = Theme.PanelBack;
        Padding = new Padding(1);
        SetStyle(ControlStyles.ResizeRedraw, true);
    }

    [DefaultValue(typeof(Color), "190, 196, 202")]
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            _borderColor = value;
            Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using var pen = new Pen(_borderColor);
        e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
    }
}
