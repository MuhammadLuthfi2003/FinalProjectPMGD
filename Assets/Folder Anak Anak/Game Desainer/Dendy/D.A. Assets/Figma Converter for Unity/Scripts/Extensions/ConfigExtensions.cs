namespace DA_Assets.FCU.Extensions
{
    public static class ConfigExtensions
    {
        public static bool UsingDaButton(this FigmaConverterUnity fcu) =>
            fcu.Settings.ComponentSettings.ButtonComponent == ButtonComponent.DAButton;
        public static bool UsingTrueShadow(this FigmaConverterUnity fcu) =>
            fcu.Settings.ComponentSettings.ShadowComponent == ShadowComponent.TrueShadow;

        public static bool UsingUnityText(this FigmaConverterUnity fcu) =>
            fcu.Settings.ComponentSettings.TextComponent == TextComponent.UnityText;
        public static bool UsingTextMeshPro(this FigmaConverterUnity fcu) =>
            fcu.Settings.ComponentSettings.TextComponent == TextComponent.TextMeshPro;
        public static bool UsingShapes2D(this FigmaConverterUnity fcu) =>
            fcu.Settings.ComponentSettings.ImageComponent == ImageComponent.Shape;
        public static bool UsingUnityImage(this FigmaConverterUnity fcu) =>
            fcu.Settings.ComponentSettings.ImageComponent == ImageComponent.UnityImage;
        public static bool UsingPUI(this FigmaConverterUnity fcu) =>
            fcu.Settings.ComponentSettings.ImageComponent == ImageComponent.ProceduralImage;
        public static bool UsingMPUIKit(this FigmaConverterUnity fcu) =>
            fcu.Settings.ComponentSettings.ImageComponent == ImageComponent.MPImage;
    }
}