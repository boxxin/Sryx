using femboy.Classes;
using StupidTemplate.Classes;
using StupidTemplate.Menu;
using TMPro;
using UnityEngine;

namespace StupidTemplate
{
    public class Settings
    {
        public static void ChangeTheme(bool forward)
        {
            int direction = forward ? 1 : -1;
            Main.ThemeValue = (Main.ThemeValue + direction + Themes.List.Length) % Themes.List.Length;

            Main.GetIndex("Change Theme").overlapText = "Change Theme: " + Themes.List[Main.ThemeValue].Name;
        }
        public static void ChangeScale(bool forward)
        {
            Settings.menuScaleMulti += forward ? 0.1f : -0.1f;
            Main.GetIndex("Change Menu Scale").overlapText = "Change Menu Scale: x" + Settings.menuScaleMulti;
        }
        public static Color trust = new Color(0f, 1f, 0f);
        public static ExtGradient backgroundColor = new ExtGradient { colors = ExtGradient.GetSolidGradient(new Color32(15, 15, 15, 255))};
        public static Color[] buttonColors = new Color[]
        {
            ColorLib.ShadowBlack,
            ColorLib.ShadowBlack
        };
        public static Color[] textColors = new Color[]
        {
            Color.white,
            ColorLib.ShadowBlack
        };
        public static Font currentFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font; //Font.CreateDynamicFontFromOSFont("Comic Sans MS", 24);
        public static TMP_FontAsset currentFontTMP = Resources.GetBuiltinResource(typeof(TMP_FontAsset), "Arial.ttf") as TMP_FontAsset;//GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText")?.GetComponent<TMP_Text>()?.font;

        public static bool fpsCounter = true;
        public static bool disconnectButton = true;
        public static bool rightHanded;
        public static bool disableNotifications;

        public static KeyCode keyboardButton = KeyCode.Q;

        public static float menuScale = menuScaleMulti;
        public static float menuScaleMulti = 1f;
        public static Vector3 menuSize = new Vector3(0.001f, 1f, 1.1f);
        public static int buttonsPerPage = 8;

        public static float gradientSpeed = 0.5f;
    }
}
