using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using System;
using System.Drawing;

namespace MMR.Randomizer.Extensions;

/// <summary>
/// Extensions for <see cref="ColorSpaceConverter"/>.
/// </summary>
public static class ColorSpaceConverterExtensions
{
    /// <summary>
    /// Translate a given <see cref="Color"/> as <see cref="Hsv"/> values.
    /// </summary>
    /// <param name="color">Source <see cref="Color"/>.</param>
    /// <param name="func">Translate function.</param>
    /// <returns>Translated <see cref="Color"/>.</returns>
    public static Color TranslateHsv(this ColorSpaceConverter converter, Color color, Func<Hsv, Hsv> func)
    {
        var rgb = ToRgb(color);
        Hsv hsv = ColorSpaceConverter.ToHsv(rgb);
        Hsv adjusted = func(hsv);
        Rgb newRgb = ColorSpaceConverter.ToRgb(adjusted);

        return ToColor(newRgb, color.A);
    }

    /// <summary>
    /// Convert <see cref="Rgb"/> to <see cref="Color"/>.
    /// </summary>
    /// <param name="rgb">Source <see cref="Rgb"/>.</param>
    /// <param name="alpha">Alpha</param>
    /// <returns>Converted <see cref="Color"/>.</returns>
    public static Color ToColor(this ColorSpaceConverter converter, Rgb rgb, byte alpha = 255)
    {
        return ToColor(rgb, alpha);
    }

    /// <summary>
    /// Convert <see cref="Hsv"/> to <see cref="Color"/>.
    /// </summary>
    /// <param name="hsv">Source <see cref="Hsv"/>.</param>
    /// <param name="alpha">Alpha</param>
    /// <returns>Converted <see cref="Color"/>.</returns>
    public static Color ToColor(this ColorSpaceConverter converter, Hsv hsv, byte alpha = 255)
    {
        Rgb rgb = ColorSpaceConverter.ToRgb(hsv);
        return ToColor(rgb, alpha);
    }

    /// <summary>
    /// Convert <see cref="Color"/> to <see cref="Hsv"/>.
    /// </summary>
    /// <param name="color">Source <see cref="Color"/>.</param>
    /// <returns>Converted <see cref="Hsv"/>.</returns>
    public static Hsv ToHsv(this ColorSpaceConverter converter, Color color)
    {
        Rgb rgb = ToRgb(color);
        return ColorSpaceConverter.ToHsv(rgb);
    }

    /// <summary>
    /// Convert <see cref="Color"/> to <see cref="Rgb"/>.
    /// </summary>
    /// <param name="color">Source <see cref="Color"/>.</param>
    /// <returns>Converted <see cref="Rgb"/>.</returns>
    public static Rgb ToRgb(this ColorSpaceConverter converter, Color color)
    {
        return ToRgb(color);
    }

    static Color ToColor(Rgb rgb, byte alpha = 255)
    {
        byte r = (byte)(ClampFloat(rgb.R) * 255f);
        byte g = (byte)(ClampFloat(rgb.G) * 255f);
        byte b = (byte)(ClampFloat(rgb.B) * 255f);

        return Color.FromArgb(r, g, b, alpha);
    }

    static Rgb ToRgb(Color color)
    {
        return new Rgb(color.R / 255f, color.G / 255f, color.B / 255f);
    }

    private static float ClampFloat(float v)
    {
        if (v < 0f) return 0f;
        if (v > 1f) return 1f;
        return v;
    }
}
