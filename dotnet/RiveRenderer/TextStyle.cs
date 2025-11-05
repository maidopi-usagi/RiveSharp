using System;

namespace RiveRenderer;

public readonly struct TextStyle
{
    public TextStyle(
        float size,
        float lineHeight = -1f,
        float letterSpacing = 0f,
        float width = -1f,
        float paragraphSpacing = 0f,
        TextAlign align = TextAlign.Left,
        TextWrap wrap = TextWrap.Wrap,
        TextDirection direction = TextDirection.Automatic)
    {
        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "Text size must be positive.");
        }

        Size = size;
        LineHeight = lineHeight;
        LetterSpacing = letterSpacing;
        Width = width;
        ParagraphSpacing = paragraphSpacing;
        Align = align;
        Wrap = wrap;
        Direction = direction;
    }

    public float Size { get; }
    public float LineHeight { get; }
    public float LetterSpacing { get; }
    public float Width { get; }
    public float ParagraphSpacing { get; }
    public TextAlign Align { get; }
    public TextWrap Wrap { get; }
    public TextDirection Direction { get; }

    internal TextStyleOptions ToNative()
    {
        return new TextStyleOptions
        {
            Size = Size,
            LineHeight = LineHeight,
            LetterSpacing = LetterSpacing,
            Width = Width,
            ParagraphSpacing = ParagraphSpacing,
            Align = Align,
            Wrap = Wrap,
            Direction = Direction,
        };
    }
}
