using System.ComponentModel;

namespace Northfield.Fiscal.Desktop.Controls;

[DefaultEvent(nameof(TextChanged))]
public class NorthfieldSearchBox : UserControl
{
    private readonly TextBox _input;
    private readonly Button _clearButton;
    private bool _focused;

    public NorthfieldSearchBox()
    {
        BackColor = Color.White;
        Font = Theme.UiFont(8.4F);
        ForeColor = Theme.Text;
        MinimumSize = new Size(120, 26);
        Size = new Size(320, 26);
        Padding = new Padding(26, 4, 25, 3);
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.UserPaint, true);

        _input = new TextBox
        {
            BorderStyle = BorderStyle.None,
            Dock = DockStyle.Fill,
            Font = Theme.UiFont(8.4F),
            ForeColor = Theme.Text,
            BackColor = Color.White,
            Margin = Padding.Empty
        };

        _clearButton = new Button
        {
            Dock = DockStyle.Right,
            Width = 24,
            Text = "×",
            Font = Theme.UiFont(9F),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.White,
            ForeColor = Theme.Muted,
            Cursor = Cursors.Hand,
            TabStop = false,
            Visible = false
        };
        _clearButton.FlatAppearance.BorderSize = 0;
        _clearButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 239, 239);
        _clearButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(228, 228, 228);

        Controls.Add(_input);
        Controls.Add(_clearButton);

        _input.TextChanged += (_, _) =>
        {
            _clearButton.Visible = _input.TextLength > 0;
            base.OnTextChanged(EventArgs.Empty);
        };
        _input.Enter += (_, _) => { _focused = true; Invalidate(); };
        _input.Leave += (_, _) => { _focused = false; Invalidate(); };
        _clearButton.Click += (_, _) =>
        {
            _input.Clear();
            _input.Focus();
        };
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
        get => _input.Text;
        set => _input.Text = value ?? string.Empty;
    }

    [Category("Appearance")]
    [DefaultValue("")]
    public string PlaceholderText
    {
        get => _input.PlaceholderText;
        set => _input.PlaceholderText = value ?? string.Empty;
    }

    public void Clear() => _input.Clear();

    public void SelectAll() => _input.SelectAll();

    protected override void OnEnter(EventArgs e)
    {
        base.OnEnter(e);
        _input.Focus();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var border = _focused ? Theme.Primary : Theme.BorderSoft;
        using var borderPen = new Pen(border);
        e.Graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);

        var iconColor = _focused ? Theme.PrimaryDark : Theme.Muted;
        using var iconPen = new Pen(iconColor, 1.4F);
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        e.Graphics.DrawEllipse(iconPen, 8, 7, 8, 8);
        e.Graphics.DrawLine(iconPen, 15, 14, 19, 18);
    }
}
