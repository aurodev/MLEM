using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MLEM.Font {
    public interface IGenericFont {

        float LineHeight { get; }

        Vector2 MeasureString(string text);

        Vector2 MeasureString(StringBuilder text);

        void DrawString(SpriteBatch batch, string text, Vector2 position, Color color);

        void DrawString(SpriteBatch batch, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);

        void DrawString(SpriteBatch batch, string text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth);

        void DrawString(SpriteBatch batch, StringBuilder text, Vector2 position, Color color);

        void DrawString(SpriteBatch batch, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);

        void DrawString(SpriteBatch batch, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth);

        void DrawCenteredString(SpriteBatch batch, string text, Vector2 position, float scale, Color color, bool horizontal = true, bool vertical = false, float addedScale = 0);

        string SplitString(string text, float width, float scale);

        string TruncateString(string text, float width, float scale, bool fromBack = false);

    }
}