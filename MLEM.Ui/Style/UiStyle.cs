using Microsoft.Xna.Framework;
using MLEM.Font;
using MLEM.Textures;

namespace MLEM.Ui.Style {
    public class UiStyle {

        public NinePatch SelectionIndicator;
        public NinePatch ButtonTexture;
        public NinePatch ButtonHoveredTexture;
        public Color ButtonHoveredColor;
        public NinePatch PanelTexture;
        public NinePatch TextFieldTexture;
        public NinePatch TextFieldHoveredTexture;
        public Color TextFieldHoveredColor;
        public NinePatch ScrollBarBackground;
        public NinePatch ScrollBarScrollerTexture;
        public NinePatch CheckboxTexture;
        public NinePatch CheckboxHoveredTexture;
        public Color CheckboxHoveredColor;
        public TextureRegion CheckboxCheckmark;
        public NinePatch RadioTexture;
        public NinePatch RadioHoveredTexture;
        public Color RadioHoveredColor;
        public TextureRegion RadioCheckmark;
        public NinePatch TooltipBackground;
        public Color TooltipBackgroundColor;
        public NinePatch ProgressBarTexture;
        public Color ProgressBarColor;
        public Vector2 ProgressBarProgressPadding;
        public NinePatch ProgressBarProgressTexture;
        public Color ProgressBarProgressColor;
        public IGenericFont Font;
        public IGenericFont BoldFont;
        public IGenericFont ItalicFont;
        public float TextScale = 1;

    }
}