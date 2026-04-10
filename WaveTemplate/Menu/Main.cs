using BepInEx;
using GorillaLocomotion;
using HarmonyLib;
using Photon.Pun;
using StupidTemplate.Classes;
using StupidTemplate.Mods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static StupidTemplate.Menu.Buttons;
using static StupidTemplate.Settings;
using Debug = UnityEngine.Debug;
using Text = UnityEngine.UI.Text;

namespace StupidTemplate.Menu
{
    [HarmonyPatch(typeof(GTPlayer), "LateUpdate")]
    public class Main : MonoBehaviour
    {
        #region vars
        public static bool outlining = true;
        public static bool outliningButtons = true;
        public static bool sidebarenable = false;
        public static bool scintillawidemenu = false;
        public static bool L_0 = false;
        public static bool ww = false;
        public static float tett = 0.2f;
        public static bool opensound = true;
        public static bool sidebuttons = false;
        public static float SmFl = 0.0035f;
        public static int BtnIndex = 0;
        public static float IncrementCooldown = 0f;
        public static float OffBrightness = 0.5f;
        public static bool memoryleak = true;
        private static float menuAnimSpeed = 8f;
        private static bool isClosing = false;
        private static bool playOpenAnimation = false;
        public static bool rounding = false;
        public static bool IsCatLeft = true;
        public static bool IsCatRotated = false;
        public static float BtnHeight = 0.07f;
        public static float BtnSpace = 0.005f;
        public static int CatIndex = 0;
        public static float TextSize = 0.5f;
        public static GameObject Sidebar;
        public static float MenuWidth = 0.8f;
        public static float BtnInset = 0.1f;
        public static float BtnUpset = 0.3f;
        public static float GradVal = 0.066f;
        public static GameObject BoardGradientObject = null;
        public static bool drop = false;
        public static bool sus = false;
        public static bool Loaded = true;
        public static int ThemeValue = 0;
        public static Material BoardMat;
        public static GameObject menu;
        public static GameObject menuBackground;
        public static GameObject reference;
        public static GameObject canvasObject;
        public static SphereCollider buttonCollider;
        public static Camera TPC;
        public static Text fpsObject;
        public static int pageNumber = 0;
        public static int pageNumber2 = 0;
        public static int _currentCategory;
        public static int _currentCategory2;
        public static int currentCategory
        {
            get => _currentCategory;
            set
            {
                _currentCategory = value;
                pageNumber = 0;
            }
        }
        public static int currentCategory2
        {
            get => _currentCategory2;
            set
            {
                _currentCategory2 = value;
                pageNumber2 = 0;
            }
        }
        #endregion
        public static void Prefix()
        {
            try
            {
                if (!Visual.eventSubscribed)
                {
                    PhotonNetwork.NetworkingClient.EventReceived += Visual.OnEvent2; // yes ik this networking is shit but no one knows my menu so i dont think anyone will make a crasher for it
                    PhotonNetwork.NetworkingClient.EventReceived += Visual.OnEvent;
                    Visual.eventSubscribed = true;
                }
                if (sidebuttons)
                {
                    buttonsPerPage = 9;
                    if (sus == false)
                    {
                        RecreateMenu();
                        sus = true;
                    }
                }
                else
                {
                    buttonsPerPage = 8;
                    if (sus == true)
                    {
                        RecreateMenu();
                        sus = false;
                    }
                }
                if (BoardGradientObject == null)
                {
                    BoardGradientObject = new GameObject("plasma");
                    UnityEngine.Object.DontDestroyOnLoad(BoardGradientObject);
                    BoardGradientObject.AddComponent<MeshRenderer>();
                    GradientSetter gradientSetter = BoardGradientObject.AddComponent<GradientSetter>();
                    gradientSetter.gradientOffset = 0f;
                }
                if (memoryleak == true)
                {
                    Visual.testlo();
                    #region
                    Debug.Log("Succesfully Loaded Plasma!\n                                                                                 @@@@                                  \r\n                                                                                %%%%%%%                                \r\n                                                                            @%%%######%%%                              \r\n                                                                          %%%%%% ######%%%%                            \r\n                                                                         %%%%%%%%%#######%%%                           \r\n                                                                      @@@@%%%%%###########%%%                          \r\n                                    #**+++=+++++++++******####%%%%%%%%%%%%%%%%%%%#####%%%%%%                           \r\n                                    **++========+++++++*****####%%%%%%%%%%%%%%%%%%##%%%%%                              \r\n                                    **++=========+++++++****####%%%%%%%%%%%%%% %%%%%%%                                 \r\n                                    #**++====+++++*******########%%%%%%%%%@@@   @@@@                                   \r\n                                                                  @@%%%%%@@@                                           \r\n                                                                 @@%%%%%%@@                                            \r\n                                                                @%%%%%%%@@                                             \r\n                                                               %%%%%%%%@@                                              \r\n                                                               %%%%%%%%%                                               \r\n                                                              %%####%%%                                                \r\n                                                             #######%%%                                                \r\n                                                            ###***##%%                                                 \r\n                                                          ###****###                                                   \r\n                                                         ##*****###                                                    \r\n                                                       ###**+***##                                                     \r\n                                                     ##**++++++**###%                                                  \r\n                                                    #**+++==+++++**###%                                                \r\n                                                  ***++===++++++++***####                                              \r\n                                                ***++===+***  *##******###%                                            \r\n                                             ****+====++**#     ###*****###%                                           \r\n                                           #***+====++***         ###*****##%%                                         \r\n                                         #**++====++***            %####***##%%%                                       \r\n                                      ##**+=====++***                ######*##%%%                                      \r\n                                   ###**++==+++***#                    %%######%%%%                                    \r\n                               ####**+++++++**##                         %%%#####%%%%                                  \r\n                            %%###****++++**###                            %%%%####%%@@                                 \r\n                             %###******####                                 %%%%%%%%%%@@                               \r\n                              %%######%%                                      %%%%%%%%@                                \r\n                                @@@%%                                          @@@@@                                   \r\n                                                                                 @");
                    #endregion
                    memoryleak = false;
                }
                HandleMenu();
                RunEnabledMods();
            }
            catch { }
            if (Loaded == true)
            {
                Loaded = false;

                if (!Directory.Exists("plasma"))
                {
                    Directory.CreateDirectory("plasma");
                }
                using (FileStream fs = File.Create(System.IO.Path.Combine("plasma/", "readme.txt")))
                {
                    string text = "im touching myself"; // im not actually

                    byte[] info = new UTF8Encoding(true).GetBytes(text);
                    fs.Write(info, 0, info.Length);
                }
            }
        }
        #region some helpers

        public static Texture2D LoadTextureFromURL(string resourcePath, string fileName) // from old willy wiper
        {
            Texture2D texture = new Texture2D(2, 2);
            if (!Directory.Exists("plasma"))
            {
                Directory.CreateDirectory("plasma");
            }
            if (!File.Exists("plasma/" + fileName))
            {
                WebClient stream = new WebClient();
                stream.DownloadFile(resourcePath, "plasma/" + fileName);
            }
            byte[] bytes = File.ReadAllBytes("plasma/" + fileName);
            texture.LoadImage(bytes);
            return texture;
        }
        public static void OutlineGradient(GameObject toOutline) // g3if <3
        {
            if (toOutline != null && menu != null)
            {
                GameObject outline = GameObject.CreatePrimitive(PrimitiveType.Cube);
                outline.transform.parent = menu.transform;
                outline.transform.rotation = Quaternion.identity;
                outline.transform.localScale = toOutline.transform.localScale - new Vector3(SmFl / 2f, 0f, 0f);
                outline.transform.localPosition = toOutline.transform.localPosition;
                outline.transform.rotation = toOutline.transform.rotation;
                GameObject.Destroy(outline.GetComponent<Rigidbody>());
                GameObject.Destroy(outline.GetComponent<BoxCollider>());
                if (Themes.List[ThemeValue].tryy == true)
                {
                    outline.AddComponent<AnimatedGradient>();
                }
                else
                {
                    GradientSetter cs1 = outline.AddComponent<GradientSetter>();
                    var sourceGradient = toOutline.GetComponent<GradientSetter>();
                    if (sourceGradient != null)
                    {
                        cs1.brightness = sourceGradient.brightness - 0.3f;
                        cs1.gradientOffset = sourceGradient.gradientOffset;
                    }
                }
                if (rounding)
                {
                    var sourceCorners = toOutline.GetComponent<RoundedCorners>();
                    if (sourceCorners != null)
                    {
                        RoundedCorners corners = outline.AddComponent<RoundedCorners>();
                        corners.bevel = sourceCorners.bevel;
                    }
                }
                toOutline.transform.localScale = toOutline.transform.localScale - new Vector3(0f, 0.01f, 0.01f);
            }
        }
        #endregion

        #region Menu Handling

        private static void HandleMenu()
        {
            try
            {
                bool controllerOpen =
                    (!rightHanded && ControllerInputPoller.instance.leftControllerSecondaryButton) ||
                    (rightHanded && ControllerInputPoller.instance.rightControllerSecondaryButton);

                bool keyboardOpen = UnityInput.Current.GetKey(keyboardButton);
                bool shouldOpen = controllerOpen || keyboardOpen;
                if (menu == null)
                {
                    if (!shouldOpen) return;
                    playOpenAnimation = true;
                    if (opensound)
                    {
                        Soundboard.PlayAudioCS(Sounds.ListOpenSounds[Sounds.soundValueOpen].soundUrlOpen, Sounds.ListOpenSounds[Sounds.soundValueOpen].wav);
                    }
                    CreateMenu();
                    RecenterMenu(rightHanded, keyboardOpen);

                    if (reference == null)
                        CreateReference(rightHanded);

                    return;
                }
                if (shouldOpen)
                {

                    RecenterMenu(rightHanded, keyboardOpen);
                    return;
                }

                CloseMenu();
            }
            catch (Exception ex)
            {
                LogError("ooo", ex);
            }
        }

        private static void CloseMenu()
        {
            if (isClosing) return;

            isClosing = true;

            GameObject.Find("Shoulder Camera")
                      .transform.Find("CM vcam1")
                      .gameObject.SetActive(true);

            float time = 0f;
            if (drop)
            {
                Rigidbody comp = menu.AddComponent<Rigidbody>();
                Vector3 velocity = rightHanded ? GorillaLocomotion.GTPlayer.Instance.RightHand.velocityTracker.GetAverageVelocity(true, 0) : GorillaLocomotion.GTPlayer.Instance.LeftHand.velocityTracker.GetAverageVelocity(true, 0);
                comp.angularVelocity = velocity;
                time = 2f;
            }
            else
            {
                GTPlayer.Instance.StartCoroutine(AnimateMenu(false));
            }
            if (opensound)
            {
                Soundboard.PlayAudioCS(Sounds.ListOpenSounds[Sounds.soundValueOpen].soundUrlClose, Sounds.ListOpenSounds[Sounds.soundValueOpen].wav);
            }
            UnityEngine.Object.Destroy(menu, time);
            menu = null;
            UnityEngine.Object.Destroy(reference);
            reference = null;
            isClosing = false;
        }

        #endregion

        #region Mod Execution
        private static void LogError(string context, Exception ex)
        {
            Debug.LogError($"{PluginInfo.Name} // {context}\n{ex}");
        }
        private static void RunEnabledMods()
        {
            try
            {
                UpdateFPSCounter();

                foreach (var button in buttons
                    .SelectMany(group => group)
                    .Where(b => b.enabled && b.method != null))
                {
                    try
                    {
                        button.method.Invoke();
                    }
                    catch (Exception ex)
                    {
                        LogError($"Mod Error: {button.buttonText}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("Mod Execution Error", ex);
            }
        }

        private static void UpdateFPSCounter()
        {
            if (fpsObject == null) return;

            float fps = Mathf.Ceil(1f / Time.unscaledDeltaTime);
            fpsObject.text = $"FPS: {fps}";
        }
        #endregion

        #region Menu Creation

        public static void CreateMenu()
        {
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menu.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menu.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);

            menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menuBackground.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menuBackground.GetComponent<BoxCollider>());
            menuBackground.transform.parent = menu.transform;
            menuBackground.transform.rotation = Quaternion.identity;
            menuBackground.transform.localScale = menuSize;
            menuBackground.transform.position = new Vector3(0.05f, 0f, 0f);
            if (Themes.List[ThemeValue].tryy2)
            {
                Renderer rend = menuBackground.GetComponent<Renderer>();
                rend.material.shader = Shader.Find("Universal Render Pipeline/Lit");
                rend.material.color = Color.white;
                rend.material.mainTexture = LoadTextureFromURL("https://pnrjyzqnbiryqqkjpghd.supabase.co/functions/v1/api-proxy/menu/images/seroxen.png", "seroxentheme.jpg");
            }
            else
            {
                GradientSetter menuBackground2 = menuBackground.AddComponent<GradientSetter>();
                if (rounding) { menuBackground.AddComponent<RoundedCorners>(); }
            }
            if (outlining) { OutlineGradient(menuBackground); }

            canvasObject = new GameObject();
            canvasObject.transform.parent = menu.transform;
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 10000f;
            if (sidebarenable)
            {
                Sidebar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Sidebar.transform.parent = menu.transform;
                Sidebar.transform.localScale = new Vector3(0.001f, 0.45f, 1.05f);
                if (sidebuttons)
                {
                    float far = IsCatLeft ? tett : -tett;
                    Sidebar.transform.localPosition = new Vector3(
                        0.45f,
                        (IsCatLeft ? -((menuBackground.transform.localScale.y / 2f) + (Sidebar.transform.localScale.y / 2f)) : ((menuBackground.transform.localScale.y / 2f) + (Sidebar.transform.localScale.y / 2f))) + (IsCatLeft ? -(SmFl * 20f) : (SmFl * 20f)) - far,
                        -0.01f);
                }
                else
                {
                    Sidebar.transform.localPosition = new Vector3(
            0.45f,
            (IsCatLeft ? -((menuBackground.transform.localScale.y / 2f) + (Sidebar.transform.localScale.y / 2f)) : ((menuBackground.transform.localScale.y / 2f) + (Sidebar.transform.localScale.y / 2f))) + (IsCatLeft ? -(SmFl * 20f) : (SmFl * 20f)),
            -0.01f);
                }
                Sidebar.transform.localRotation = Quaternion.Euler(
        0f,
        0f,
        IsCatRotated ? (IsCatLeft ? 45f : -45f) : 0f
    );

                if (Themes.List[ThemeValue].tryy2)
                {
                    Renderer rend = Sidebar.GetComponent<Renderer>();
                    rend.material.shader = Shader.Find("Universal Render Pipeline/Lit");
                    rend.material.color = Color.white;
                    rend.material.mainTexture = LoadTextureFromURL("https://pnrjyzqnbiryqqkjpghd.supabase.co/functions/v1/api-proxy/menu/images/seroxen.png", "seroxentheme.jpg");
                }
                else
                {
                    GradientSetter sidebarGradient = Sidebar.AddComponent<GradientSetter>();
                    if (rounding) Sidebar.AddComponent<RoundedCorners>();
                }
                if (outlining)
                {
                    OutlineGradient(Sidebar);
                }
                GameObject.Destroy(Sidebar.GetComponent<Rigidbody>());
                GameObject.Destroy(Sidebar.GetComponent<BoxCollider>());
                ButtonInfo[] categoryButtons = buttonsCat[currentCategory2].ToArray();
                for (int i = 0; i < categoryButtons.Length; i++)
                    CreateCategoryPanel(i * 0.1f, categoryButtons[i]);
            }
            CreateMenuText();
            CreatePageButtons();
            CreateReturnButton();
            if (disconnectButton)
            {
                CreateDisconnectButton();
            }
            ButtonInfo[] activeButtons = buttons[currentCategory].Skip(pageNumber * buttonsPerPage).Take(buttonsPerPage).ToArray();
            for (int i = 0; i < activeButtons.Length; i++)
                CreateButton(i * 0.1f, activeButtons[i]);
            if (playOpenAnimation)
            {
                GTPlayer.Instance.StartCoroutine(AnimateMenu(true));
                playOpenAnimation = false;
            }
            menu.transform.localScale = new Vector3(0.1f, L_0 ? 0.5f : 0.3f, scintillawidemenu ? 0.3f : 0.3825f) * GTPlayer.Instance.scale * menuScaleMulti;
        }
        public static void CreatePageButtons()
        {
            CreatePageButton("PreviousPage", "<",
                sidePos: new Vector3(0.52f, 0.6f, 0),
                flatPos: new Vector3(0.52f, 0.24f, -0.475f),
                textSidePos: new Vector3(0.053f, 0.18f, 0),
                textFlatPos: new Vector3(0.052f, 0.077f, -0.185f));

            CreatePageButton("NextPage", ">",
                sidePos: new Vector3(0.52f, -0.6f, 0),
                flatPos: new Vector3(0.52f, -0.24f, -0.475f),
                textSidePos: new Vector3(0.053f, -0.18f, 0),
                textFlatPos: new Vector3(0.052f, -0.077f, -0.185f));
        }

        private static void CreatePageButton(string relatedText, string label, Vector3 sidePos, Vector3 flatPos, Vector3 textSidePos, Vector3 textFlatPos)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q)) go.layer = 2;
            UnityEngine.Object.Destroy(go.GetComponent<Rigidbody>());
            go.GetComponent<BoxCollider>().isTrigger = true;
            go.transform.parent = menu.transform;
            go.transform.rotation = Quaternion.identity;
            go.AddComponent<Classes.Button>().relatedText = relatedText;

            if (sidebuttons)
            {
                go.transform.localScale = new Vector3(0.001f, 0.15f, 1.075f);
                go.transform.localPosition = sidePos;
                go.AddComponent<GradientSetter>();
                if (rounding) go.AddComponent<RoundedCorners>();
                if (outliningButtons) OutlineGradient(go);
            }
            else
            {
                go.transform.localScale = new Vector3(0.001f, 0.47f, 0.1f);
                go.transform.localPosition = flatPos;
                ApplyTransparentMaterial(go, buttonColors[0], 0.5f);
            }

            TextMeshPro text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<TextMeshPro>();
            text.font = currentFontTMP;
            text.text = label;
            text.fontSize = 1;
            text.alignment = TextAlignmentOptions.Center;
            text.enableAutoSizing = true;
            text.fontSizeMin = 0;
            text.color = Color.white;

            RectTransform rt = text.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(0.2f, 0.03f);
            rt.localPosition = sidebuttons ? textSidePos : textFlatPos;
            rt.rotation = Quaternion.Euler(180f, 90f, 90f);
        } // thanks ai
        private static void ApplyTransparentMaterial(GameObject go, Color baseColor, float alpha)
        {
            Renderer r = go.GetComponent<Renderer>();
            r.material.SetFloat("_Mode", 3f);
            r.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            r.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            r.material.SetInt("_ZWrite", 0);
            r.material.DisableKeyword("_ALPHATEST_ON");
            r.material.EnableKeyword("_ALPHABLEND_ON");
            r.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            r.material.renderQueue = 3000;
            baseColor.a = alpha;
            r.material.color = baseColor;
        }
        public static void CreateDisconnectButton()
        {
            #region button
            GameObject plusButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
                plusButton.layer = 2;
            UnityEngine.Object.Destroy(plusButton.GetComponent<Rigidbody>());
            plusButton.GetComponent<BoxCollider>().isTrigger = true;
            plusButton.transform.parent = menu.transform;
            plusButton.transform.rotation = Quaternion.identity;
            plusButton.transform.localScale = new Vector3(0.001f, 0.13f, 0.08f);
            plusButton.transform.localPosition = new Vector3(0.52f, -0.4f, 0.475f);
            plusButton.AddComponent<Classes.Button>().relatedText = "Disconnect";

            Renderer renderer = plusButton.GetComponent<Renderer>();
            renderer.material.SetFloat("_Mode", 3f);
            renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            renderer.material.SetInt("_ZWrite", 0);
            renderer.material.DisableKeyword("_ALPHATEST_ON");
            renderer.material.EnableKeyword("_ALPHABLEND_ON");
            renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            renderer.material.renderQueue = 3000;
            Color btnColor = buttonColors[0];
            btnColor.a = 0.5f;
            renderer.material.color = btnColor;
            #endregion
            #region text
            TextMeshPro text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<TextMeshPro>();
            text.font = currentFontTMP;
            text.text = "l";
            text.fontSize = 1;
            text.alignment = TextAlignmentOptions.Center;
            text.enableAutoSizing = true;
            text.fontSizeMin = 0;
            text.color = Color.red;
            RectTransform rectt = text.GetComponent<RectTransform>();
            rectt.localPosition = Vector3.zero;
            rectt.sizeDelta = new Vector2(0.1f, 0.03f);
            rectt.localPosition = new Vector3(0.052f, plusButton.transform.position.y, plusButton.transform.position.z - 0.002f);
            rectt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            #endregion
        }
        public static void CreateReturnButton()
        {
            if (currentCategory != 0)
            {
                #region button
                GameObject plusButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                    plusButton.layer = 2;
                UnityEngine.Object.Destroy(plusButton.GetComponent<Rigidbody>());
                plusButton.GetComponent<BoxCollider>().isTrigger = true;
                plusButton.transform.parent = menu.transform;
                plusButton.transform.rotation = Quaternion.identity;
                plusButton.transform.localScale = new Vector3(0.001f, 0.13f, 0.08f);
                plusButton.transform.localPosition = new Vector3(0.52f, -0.25f, 0.475f);
                plusButton.AddComponent<Classes.Button>().relatedText = "return";

                Renderer renderer = plusButton.GetComponent<Renderer>();
                renderer.material.SetFloat("_Mode", 3f);
                renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                renderer.material.SetInt("_ZWrite", 0);
                renderer.material.DisableKeyword("_ALPHATEST_ON");
                renderer.material.EnableKeyword("_ALPHABLEND_ON");
                renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                renderer.material.renderQueue = 3000;
                Color btnColor = buttonColors[0];
                btnColor.a = 0.5f;
                renderer.material.color = btnColor;
                #endregion
                #region text
                TextMeshPro text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<TextMeshPro>();
                text.font = currentFontTMP;
                text.text = "h";
                text.fontSize = 1;
                text.alignment = TextAlignmentOptions.Center;
                text.enableAutoSizing = true;
                text.fontSizeMin = 0;
                text.color = Color.red;
                RectTransform rectt = text.GetComponent<RectTransform>();
                rectt.localPosition = Vector3.zero;
                rectt.sizeDelta = new Vector2(0.1f, 0.03f);
                rectt.localPosition = new Vector3(0.052f, plusButton.transform.position.y, plusButton.transform.position.z - 0.002f);
                rectt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
                #endregion
            }
        }
        public static void CreateMenuText()
        {
            TextMeshPro text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<TextMeshPro>();
            text.font = currentFontTMP;
            text.text = L_0 ? "★彡[ʟᴇᴀɴxʟ_0]彡★" /* took straight from og l_0 */ : PluginInfo.Name;
            text.fontSize = 1;
            text.alignment = TextAlignmentOptions.Midline;
            text.enableAutoSizing = true;
            text.fontSizeMin = 0;
            text.color = Color.white;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.25f, 0.04f);
            component.position = new Vector3(.052f, 0, 0.18f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }
        public static void ttte(ButtonInfo method, Vector3 pos, TextAlignmentOptions alig = TextAlignmentOptions.Center)
        {
            TextMeshPro text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<TextMeshPro>();
            text.font = currentFontTMP;
            text.text = method.buttonText;
            if (method.overlapText != null)
                text.text = method.overlapText;
            text.fontSize = 1;
            text.alignment = alig;
            text.enableAutoSizing = true;
            text.fontSizeMin = 0;
            if (method.enabled)
            {
                text.color = textColors[1];
            }
            else
            {
                text.color = Color.white;
            }
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .02f);
            component.localPosition = pos;
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }
        public static void CreateButton(float offset, ButtonInfo method)
        {
            if (!method.justtext)
            {
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    gameObject.layer = 2;
                }
                UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
                gameObject.transform.parent = menu.transform;
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.localScale = new Vector3(0.001f, method.isIncremental ? ww ? 0.1f : 0.65f : ww ? 0.1f : 0.95f, 0.08f);
                gameObject.transform.localPosition = new Vector3(0.52f, method.isIncremental ? ww ? 0.4f : 0.15f : ww ? 0.4f : 0f, 0.34f - offset);
                Renderer renderer = gameObject.GetComponent<Renderer>();
                renderer.material.SetFloat("_Mode", 3f);
                renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                renderer.material.SetInt("_ZWrite", 0);
                renderer.material.DisableKeyword("_ALPHATEST_ON");
                renderer.material.EnableKeyword("_ALPHABLEND_ON");
                renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                renderer.material.renderQueue = 3000;
                Color btnColor = method.enabled ? buttonColors[1] : buttonColors[0];
                btnColor.a = 0.5f;
                renderer.material.color = btnColor;
                gameObject.AddComponent<Classes.Button>().relatedText = method.buttonText;
                if (method.isIncremental)
                {
                    void ApplyTransparentMaterial(Renderer r, Color c)
                    {
                        r.material.SetFloat("_Mode", 3f);
                        r.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        r.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        r.material.SetInt("_ZWrite", 0);
                        r.material.DisableKeyword("_ALPHATEST_ON");
                        r.material.EnableKeyword("_ALPHABLEND_ON");
                        r.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        r.material.renderQueue = 3000;
                        Color col = c;
                        col.a = 0.5f;
                        r.material.color = col;
                    }

                    GameObject plusButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    if (!UnityInput.Current.GetKey(KeyCode.Q))
                        plusButton.layer = 2;
                    UnityEngine.Object.Destroy(plusButton.GetComponent<Rigidbody>());
                    plusButton.GetComponent<BoxCollider>().isTrigger = true;
                    plusButton.transform.parent = menu.transform;
                    plusButton.transform.rotation = Quaternion.identity;
                    plusButton.transform.localScale = new Vector3(0.001f, 0.13f, 0.08f);
                    plusButton.transform.localPosition = new Vector3(0.52f, -0.4f, 0.34f - offset);
                    ApplyTransparentMaterial(plusButton.GetComponent<Renderer>(), buttonColors[0]);
                    IncrementalButtonCollider incrementalButton = plusButton.AddComponent<IncrementalButtonCollider>();
                    incrementalButton.onClick = () => method.Up.Invoke();

                    GameObject minusButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    if (!UnityInput.Current.GetKey(KeyCode.Q))
                        minusButton.layer = 2;
                    UnityEngine.Object.Destroy(minusButton.GetComponent<Rigidbody>());
                    minusButton.GetComponent<BoxCollider>().isTrigger = true;
                    minusButton.transform.parent = menu.transform;
                    minusButton.transform.rotation = Quaternion.identity;
                    minusButton.transform.localScale = new Vector3(0.001f, 0.13f, 0.08f);
                    minusButton.transform.localPosition = new Vector3(0.52f, -0.25f, 0.34f - offset);
                    ApplyTransparentMaterial(minusButton.GetComponent<Renderer>(), buttonColors[0]);
                    IncrementalButtonCollider incrementalButton2 = minusButton.AddComponent<IncrementalButtonCollider>();
                    incrementalButton2.onClick = () => method.Down.Invoke();

                    TextMeshPro minusText = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<TextMeshPro>();
                    minusText.font = currentFontTMP;
                    minusText.text = "-";
                    minusText.fontSize = 1;
                    minusText.alignment = TextAlignmentOptions.Center;
                    minusText.enableAutoSizing = true;
                    minusText.fontSizeMin = 0;
                    minusText.color = Color.white;
                    RectTransform minusRect = minusText.GetComponent<RectTransform>();
                    minusRect.sizeDelta = new Vector2(.2f, .02f);
                    minusRect.localPosition = new Vector3(.052f, -0.195f / 2.6f, 0.13f - offset / 2.6f);
                    minusRect.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

                    TextMeshPro plusText = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<TextMeshPro>();
                    plusText.font = currentFontTMP;
                    plusText.text = "+";
                    plusText.fontSize = 1;
                    plusText.alignment = TextAlignmentOptions.Center;
                    plusText.enableAutoSizing = true;
                    plusText.fontSizeMin = 0;
                    plusText.color = Color.white;
                    RectTransform plusRect = plusText.GetComponent<RectTransform>();
                    plusRect.sizeDelta = new Vector2(.2f, .02f);
                    plusRect.localPosition = new Vector3(.052f, -0.31298f / 2.6f, 0.13f - offset / 2.6f);
                    plusRect.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
                }
            }
            ttte(method, new Vector3(.052f, method.isIncremental ? 0.045f : 0, 0.13f - offset / 2.6f), TextAlignmentOptions.Midline);
        }
        public static void CreateCategoryPanel(float offset, ButtonInfo method)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.001f, 0.4f, BtnHeight);
            if (sidebuttons)
            {
                float far = IsCatLeft ? tett : -tett;
                gameObject.transform.localPosition = new Vector3(
        0.52f,
        (IsCatLeft ? -((menuBackground.transform.localScale.y / 2f) + (Sidebar.transform.localScale.y / 2f)) : ((menuBackground.transform.localScale.y / 2f) + (Sidebar.transform.localScale.y / 2f))) + (IsCatRotated ? 0f : (IsCatLeft ? -(SmFl * 20f) : (SmFl * 20f))) - far,
        0.46f - ((BtnHeight + BtnSpace) * CatIndex) - offset);
            }
            else
            {
                gameObject.transform.localPosition = new Vector3(
        0.52f,
        (IsCatLeft ? -((menuBackground.transform.localScale.y / 2f) + (Sidebar.transform.localScale.y / 2f)) : ((menuBackground.transform.localScale.y / 2f) + (Sidebar.transform.localScale.y / 2f))) + (IsCatRotated ? 0f : (IsCatLeft ? -(SmFl * 20f) : (SmFl * 20f))),
        0.46f - ((BtnHeight + BtnSpace) * CatIndex) - offset);
            }
            gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, IsCatRotated ? (IsCatLeft ? 45f : (-45f)) : 0f);
            gameObject.AddComponent<Classes.Button>().relatedText = method.buttonText;
            Renderer renderer = gameObject.GetComponent<Renderer>();
            renderer.material.SetFloat("_Mode", 3f);
            renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            renderer.material.SetInt("_ZWrite", 0);
            renderer.material.DisableKeyword("_ALPHATEST_ON");
            renderer.material.EnableKeyword("_ALPHABLEND_ON");
            renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            renderer.material.renderQueue = 3000;
            Color btnColor = buttonColors[0];
            btnColor.a = 0.5f;
            renderer.material.color = btnColor;
            TextMeshPro text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<TextMeshPro>();
            if (method.overlapText != null)
                text.text = method.overlapText;
            text.font = currentFontTMP;
            text.text = method.buttonText;
            text.fontSize = 1;
            text.alignment = TextAlignmentOptions.Center;
            text.enableAutoSizing = true;
            text.fontSizeMin = 0;
            text.color = Color.white;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .02f);
            float updown = 0.17f;
            float offs = 2.67f;
            if (sidebuttons)
            {
                float far = IsCatLeft ? tett : -0.6f;
                component.localPosition = new Vector3(.052f, gameObject.transform.position.y - far, updown - offset / offs);
            }
            else
            {
                component.localPosition = new Vector3(.052f, gameObject.transform.position.y, updown - offset / offs);
            }
            component.localPosition = new Vector3(.052f, gameObject.transform.position.y, updown - offset / offs);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f) + new Vector3(IsCatRotated ? (IsCatLeft ? (-19f) : 19f) : 0f, 0f, 0f));
        }
        public static void RecreateMenu()
        {
            if (menu != null)
            {
                Destroy(menu);
                menu = null;

                CreateMenu();
                RecenterMenu(rightHanded, UnityInput.Current.GetKey(keyboardButton));
            }
        }
        public static void RecenterMenu(bool isRightHanded, bool isKeyboardCondition)
        {
            if (!isKeyboardCondition)
            {

                if (!isRightHanded)
                {
                    menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                }
                else
                {
                    menu.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    Vector3 rotation2 = GorillaTagger.Instance.rightHandTransform.rotation.eulerAngles;
                    rotation2 += new Vector3(0f, 0f, 180f);
                    menu.transform.rotation = Quaternion.Euler(rotation2);
                }
            }
            else
            {
                try
                {
                    TPC = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>();
                }
                catch { }

                GameObject.Find("Shoulder Camera").transform.Find("CM vcam1").gameObject.SetActive(false);

                if (TPC != null)
                {
                    TPC.transform.position = new Vector3(-999f, -999f, -999f);
                    TPC.transform.rotation = Quaternion.identity;
                    menu.transform.parent = TPC.transform;
                    menu.transform.position = TPC.transform.position + (TPC.transform.forward * 0.5f) + (TPC.transform.up * -0.02f);
                    menu.transform.rotation = TPC.transform.rotation * Quaternion.Euler(-90f, 90f, 0f);

                    if (reference != null)
                    {
                        if (Mouse.current.leftButton.isPressed)
                        {
                            Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                            bool hitButton = Physics.Raycast(ray, out RaycastHit hit, 100);
                            if (hitButton)
                            {
                                Classes.Button collide = hit.transform.gameObject.GetComponent<Classes.Button>();
                                collide?.OnTriggerEnter(buttonCollider);
                                IncrementalButtonCollider collide2 = hit.transform.gameObject.GetComponent<IncrementalButtonCollider>();
                                collide2?.OnTriggerEnter(buttonCollider);
                            }
                        }
                        else
                            reference.transform.position = new Vector3(999f, -999f, -999f);
                    }
                }
            }
        }
        public enum EasingType { Linear, EaseIn, EaseOut, EaseInOut, Bounce, Overshoot, Elastic, Punch }
        public enum AnimationType { Scale, ScaleX, ScaleY, ScaleZ, ScaleYZ }

        public static EasingType easingType = EasingType.EaseInOut;
        public static AnimationType animationType = AnimationType.Scale;

        public static string currentAnimationName => $"{animationType} ({easingType})";
        public static void ChangeAnimationType(bool forward)
        {
            int direction = forward ? 1 : -1;
            int length = System.Enum.GetValues(typeof(AnimationType)).Length;
            animationType = (AnimationType)(((int)animationType + direction + length) % length);
            Main.GetIndex("Change Animation Type").overlapText = "Change Animation Type: " + animationType.ToString();
        }
        public static void ChangeEasingType(bool forward)
        {
            int direction = forward ? 1 : -1;
            int length = System.Enum.GetValues(typeof(EasingType)).Length;
            easingType = (EasingType)(((int)easingType + direction + length) % length);
            Main.GetIndex("Change Easing Type").overlapText = "Change Easing Type: " + easingType.ToString();
        }
        private static IEnumerator AnimateMenu(bool opening) // thanks claude
        {
            float time = 0f;

            Vector3 finalScale = menu.transform.localScale = new Vector3(0.1f, L_0 ? 0.5f : 0.3f, scintillawidemenu ? 0.3f : 0.3825f) * GTPlayer.Instance.scale * menuScaleMulti;
            switch (animationType)
            {
                case AnimationType.Scale:
                    menu.transform.localScale = opening ? Vector3.zero : finalScale;
                    break;
                case AnimationType.ScaleX:
                    menu.transform.localScale = opening ? new Vector3(0f, finalScale.y, finalScale.z) : finalScale;
                    break;
                case AnimationType.ScaleY:
                    menu.transform.localScale = opening ? new Vector3(finalScale.x, 0f, finalScale.z) : finalScale;
                    break;
                case AnimationType.ScaleZ:
                    menu.transform.localScale = opening ? new Vector3(finalScale.x, finalScale.y, 0f) : finalScale;
                    break;
                case AnimationType.ScaleYZ:
                    menu.transform.localScale = opening ? new Vector3(finalScale.x, 0f, 0f) : finalScale;
                    break;
            }

            while (time < 1f)
            {
                time += Time.deltaTime * menuAnimSpeed;
                float t = Mathf.Clamp01(time);
                float eased = Ease(t, easingType);

                switch (animationType)
                {
                    case AnimationType.Scale:
                        menu.transform.localScale = Vector3.LerpUnclamped(
                            opening ? Vector3.zero : finalScale,
                            opening ? finalScale : Vector3.zero,
                            eased);
                        break;
                    case AnimationType.ScaleX:
                        menu.transform.localScale = new Vector3(
                            Mathf.LerpUnclamped(opening ? 0f : finalScale.x, opening ? finalScale.x : 0f, eased),
                            finalScale.y,
                            finalScale.z);
                        break;
                    case AnimationType.ScaleY:
                        menu.transform.localScale = new Vector3(
                            finalScale.x,
                            Mathf.LerpUnclamped(opening ? 0f : finalScale.y, opening ? finalScale.y : 0f, eased),
                            finalScale.z);
                        break;
                    case AnimationType.ScaleZ:
                        menu.transform.localScale = new Vector3(
                            finalScale.x,
                            finalScale.y,
                            Mathf.LerpUnclamped(opening ? 0f : finalScale.z, opening ? finalScale.z : 0f, eased));
                        break;
                    case AnimationType.ScaleYZ:
                        menu.transform.localScale = new Vector3(
                            Mathf.LerpUnclamped(opening ? 0f : finalScale.x, opening ? finalScale.x : 0f, eased),
                            Mathf.LerpUnclamped(opening ? 0f : finalScale.y, opening ? finalScale.y : 0f, eased),
                            finalScale.z);
                        break;
                }

                yield return null;
            }

            switch (animationType)
            {
                case AnimationType.Scale:
                    menu.transform.localScale = opening ? finalScale : Vector3.zero;
                    break;
                case AnimationType.ScaleX:
                    menu.transform.localScale = opening ? finalScale : new Vector3(0f, finalScale.y, finalScale.z);
                    break;
                case AnimationType.ScaleY:
                    menu.transform.localScale = opening ? finalScale : new Vector3(finalScale.x, 0f, finalScale.z);
                    break;
                case AnimationType.ScaleZ:
                    menu.transform.localScale = opening ? finalScale : new Vector3(finalScale.x, finalScale.y, 0f);
                    break;
                case AnimationType.ScaleYZ:
                    menu.transform.localScale = opening ? finalScale : new Vector3(finalScale.x, 0f, 0f);
                    break;
            }

            if (!opening)
            {
                UnityEngine.Object.Destroy(menu);
                UnityEngine.Object.Destroy(reference);
                menu = null;
                reference = null;
                isClosing = false;
            }
        }
        private static float Ease(float t, EasingType type)
        {
            switch (type)
            {
                case EasingType.Linear:
                    return t;
                case EasingType.EaseIn:
                    return t * t;
                case EasingType.EaseOut:
                    return t * (2f - t);
                case EasingType.EaseInOut:
                    return t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;
                case EasingType.Bounce:
                    if (t < 1f / 2.75f) return 7.5625f * t * t;
                    else if (t < 2f / 2.75f) { t -= 1.5f / 2.75f; return 7.5625f * t * t + 0.75f; }
                    else if (t < 2.5f / 2.75f) { t -= 2.25f / 2.75f; return 7.5625f * t * t + 0.9375f; }
                    else { t -= 2.625f / 2.75f; return 7.5625f * t * t + 0.984375f; }
                case EasingType.Overshoot:
                    const float c1 = 2.70158f, c3 = c1 + 1f;
                    return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
                case EasingType.Elastic:
                    if (t == 0f || t == 1f) return t;
                    return -Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((t * 10f - 10.75f) * (2f * Mathf.PI) / 3f);
                case EasingType.Punch:
                    return Mathf.Sin(t * Mathf.PI) * 0.25f + t;
                default:
                    return t;
            }
        }
        public static void CreateReference(bool isRightHanded)
        {
            reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            reference.transform.parent = isRightHanded ? GorillaTagger.Instance.leftHandTransform : GorillaTagger.Instance.rightHandTransform;
            reference.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            buttonCollider = reference.GetComponent<SphereCollider>();
        }
        #endregion

        #region Button Handling
        public static void Toggle(string buttonText)
        {
            int lastPage = ((buttons[currentCategory].Length + buttonsPerPage - 1) / buttonsPerPage) - 1;
            if (buttonText == "return")
            {
                pageNumber = 0;
                currentCategory = 0;
            }
            if (buttonText == "PreviousPage")
            {
                pageNumber--;
                if (pageNumber < 0)
                    pageNumber = lastPage;
            }
            else
            {
                if (buttonText == "NextPage")
                {
                    pageNumber++;
                    if (pageNumber > lastPage)
                        pageNumber = 0;
                }
                else
                {
                    ButtonInfo target = GetIndex(buttonText);
                    if (target != null)
                    {
                        if (target.isTogglable)
                        {
                            target.enabled = !target.enabled;
                            if (target.enabled)
                            {
                                if (target.enableMethod != null)
                                    try { target.enableMethod.Invoke(); } catch { }
                            }
                            else
                            {
                                if (target.disableMethod != null)
                                    try { target.disableMethod.Invoke(); } catch { }
                            }
                        }
                        else
                        {
                            if (target.method != null)
                                try { target.method.Invoke(); } catch { }
                        }
                    }
                    else
                        Debug.LogError(buttonText + " does not exist");
                }
            }
            RecreateMenu();
        }

        private static readonly Dictionary<string, (int Category, int Index)> cacheGetIndex = new Dictionary<string, (int Category, int Index)>();
        public static ButtonInfo GetIndex(string buttonText)
        {
            if (buttonText == null)
                return null;

            if (cacheGetIndex.ContainsKey(buttonText))
            {
                var CacheData = cacheGetIndex[buttonText];
                try
                {
                    if (buttons[CacheData.Category][CacheData.Index].buttonText == buttonText)
                        return buttons[CacheData.Category][CacheData.Index];
                }
                catch { cacheGetIndex.Remove(buttonText); }
            }

            int categoryIndex = 0;
            foreach (ButtonInfo[] buttons in buttons)
            {
                int buttonIndex = 0;
                foreach (ButtonInfo button in buttons)
                {
                    if (button.buttonText == buttonText)
                    {
                        try
                        {
                            cacheGetIndex.Add(buttonText, (categoryIndex, buttonIndex));
                        }
                        catch
                        {
                            if (cacheGetIndex.ContainsKey(buttonText))
                                cacheGetIndex.Remove(buttonText);
                        }

                        return button;
                    }
                    buttonIndex++;
                }
                categoryIndex++;
            }
            foreach (ButtonInfo[] buttons in buttonsCat)
            {
                int buttonIndex = 0;
                foreach (ButtonInfo button in buttons)
                {
                    if (button.buttonText == buttonText)
                    {
                        try
                        {
                            cacheGetIndex.Add(buttonText, (categoryIndex, buttonIndex));
                        }
                        catch
                        {
                            if (cacheGetIndex.ContainsKey(buttonText))
                                cacheGetIndex.Remove(buttonText);
                        }

                        return button;
                    }
                    buttonIndex++;
                }
                categoryIndex++;
            }
            return null;
        }
        #endregion

        #region things
        public static Color RandomColor(byte range = 255, byte alpha = 255) =>
            new Color32((byte)UnityEngine.Random.Range(0, range),
                        (byte)UnityEngine.Random.Range(0, range),
                        (byte)UnityEngine.Random.Range(0, range),
                        alpha);
        public static void WorldScale(GameObject obj, Vector3 targetWorldScale)
        {
            Vector3 parentScale = obj.transform.parent.lossyScale;
            obj.transform.localScale = new Vector3(
                targetWorldScale.x / parentScale.x,
                targetWorldScale.y / parentScale.y,
                targetWorldScale.z / parentScale.z
            );
        }
        private static int? noInvisLayerMask;
        public static int NoInvisLayerMask()
        {
            noInvisLayerMask ??= ~(
                1 << LayerMask.NameToLayer("TransparentFX") |
                1 << LayerMask.NameToLayer("Ignore Raycast") |
                1 << LayerMask.NameToLayer("Zone") |
                1 << LayerMask.NameToLayer("Gorilla Trigger") |
                1 << LayerMask.NameToLayer("Gorilla Boundary") |
                1 << LayerMask.NameToLayer("GorillaCosmetics") |
                1 << LayerMask.NameToLayer("GorillaParticle"));

            return noInvisLayerMask ?? GTPlayer.Instance.locomotionEnabledLayers;
        }
        public static void GetOtherBoards()
        {
            if (BoardMat == null) return;

            var treeRoom = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom");
            if (treeRoom == null) return;

            var stumpChildren = treeRoom.transform
                .Cast<Transform>()
                .Where(x => x.name.Contains("UnityTempFile"))
                .ToList();

            if (stumpChildren.Count <= 3) return;

            Renderer ren = stumpChildren[3].GetComponent<Renderer>();
            if (ren != null)
                ren.material = BoardMat;
        }
        #endregion

        // g3ifs shit
        public class IncrementalButtonCollider : MonoBehaviour
        {
            public Action onClick;
            public void OnTriggerEnter(Collider other)
            {
                Sound sound = Sounds.List[Sounds.soundValue];
                if (other.gameObject == reference && Time.time > IncrementCooldown)
                {
                    IncrementCooldown = Time.time + 0.15f;
                    onClick?.Invoke();
                    if (sound.soundgtag)
                    {
                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(sound.soundId, true, 4f);
                    }
                    else
                    {
                        Soundboard.PlayAudioCS(sound.soundUrl, sound.wav);
                    }
                    StartCoroutine(RebuildNextFrame());
                }
            }
            private IEnumerator RebuildNextFrame()
            {
                yield return null;
                RecreateMenu();
            }
        }
        public class RoundedCorners : MonoBehaviour
        {
            [Range(0f, 0.5f)] public float bevel = 0.02f;
            public bool topLeft = true;
            public bool topRight = true;
            public bool bottomLeft = true;
            public bool bottomRight = true;
            public float multX = 0f;
            public float multY = 0f;
            public float bevelX = 0f;
            public float bevelY = 0f;
            private Renderer sourceRenderer;
            private GradientSetter gradientSetter;
            private ColorSetter colorSetter;
            void Start()
            {
                sourceRenderer = GetComponent<Renderer>();
                if (!sourceRenderer) return;
                gradientSetter = GetComponent<GradientSetter>();
                colorSetter = GetComponent<ColorSetter>();
                float sx = Mathf.Max(transform.localScale.y, 0.001f);
                float sy = Mathf.Max(transform.localScale.z, 0.001f);
                multX = (1f / sx) * (1f + Mathf.Log(sx + 1f));
                multY = (1f / sy) * (1f + Mathf.Log(sy + 1f));
                bevelX = bevel * multX;
                bevelY = bevel * multY;
                CreateGeometry();
                sourceRenderer.enabled = false;
            }
            void CreateGeometry()
            {
                Transform parent = transform;
                CreateCube(parent, Vector3.zero, new Vector3(1f, 1f - bevelX * 2f, 1f), false, -1);
                CreateCube(parent, Vector3.zero, new Vector3(1f, 1f, 1f - bevelY * 2f), false, -1);
                bool[] enabled = { topLeft, bottomLeft, topRight, bottomRight };
                Vector3[] offsets =
                {
                    new Vector3(0f, -0.5f + bevelX, -0.5f + bevelY),
                    new Vector3(0f, 0.5f - bevelX, -0.5f + bevelY),
                    new Vector3(0f, -0.5f + bevelX, 0.5f - bevelY),
                    new Vector3(0f, 0.5f - bevelX, 0.5f - bevelY)
                };
                for (int i = 0; i < 4; i++)
                {
                    bool isTop = (i == 2 || i == 3);
                    if (enabled[i])
                    {
                        GameObject c = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        Destroy(c.GetComponent<Collider>());
                        c.transform.SetParent(parent, false);
                        c.transform.localRotation = Quaternion.Euler(0, 0, 90);
                        c.transform.localScale = new Vector3(bevelX * 2f, 0.5f, bevelY * 2f);
                        c.transform.localPosition = offsets[i];
                        ConfigureRenderer(c.GetComponent<Renderer>(), true, isTop ? 0 : 1);
                    }
                    else
                    {
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        Destroy(cube.GetComponent<Collider>());
                        cube.transform.SetParent(parent, false);
                        cube.transform.localScale = new Vector3(1f, bevelX * 2f, bevelY * 2f);
                        cube.transform.localPosition = offsets[i];
                        ConfigureRenderer(cube.GetComponent<Renderer>(), true, isTop ? 0 : 1);
                    }
                }
            }
            void CreateCube(Transform parent, Vector3 pos, Vector3 scale, bool isCorner, int cornerType)
            {
                GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(g.GetComponent<Collider>());
                g.transform.SetParent(parent, false);
                g.transform.localPosition = pos;
                g.transform.localScale = scale;
                ConfigureRenderer(g.GetComponent<Renderer>(), isCorner, cornerType);
            }
            void ConfigureRenderer(Renderer r, bool isCorner, int cornerType)
            {
                Material oldMaterial = r.material;
                if (oldMaterial != null)
                    Destroy(oldMaterial);

                if (gradientSetter != null)
                {
                    GradientSetter gs = r.gameObject.AddComponent<GradientSetter>();
                    gs.brightness = gradientSetter.brightness;
                    gs.isVertical = gradientSetter.isVertical;
                    if (isCorner)
                    {
                        float bevelOffset = bevel * gradientSetter.gradientOffset;
                        if (cornerType == 0)
                        {
                            gs.startOffset = gradientSetter.startOffset;
                            gs.gradientOffset = bevelOffset;
                        }
                        else
                        {
                            gs.startOffset = gradientSetter.startOffset + gradientSetter.gradientOffset - bevelOffset;
                            gs.gradientOffset = bevelOffset;
                        }
                    }
                    else
                    {
                        gs.gradientOffset = gradientSetter.gradientOffset;
                        gs.startOffset = gradientSetter.startOffset;
                    }
                }
                else if (colorSetter != null)
                {
                    ColorSetter cs = r.gameObject.AddComponent<ColorSetter>();
                    cs.brightness = colorSetter.brightness;
                    cs.colorOffset = colorSetter.colorOffset;
                }
                r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                r.receiveShadows = false;
                r.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
                r.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            }
        }
        public class GradientSetter : MonoBehaviour
        {
            [Header("Color Settings")]
            [SerializeField, Range(0f, 2f)] public float brightness = 1f;
            [SerializeField] public bool isVertical = false;
            [SerializeField, Range(0f, 10f)] public float gradientOffset = 1f;
            [SerializeField, Range(0f, 10f)] public float startOffset = 0f;

            private Renderer rend;
            private Material materialInstance;
            private Texture2D gradientTexture;
            private Color[] pixels;

            private const int resolution = 64;

            private Color lastColorA;
            private Color lastColorB;

            private float timer;
            private const float updateInterval = 0.033f;

            private bool isCylinder;

            void Awake()
            {
                rend = GetComponent<Renderer>();
                if (!rend) return;

                isCylinder = GetComponent<MeshFilter>()?.sharedMesh.name.Contains("Cylinder") ?? false;

                materialInstance = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
                rend.material = materialInstance;

                CreateTexture();
                UpdateGradient(true);
            }

            void Update()
            {
                if (!isActiveAndEnabled || gradientTexture == null)
                    return;

                timer += Time.deltaTime;
                if (timer < updateInterval)
                    return;

                timer = 0f;
                UpdateGradient(false);
            }

            private void CreateTexture()
            {
                // 1D gradient texture (much faster)
                gradientTexture = new Texture2D(resolution, 1, TextureFormat.RGB24, false);
                gradientTexture.wrapMode = isCylinder ? TextureWrapMode.Repeat : TextureWrapMode.Clamp;
                gradientTexture.filterMode = FilterMode.Bilinear;

                pixels = new Color[resolution];

                materialInstance.mainTexture = gradientTexture;
                materialInstance.color = Color.white;
            }
            private bool ColorsAlmostEqual(Color a, Color b)
            {
                float r = a.r - b.r;
                float g = a.g - b.g;
                float bDiff = a.b - b.b;

                return (r * r + g * g + bDiff * bDiff) < 0.0004f;
            }
            private void UpdateGradient(bool force)
            {
                Theme theme = Themes.List[ThemeValue];
                if (theme?.Colors == null || theme.Colors.Length == 0)
                    return;

                Color colorA = GetOffsetColor(theme, startOffset) * brightness;
                Color colorB = GetOffsetColor(theme, startOffset + gradientOffset) * brightness;

                if (!force &&
    ColorsAlmostEqual(colorA, lastColorA) &&
    ColorsAlmostEqual(colorB, lastColorB))
                    return;

                lastColorA = colorA;
                lastColorB = colorB;

                for (int i = 0; i < resolution; i++)
                {
                    float t = (float)i / (resolution - 1);
                    pixels[i] = Color.Lerp(colorA, colorB, t);
                }

                gradientTexture.SetPixels(pixels);
                gradientTexture.Apply(false);
            }

            private Color GetOffsetColor(Theme theme, float offset)
            {
                if (theme.Colors.Length == 1)
                    return theme.Colors[0];

                float totalRange = theme.Colors.Length - 1;
                float t = Mathf.PingPong((Time.time + offset) * theme.Speed, totalRange);

                int indexA = Mathf.FloorToInt(t);
                int indexB = Mathf.Clamp(indexA + 1, 0, theme.Colors.Length - 1);

                float localT = t - indexA;

                float easedT = localT < 0.5f
                    ? 2f * localT * localT
                    : 1f - Mathf.Pow(-2f * localT + 2f, 2f) / 2f;

                return Color.Lerp(theme.Colors[indexA], theme.Colors[indexB], easedT);
            }

            public void SetBrightness(float value)
            {
                brightness = Mathf.Max(0f, value);
                UpdateGradient(true);
            }

            private void OnDestroy()
            {
                if (gradientTexture)
                    Destroy(gradientTexture);

                if (materialInstance)
                    Destroy(materialInstance);
            }
        }
        public class ColorSetter : MonoBehaviour
        {
            [Header("Color Settings")]
            [SerializeField, Range(0f, 1f)] public float brightness = 1f;
            [SerializeField, Range(0f, 10f)] public float colorOffset = 0f;
            private Renderer rend;
            private Material instanceMaterial;
            private Color lastAppliedColor;
            private float updateTimer = 0f;
            private const float updateInterval = 0.033f;
            private void Start()
            {
                rend = GetComponent<Renderer>();
                if (rend == null) return;

                instanceMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
                instanceMaterial.SetFloat("_Surface", 1);
                instanceMaterial.SetFloat("_Blend", 0);
                instanceMaterial.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
                instanceMaterial.SetFloat("_DstBlend", (float)BlendMode.OneMinusSrcAlpha);
                instanceMaterial.SetFloat("_ZWrite", 0);
                instanceMaterial.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                instanceMaterial.renderQueue = (int)RenderQueue.Transparent;

                rend.material = instanceMaterial;
                lastAppliedColor = new Color(0f, 0f, 0f, 1f - brightness);
                instanceMaterial.color = lastAppliedColor;
            }
            private void Update()
            {
                if (rend == null || instanceMaterial == null || !isActiveAndEnabled) return;
                updateTimer += Time.deltaTime;
                if (updateTimer >= updateInterval)
                {
                    updateTimer = 0f;
                    Color targetColor = new Color(0f, 0f, 0f, 1f - brightness);
                    if (Mathf.Abs(lastAppliedColor.a - targetColor.a) > 0.02f)
                    {
                        lastAppliedColor = targetColor;
                        instanceMaterial.color = targetColor;
                    }
                }
            }
            public void SetBrightness(float value)
            {
                brightness = Mathf.Clamp01(value);
            }
            private void OnDestroy()
            {
                if (instanceMaterial != null)
                    Destroy(instanceMaterial);
            }
        }
    }
}