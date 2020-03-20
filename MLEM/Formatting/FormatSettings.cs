using Microsoft.Xna.Framework;
using MLEM.Misc;

namespace MLEM.Formatting {
    public class FormatSettings : GenericDataHolder {

        public static readonly FormatSettings Default = new FormatSettings();

        public float WobbleModifier = 5;
        public float WobbleHeightModifier = 1 / 8F;
        public float TypingSpeed = 20;
        public Color DropShadowColor = Color.Black;
        public Vector2 DropShadowOffset = new Vector2(2);

    }
}