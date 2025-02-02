using System.Globalization;
using Microsoft.Xna.Framework;

namespace MLEM.Extensions {
    /// <summary>
    /// A set of extensions for dealing with <see cref="Color"/> objects.
    /// </summary>
    public static class ColorExtensions {

        /// <summary>
        /// Copies the alpha value from another color into this color and returns the result.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="other">The color to copy the alpha from.</param>
        /// <returns>The first color with the second color's alpha value.</returns>
        public static Color CopyAlpha(this Color color, Color other) {
            return color * (other.A / 255F);
        }

        /// <summary>
        /// Returns an inverted version of this color.
        /// </summary>
        /// <param name="color">The color to invert.</param>
        /// <returns>The inverted color.</returns>
        public static Color Invert(this Color color) {
            return new Color(255 - color.R, 255 - color.G, 255 - color.B, color.A);
        }

        /// <summary>
        /// Multiplies this color with another color and returns the result.
        /// </summary>
        /// <param name="color">The first color.</param>
        /// <param name="other">The second color.</param>
        /// <returns>The two colors multiplied together.</returns>
        public static Color Multiply(this Color color, Color other) {
            return new Color(color.ToVector4() * other.ToVector4());
        }

    }

    /// <summary>
    /// A set of utility methods for dealing with <see cref="Color"/> objects.
    /// </summary>
    public static class ColorHelper {

        /// <summary>
        /// Parses a hexadecimal number into an rgba color.
        /// The number should be in the format <c>0xaarrggbb</c>.
        /// </summary>
        /// <param name="value">The number to parse.</param>
        /// <returns>The resulting color.</returns>
        public static Color FromHexRgba(int value) {
            return new Color(value >> 16 & 0xFF, value >> 8 & 0xFF, value >> 0 & 0xFF, value >> 24 & 0xFF);
        }

        /// <summary>
        /// Parses a hexadecimal number into an rgb color with 100% alpha.
        /// The number should be in the format <c>0xrrggbb</c>.
        /// </summary>
        /// <param name="value">The number to parse.</param>
        /// <returns>The resulting color.</returns>
        public static Color FromHexRgb(int value) {
            return new Color(value >> 16 & 0xFF, value >> 8 & 0xFF, value >> 0 & 0xFF);
        }

        /// <summary>
        /// Parses a hexadecimal string into a color.
        /// The string can either be formatted as RRGGBB or AARRGGBB and can optionally start with a <c>#</c>.
        /// </summary>
        /// <param name="value">The string to parse.</param>
        /// <returns>The resulting color.</returns>
        public static Color FromHexString(string value) {
            if (value.StartsWith("#"))
                value = value.Substring(1);
            var val = int.Parse(value, NumberStyles.HexNumber);
            return value.Length > 6 ? ColorHelper.FromHexRgba(val) : ColorHelper.FromHexRgb(val);
        }

    }
}
