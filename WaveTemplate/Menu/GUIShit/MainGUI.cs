using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using Photon.Pun;
using StupidTemplate.Classes;
using StupidTemplate.Menu;

namespace GTagHUD
{
    [BepInPlugin("com.boxxin.gui", "sryxgui", "1.0.0")]
    public class GTagHUDPlugin : BaseUnityPlugin
    {
        private ConfigEntry<int> _fontSize;
        private ConfigEntry<string> _color1;
        private ConfigEntry<string> _color2;

        private float _fps;
        private float _fpsTimer;
        private int _frameCount;
        private string _timeString = "00:00:00";
        private int _ping;
        private int _pingTimer;
        private float _colorTimer;

        private GUIStyle _labelStyle;
        private GUIStyle _titleStyle;
        private GUIStyle _shadowStyle;
        private bool _stylesInit;

        private void Awake()
        {
            _fontSize = Config.Bind("Display", "FontSize", 30, "HUD font size");
            _color1 = Config.Bind("Display", "Color1", "FF6B35", "First title color (hex, no #)");
            _color2 = Config.Bind("Display", "Color2", "FFDD00", "Second title color (hex, no #)");
        }

        private void Update()
        {
            _colorTimer += Time.unscaledDeltaTime;

            _frameCount++;
            _fpsTimer += Time.unscaledDeltaTime;
            if (_fpsTimer >= 0.5f)
            {
                _fps = _frameCount / _fpsTimer;
                _frameCount = 0;
                _fpsTimer = 0f;
            }

            var now = System.DateTime.Now;
            _timeString = $"{now.Hour:D2}:{now.Minute:D2}:{now.Second:D2}";

            _pingTimer++;
            if (_pingTimer >= 120)
            {
                _pingTimer = 0;
                _ping = PhotonNetwork.IsConnected ? PhotonNetwork.GetPing() : 0;
            }
        }

        private void InitStyles()
        {
            if (_stylesInit) return;
            _stylesInit = true;

            _titleStyle = new GUIStyle
            {
                fontSize = _fontSize.Value + 16,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Themes.List[Main.ThemeValue].Colors[1] }
            };

            _shadowStyle = new GUIStyle
            {
                fontSize = _fontSize.Value,
                fontStyle = FontStyle.Bold,
                normal = { textColor = new Color(0f, 0f, 0f, 0.6f) }
            };

            _labelStyle = new GUIStyle
            {
                fontSize = _fontSize.Value,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.white }
            };
        }

        private void OnGUI()
        {
            InitStyles();

            _titleStyle.fontSize = _fontSize.Value + 30;
            _labelStyle.fontSize = _fontSize.Value;
            _shadowStyle.fontSize = _fontSize.Value;

            float x = 12f;
            float y = 22f;
            float lineH = _fontSize.Value + 22f;
            float shadow = 1.5f;

            Color c1 = Themes.List[Main.ThemeValue].Colors[0];
            Color c2 = Themes.List[Main.ThemeValue].Colors[1];
            float t = (Mathf.Sin(_colorTimer * 2f) + 1f) / 2f;
            _titleStyle.normal.textColor = Color.Lerp(c1, c2, t);
            DrawStyled(x, y, "Sryx " + StupidTemplate.PluginInfo.Version, shadow, _titleStyle);
            y += _fontSize.Value + 42f;

            _labelStyle.normal.textColor = Color.white;
            DrawLine(x, y, $"Time  {_timeString}", shadow);
            y += lineH;

            int fpsInt = Mathf.RoundToInt(_fps);
            _labelStyle.normal.textColor = FPSColor(fpsInt);
            DrawLine(x, y, $"FPS  {fpsInt}", shadow);
            y += lineH;

            _labelStyle.normal.textColor = Color.white;
            DrawLine(x, y, $"Ping  {_ping} ms", shadow);
        }

        private void DrawLine(float x, float y, string text, float shadow)
            => DrawStyled(x, y, text, shadow, _labelStyle);

        private void DrawStyled(float x, float y, string text, float shadow, GUIStyle style)
        {
            GUI.Label(new Rect(x, y, 400f, 60f), text, style);
        }

        private Color HexToColor(string hex)
        {
            hex = hex.TrimStart('#');
            if (hex.Length < 6) return Color.white;
            byte r = System.Convert.ToByte(hex.Substring(0, 2), 16);
            byte g = System.Convert.ToByte(hex.Substring(2, 2), 16);
            byte b = System.Convert.ToByte(hex.Substring(4, 2), 16);
            return new Color(r / 255f, g / 255f, b / 255f);
        }

        private Color FPSColor(int fps)
        {
            if (fps >= 60) return new Color(0.37f, 0.91f, 0.48f);
            if (fps >= 40) return new Color(0.95f, 0.79f, 0.30f);
            return new Color(0.92f, 0.34f, 0.34f);
        }
    }
}