using System.ComponentModel;

namespace Northfield.Fiscal.Desktop.Controls;

public sealed class NorthfieldButton : Button
{
    private bool _primary;
    private bool _hovered;
    private bool _pressed;

    public NorthfieldButton()
    {
        AutoSize = false;
        Cursor = Cursors.Hand;
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 1;
        Font = Theme.UiFont(8.3F);
        Height = 30;
        Padding = new Padding(8, 0, 8, 0);
        UseVisualStyleBackColor = false;
        ApplyVisualState();
    }

    [DefaultValue(false)]
    public bool Primary
    {
        get => _primary;
        set
        {
            if (_primary == value) return;
            _primary = value;
            Font = Theme.UiFont(8.3F, _primary ? FontStyle.Bold : FontStyle.Regular);
            ApplyVisualState();
        }
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        _hovered = true;
        ApplyVisualState();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        _hovered = false;
        _pressed = false;
        ApplyVisualState();
    }

    protected override void OnMouseDown(MouseEventArgs mevent)
    {
        base.OnMouseDown(mevent);
        if (mevent.Button == MouseButtons.Left)
        {
            _pressed = true;
            ApplyVisualState();
        }
    }

    protected override void OnMouseUp(MouseEventArgs mevent)
    {
        base.OnMouseUp(mevent);
        _pressed = false;
        ApplyVisualState();
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        ApplyVisualState();
    }

    private void ApplyVisualState()
    {
        ForeColor = Enabled ? Theme.Text : SystemColors.GrayText;

        if (!Enabled)
        {
            BackColor = Color.FromArgb(239, 239, 239);
            FlatAppearance.BorderColor = Color.FromArgb(205, 205, 205);
            return;
        }

        if (_primary)
        {
            BackColor = _pressed
                ? Color.FromArgb(194, 211, 226)
                : _hovered
                    ? Color.FromArgb(208, 222, 235)
                    : Color.FromArgb(218, 228, 237);

            FlatAppearance.BorderColor = _hovered || _pressed
                ? Theme.Primary
                : Color.FromArgb(159, 177, 194);
        }
        else
        {
            BackColor = _pressed
                ? Color.FromArgb(218, 218, 218)
                : _hovered
                    ? Color.FromArgb(230, 230, 230)
                    : Color.FromArgb(238, 238, 238);

            FlatAppearance.BorderColor = _hovered || _pressed
                ? Color.FromArgb(154, 154, 154)
                : Theme.BorderSoft;
        }
    }
}
