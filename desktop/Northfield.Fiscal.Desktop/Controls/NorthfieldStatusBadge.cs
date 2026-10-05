using System.ComponentModel;

namespace Northfield.Fiscal.Desktop.Controls;

public enum NorthfieldBadgeTone
{
    Neutral,
    Success,
    Warning,
    Error
}

public class NorthfieldStatusBadge : Label
{
    private NorthfieldBadgeTone _tone = NorthfieldBadgeTone.Neutral;

    public NorthfieldStatusBadge()
    {
        AutoSize = true;
        Font = Theme.UiFont(7.9F, FontStyle.Bold);
        Padding = new Padding(8, 3, 8, 3);
        TextAlign = ContentAlignment.MiddleCenter;
        ApplyTone();
    }

    [DefaultValue(NorthfieldBadgeTone.Neutral)]
    public NorthfieldBadgeTone Tone
    {
        get => _tone;
        set
        {
            if (_tone == value) return;
            _tone = value;
            ApplyTone();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using var pen = new Pen(GetBorderColor());
        e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
    }

    private void ApplyTone()
    {
        (BackColor, ForeColor) = _tone switch
        {
            NorthfieldBadgeTone.Success => (Theme.SuccessSoft, Theme.Success),
            NorthfieldBadgeTone.Warning => (Theme.WarningSoft, Theme.Warning),
            NorthfieldBadgeTone.Error => (Color.FromArgb(247, 229, 229), Theme.Error),
            _ => (Color.FromArgb(232, 234, 236), Theme.Muted)
        };

        Invalidate();
    }

    private Color GetBorderColor() => _tone switch
    {
        NorthfieldBadgeTone.Success => Color.FromArgb(170, 207, 181),
        NorthfieldBadgeTone.Warning => Color.FromArgb(222, 202, 146),
        NorthfieldBadgeTone.Error => Color.FromArgb(220, 170, 170),
        _ => Color.FromArgb(198, 202, 206)
    };
}
