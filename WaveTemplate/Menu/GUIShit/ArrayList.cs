using BepInEx;
using StupidTemplate.Classes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StupidTemplate.Menu.GUIShit
{
    [BepInPlugin("com.boxxin.arraylist", "ArrayList", "1.0.0")]
    internal class ArrayList : BaseUnityPlugin
    {
        private static Texture2D gradientTexture;
        private static Texture2D transparentTexture;
        private static List<string> modsEnabled = new List<string>();
        private static List<ModDisplay> modDisplays = new List<ModDisplay>();
        private static GUIStyle modStyle;
        private static Texture2D fillTexture;
        public static Color thatColor;
        private static Texture2D animatedGradient;
        private static float gradientOffset;
        private const int GradientWidth = 256;
        private const int GradientHeight = 32;
        private static Texture2D MakeTexture(int width, int height, Color color)
        {
            Color[] pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = color;
            Texture2D tex = new Texture2D(width, height);
            tex.SetPixels(pixels);
            tex.Apply();
            return tex;
        }
        private static Texture2D MakeGradient(Color baseColor, float timeOffset)
        {
            Texture2D tex = new Texture2D(GradientWidth, GradientHeight);
            tex.wrapMode = TextureWrapMode.Clamp;

            for (int x = 0; x < GradientWidth; x++)
            {
                float t = (float)x / GradientWidth;

                // animated wave
                float wave = Mathf.Sin((t * 6f) + timeOffset) * 0.5f + 0.5f;

                Color col = Color.Lerp(
                    baseColor * 0.6f,
                    baseColor,
                    wave
                );

                col.a = 0.28f;

                for (int y = 0; y < GradientHeight; y++)
                {
                    tex.SetPixel(x, y, col);
                }
            }

            tex.Apply();
            return tex;
        }
        private void Start()
        {
            if (modStyle == null)
            {
                modStyle = new GUIStyle
                {
                    fontSize = 22,
                    alignment = TextAnchor.MiddleRight,
                    normal = new GUIStyleState
                    {
                        textColor = Color.white
                    }
                };
            }

            fillTexture = new Texture2D(1, 1);

            transparentTexture = MakeTexture(1, 1, new Color(0, 0, 0, 0));

            RefreshModsList();
        }

        private void Update()
        {
            gradientOffset += Time.deltaTime * 2f;

            Theme theme = Themes.List[Main.ThemeValue];
            animatedGradient = MakeGradient(theme.Colors[0], gradientOffset);
            float waveSpeed = 2f;
            float waveIntensity = 0.5f;
            float waveCycle = Mathf.Sin(Time.time * waveSpeed) * waveIntensity;

            thatColor = theme.Colors[0];
            fillTexture.SetPixel(0, 0, thatColor);
            fillTexture.Apply();

            modStyle.normal.textColor = Color.white;

            RefreshModsList();

            foreach (var modDisplay in modDisplays)
            {
                modDisplay.Update();
            }
        }

        private void OnGUI()
        {
            Theme theme = Themes.List[Main.ThemeValue];

            float y = 20f;
            float rightPadding = 12f;
            float barWidth = 4f;
            float totalHeight = 0f;

            foreach (var modDisplay in modDisplays)
            {
                if (!modDisplay.IsVisible) continue;

                Vector2 size = modStyle.CalcSize(new GUIContent(modDisplay.Text));
                float x = Screen.width - size.x - 30f + modDisplay.OffsetX;

                Rect bgRect = new Rect(
                    x - 8f,
                    y - 2f,
                    size.x + 16f,
                    size.y + 4f
                );

                if (animatedGradient != null)
                {
                    float scroll = gradientOffset * 0.2f;
                    Rect uv = new Rect(scroll, 0, 1, 1);
                    GUI.DrawTextureWithTexCoords(bgRect, animatedGradient, uv);
                }

                Rect textRect = new Rect(x, y, size.x, size.y);
                GUI.Label(textRect, modDisplay.Text, modStyle);

                y += size.y + 4f;
                totalHeight = y;
            }

            Rect lineRect = new Rect(
                Screen.width - barWidth - rightPadding,
                15f,
                barWidth,
                totalHeight - 10f
            );

            GUI.color = theme.Colors[0];
            GUI.DrawTexture(lineRect, fillTexture);
            GUI.color = Color.white;
        }
        public static void RefreshModsList()
        {
            List<string> currentMods = GatherEnabledMods();
            List<string> addedMods = currentMods.Except(modsEnabled).ToList();
            List<string> removedMods = modsEnabled.Except(currentMods).ToList();

            foreach (string mod in addedMods)
            {
                var existingDisplay = modDisplays.FirstOrDefault(md => md.Text == mod);
                if (existingDisplay != null)
                {
                    existingDisplay.IsVisible = true;
                    existingDisplay.OffsetX = 300f;
                    continue;
                }

                modDisplays.Add(new ModDisplay(mod, true));
            }

            foreach (string mod in removedMods)
            {
                var display = modDisplays.FirstOrDefault(md => md.Text == mod);
                if (display != null)
                {
                    display.StartRemoval();
                }
            }

            modDisplays = modDisplays.OrderByDescending(md =>
            {
                Vector2 size = modStyle.CalcSize(new GUIContent(md.Text));
                return size.x;
            }).ToList();

            modDisplays.RemoveAll(md => !md.IsVisible);
            modsEnabled = currentMods.OrderByDescending(mod =>
            {
                Vector2 size = modStyle.CalcSize(new GUIContent(mod));
                return size.x;
            }).ToList();
        }
        private static List<string> GatherEnabledMods()
        {
            List<string> mods = new List<string>();

            foreach (var buttonPage in Buttons.buttons)
            {
                if (buttonPage == null) continue;

                foreach (var buttonHandler in buttonPage)
                {
                    if (buttonHandler != null && buttonHandler.enabled)
                    {
                        mods.Add(buttonHandler.buttonText);
                    }
                }
            }

            return mods;
        }


        private static void DrawFilledOutline(Rect rect, Color color, int thickness)
        {
            Color originalColor = GUI.color;

            for (int i = 0; i < thickness; i++)
            {
                Rect borderRect = new Rect(rect.x - i, rect.y - i, rect.width + 2 * i, rect.height + 2 * i);
                GUI.color = new Color(color.r, color.g, color.b, 0.25f - (i * 0.05f));
                GUI.DrawTexture(borderRect, fillTexture);
            }

            GUI.color = originalColor;
        }

        private class ModDisplay
        {
            public string Text { get; private set; }
            public bool IsVisible { get; set; }
            public float OffsetX { get; set; }

            private float velocityX;
            private float targetX;
            private bool isRemoving;

            private const float AnimationSpeed = 10f;

            public ModDisplay(string text, bool isVisible)
            {
                Text = text;
                IsVisible = isVisible;
                OffsetX = 300f;
                velocityX = 0f;
                targetX = 0f;
            }

            public void Update()
            {
                if (isRemoving)
                {
                    targetX = 300f;
                }

                OffsetX = Mathf.SmoothDamp(OffsetX, targetX, ref velocityX, 0.05f);

                if (isRemoving && Mathf.Abs(OffsetX - targetX) < 0.1f)
                {
                    IsVisible = false;
                }
            }

            public void StartRemoval()
            {
                isRemoving = true;
                targetX = 300f;
            }
        }
    }
}
