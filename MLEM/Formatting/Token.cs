using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Font;
using MLEM.Formatting.Codes;
using MLEM.Misc;

namespace MLEM.Formatting {
    /// <summary>
    /// A part of a <see cref="TokenizedString"/> that has a certain list of formatting codes applied.
    /// </summary>
    public class Token : GenericDataHolder {

        /// <summary>
        /// The formatting codes that are applied on this token.
        /// </summary>
        public readonly Code[] AppliedCodes;
        /// <summary>
        /// The index in the <see cref="Substring"/> that this token starts at.
        /// </summary>
        public readonly int Index;
        /// <summary>
        /// The index in the <see cref="RawSubstring"/> that this token starts at.
        /// </summary>
        public readonly int RawIndex;
        /// <summary>
        /// The substring that this token contains.
        /// </summary>
        public readonly string Substring;
        /// <summary>
        /// The string that is displayed by this token. If the tokenized string has been <see cref="TokenizedString.Split"/> or <see cref="TokenizedString.Truncate"/> has been used, this string will contain the newline characters.
        /// </summary>
        public string DisplayString => this.ModifiedSubstring ?? this.Substring;
        /// <summary>
        /// The <see cref="DisplayString"/>, but split at newline characters
        /// </summary>
        public string[] SplitDisplayString { get; internal set; }
        /// <summary>
        /// The substring that this token contains, without the formatting codes removed.
        /// </summary>
        public readonly string RawSubstring;
        internal RectangleF[] Area;
        internal string ModifiedSubstring;
        internal float[] InnerOffsets;

        internal Token(Code[] appliedCodes, int index, int rawIndex, string substring, string rawSubstring) {
            this.AppliedCodes = appliedCodes;
            this.Index = index;
            this.RawIndex = rawIndex;
            this.Substring = substring;
            this.RawSubstring = rawSubstring;
        }

        /// <summary>
        /// Get the color that this token will be rendered with
        /// </summary>
        /// <param name="defaultPick">The default color, if none is specified</param>
        /// <returns>The color to render with</returns>
        public Color GetColor(Color defaultPick) {
            foreach (var code in this.AppliedCodes) {
                var color = code.GetColor(defaultPick);
                if (color.HasValue)
                    return color.Value;
            }
            return defaultPick;
        }

        /// <summary>
        /// Get the font that this token will be rendered with
        /// </summary>
        /// <param name="defaultPick">The default font, if none is specified</param>
        /// <returns>The font to render with</returns>
        public GenericFont GetFont(GenericFont defaultPick) {
            foreach (var code in this.AppliedCodes) {
                var font = code.GetFont(defaultPick);
                if (font != null)
                    return font;
            }
            return defaultPick;
        }

        /// <summary>
        /// Returns the width of the token itself, including all of the <see cref="Code"/> instances that this token contains.
        /// Note that this method does not return the width of this token's <see cref="DisplayString"/>, but only the width that the codes themselves take up.
        /// </summary>
        /// <param name="font">The font to use for calculating the width.</param>
        /// <returns>The width of this token itself.</returns>
        public float GetSelfWidth(GenericFont font) {
            var ret = 0F;
            foreach (var code in this.AppliedCodes)
                ret += code.GetSelfWidth(font);
            return ret;
        }

        /// <summary>
        /// Draws the token itself, including all of the <see cref="Code"/> instances that this token contains.
        /// Note that, to draw the token's actual string, <see cref="DrawCharacter"/> is used.
        /// </summary>
        /// <param name="time">The time</param>
        /// <param name="batch">The sprite batch to use</param>
        /// <param name="pos">The position to draw the token at</param>
        /// <param name="font">The font to use to draw</param>
        /// <param name="color">The color to draw with</param>
        /// <param name="scale">The scale to draw at</param>
        /// <param name="depth">The depth to draw at</param>
        public void DrawSelf(GameTime time, SpriteBatch batch, Vector2 pos, GenericFont font, Color color, float scale, float depth) {
            foreach (var code in this.AppliedCodes)
                code.DrawSelf(time, batch, this, pos, font, color, scale, depth);
        }

        /// <summary>
        /// Draws a given code point using this token's formatting options.
        /// </summary>
        /// <param name="time">The time</param>
        /// <param name="batch">The sprite batch to use</param>
        /// <param name="codePoint">The code point of the character to draw</param>
        /// <param name="character">The string representation of the character to draw</param>
        /// <param name="indexInToken">The index within this token that the character is at</param>
        /// <param name="pos">The position to draw the token at</param>
        /// <param name="font">The font to use to draw</param>
        /// <param name="color">The color to draw with</param>
        /// <param name="scale">The scale to draw at</param>
        /// <param name="depth">The depth to draw at</param>
        public void DrawCharacter(GameTime time, SpriteBatch batch, int codePoint, string character, int indexInToken, Vector2 pos, GenericFont font, Color color, float scale, float depth) {
            foreach (var code in this.AppliedCodes) {
                if (code.DrawCharacter(time, batch, codePoint, character, this, indexInToken, ref pos, font, ref color, ref scale, depth))
                    return;
            }

            // if no code drew, we have to do it ourselves
            font.DrawString(batch, character, pos, color, 0, Vector2.Zero, scale, SpriteEffects.None, depth);
        }

        /// <summary>
        /// Gets a list of rectangles that encompass this token's area.
        /// This can be used to invoke events when the mouse is hovered over the token, for example.
        /// </summary>
        /// <param name="stringPos">The position that the string is drawn at</param>
        /// <param name="scale">The scale that the string is drawn at</param>
        /// <returns>A set of rectangles that this token contains</returns>
        public IEnumerable<RectangleF> GetArea(Vector2 stringPos, float scale) {
            return this.Area.Select(a => new RectangleF(stringPos + a.Location * scale, a.Size * scale));
        }

    }
}
