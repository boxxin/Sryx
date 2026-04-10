using ExitGames.Client.Photon;
using femboy.Classes;
using GorillaLocomotion;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using StupidTemplate.Classes;
using StupidTemplate.Menu;
using System.Collections;
using System.Collections.Generic;
using TagEffects;
using TMPro;
using UnityEngine;
using static StupidTemplate.Menu.Main;
using Object = UnityEngine.Object;
using Time = UnityEngine.Time;

namespace StupidTemplate.Mods
{
    internal class Visual
    {
        public static Color ESPColor;
        public static void DrawSwastikaESP() // some how got ai to do this
        {
            if (NetworkSystem.Instance?.PlayerListOthers == null)
                return;
            
            foreach (NetPlayer netPlayer in NetworkSystem.Instance.PlayerListOthers)
            {
                VRRig rig = RigManager.getVRRig(netPlayer);
                if (rig == null || rig.isOfflineVRRig || rig.isMyPlayer)
                    continue;

                string name = "swastika_" + netPlayer.UserId;
                Transform existing = rig.transform.Find(name);

                GameObject root;

                if (existing == null)
                {
                    root = new GameObject(name);
                    root.transform.SetParent(rig.transform);
                    root.transform.localPosition = Vector3.zero;

                    float thickness = 0.022f;
                    float arm = 0.35f;
                    float tail = 0.36f;

                    ESPColor.a = 0.85f;

                    CreateLine(root, Vector3.zero, new Vector3(0, arm, 0), thickness, ESPColor);
                    CreateLine(root, new Vector3(0, arm, 0), new Vector3(tail, arm, 0), thickness, ESPColor);

                    CreateLine(root, Vector3.zero, new Vector3(arm, 0, 0), thickness, ESPColor);
                    CreateLine(root, new Vector3(arm, 0, 0), new Vector3(arm, -tail, 0), thickness, ESPColor);

                    CreateLine(root, Vector3.zero, new Vector3(0, -arm, 0), thickness, ESPColor);
                    CreateLine(root, new Vector3(0, -arm, 0), new Vector3(-tail, -arm, 0), thickness, ESPColor);

                    CreateLine(root, Vector3.zero, new Vector3(-arm, 0, 0), thickness, ESPColor);
                    CreateLine(root, new Vector3(-arm, 0, 0), new Vector3(-arm, tail, 0), thickness, ESPColor);
                }
                else
                {
                    root = existing.gameObject;
                }

                root.transform.Rotate(Vector3.forward * 180f * Time.deltaTime);

                float t = Mathf.PingPong(Time.time, 1);
                Color animatedColor = Color.Lerp(ColorLib.DarkDodgerBlue, ColorLib.NeonLime, t);
                animatedColor.a = 0.85f;

                Renderer[] renderers = root.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in renderers)
                {
                    r.material.color = animatedColor;
                }
            }

        }
        public static void CleanUpSwaster()
        {
            if (NetworkSystem.Instance?.PlayerListOthers == null)
                return;

            foreach (NetPlayer netPlayer in NetworkSystem.Instance.PlayerListOthers)
            {
                VRRig rig = RigManager.getVRRig(netPlayer);
                if (rig == null)
                    continue;

                Transform esp = rig.transform.Find("swastika_" + netPlayer.UserId);
                if (esp != null)
                {
                    GameObject.Destroy(esp.gameObject);
                }
            }
        }
        private static void CreateLine(GameObject parent, Vector3 start, Vector3 end, float thickness, Color color)
        {
            GameObject lineObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            UnityEngine.Object.Destroy(lineObj.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(lineObj.GetComponent<BoxCollider>());

            lineObj.transform.SetParent(parent.transform);

            Vector3 direction = end - start;
            float distance = direction.magnitude;

            lineObj.transform.localPosition = (start + end) / 2f;
            lineObj.transform.localRotation = Quaternion.LookRotation(direction);
            lineObj.transform.localScale = new Vector3(thickness, thickness, distance);

            Renderer renderer = lineObj.GetComponent<Renderer>();
            renderer.material.shader = Shader.Find("GUI/Text Shader");
            renderer.material.color = color;
        }
        public static void DrawStarOfDavid()
        {
            if (NetworkSystem.Instance?.PlayerListOthers == null)
                return;

            foreach (NetPlayer netPlayer in NetworkSystem.Instance.PlayerListOthers)
            {
                VRRig rig = RigManager.getVRRig(netPlayer);
                if (rig == null || rig.isOfflineVRRig || rig.isMyPlayer)
                    continue;

                string name = "StarOfDavidESP_" + netPlayer.UserId;
                Transform existing = rig.transform.Find(name);

                GameObject root;

                if (existing == null)
                {
                    root = new GameObject(name);
                    root.transform.SetParent(rig.transform);
                    root.transform.localPosition = Vector3.zero;

                    float thickness = 0.012f;
                    float r = 0.45f;

                    float cx = 0f;
                    float cy = 0f;
                    float z = 0f;

                    Vector3 uTop = new Vector3(cx, cy + r, z);
                    Vector3 uBotL = new Vector3(cx - r * 0.866f, cy - r * 0.5f, z);
                    Vector3 uBotR = new Vector3(cx + r * 0.866f, cy - r * 0.5f, z);

                    Vector3 dBot = new Vector3(cx, cy - r, z);
                    Vector3 dTopL = new Vector3(cx - r * 0.866f, cy + r * 0.5f, z);
                    Vector3 dTopR = new Vector3(cx + r * 0.866f, cy + r * 0.5f, z);

                    CreateLine(root, uTop, uBotL, thickness, Color.white);
                    CreateLine(root, uBotL, uBotR, thickness, Color.white);
                    CreateLine(root, uBotR, uTop, thickness, Color.white);

                    CreateLine(root, dBot, dTopL, thickness, Color.white);
                    CreateLine(root, dTopL, dTopR, thickness, Color.white);
                    CreateLine(root, dTopR, dBot, thickness, Color.white);
                }
                else
                {
                    root = existing.gameObject;
                }

                root.transform.Rotate(Vector3.forward * 180f * Time.deltaTime);

                float t = Mathf.PingPong(Time.time, 1f);
                Color animatedColor = Color.Lerp(ColorLib.DarkDodgerBlue, ColorLib.NeonLime, t);
                animatedColor.a = 0.85f;

                Renderer[] renderers = root.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in renderers)
                {
                    r.material.color = animatedColor;
                }
            }
        }
        public static void CleanUpStar()
        {
            foreach (GameObject g in GameObject.FindObjectsOfType<GameObject>())
            {
                if (g.name.StartsWith("StarOfDavidESP_"))
                {
                    GameObject.Destroy(g);
                }
            }
        }
        public static void DrawSeroxenESP()
        {
            if (NetworkSystem.Instance?.PlayerListOthers == null)
                return;

            foreach (NetPlayer netPlayer in NetworkSystem.Instance.PlayerListOthers)
            {
                VRRig rig = RigManager.getVRRig(netPlayer);
                if (rig == null || rig.isOfflineVRRig || rig.isMyPlayer)
                    continue;

                string espName = "SeroxenESP_" + netPlayer.UserId;
                Transform existing = rig.transform.Find(espName);
                GameObject root;

                if (existing == null)
                {
                    root = new GameObject(espName);
                    root.transform.SetParent(rig.transform, false);
                    root.transform.localPosition = new Vector3(0f, 0f, 0f);
                    root.transform.localRotation = Quaternion.identity;

                    GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    UnityEngine.Object.Destroy(box.GetComponent<Rigidbody>());
                    UnityEngine.Object.DestroyImmediate(box.GetComponent<BoxCollider>());
                    box.transform.SetParent(root.transform, false);
                    box.transform.localPosition = Vector3.zero;
                    box.transform.localRotation = Quaternion.identity;

                    Vector3 rigScale = rig.transform.lossyScale;
                    float worldSize = 0.6f;
                    box.transform.localScale = new Vector3(
                        worldSize / Mathf.Max(rigScale.x, 0.001f),
                        worldSize / Mathf.Max(rigScale.y, 0.001f),
                        worldSize / Mathf.Max(rigScale.z, 0.001f));

                    Renderer rend = box.GetComponent<Renderer>();
                    rend.material.shader = Shader.Find("Universal Render Pipeline/Lit");
                    rend.material.color = Color.white;
                    rend.material.mainTexture = LoadTextureFromURL("https://madoka.fit/seroxen.png", "fgfg.jpg");
                }
                else
                {
                    root = existing.gameObject;
                }

                root.transform.Rotate(Vector3.up, 180f * Time.deltaTime, Space.World);

                float t = Mathf.PingPong(Time.time, 1f);
                Color animatedColor = Color.Lerp(ColorLib.DarkDodgerBlue, ColorLib.NeonLime, t);
                animatedColor.a = 0.85f;

                foreach (Renderer r in root.GetComponentsInChildren<Renderer>())
                    r.material.color = animatedColor;
            }
        }
        public static void CleanUpSeroxenESP()
        {
            foreach (GameObject g in GameObject.FindObjectsOfType<GameObject>())
            {
                if (g.name.StartsWith("SeroxenESP_"))
                {
                    GameObject.Destroy(g);
                }
            }
        }
        public static void DrawBoxESP()
        {
            foreach (NetPlayer netPlayer in NetworkSystem.Instance.PlayerListOthers)
            {
                VRRig rig = RigManager.getVRRig(netPlayer);
                if (!rig.isOfflineVRRig && !rig.isMyPlayer)
                {
                    GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    UnityEngine.Object.Destroy(box.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(box.GetComponent<BoxCollider>());
                    box.transform.rotation = Quaternion.identity;
                    box.transform.localScale = new Vector3(1f, 1.8f, rig.transform.localScale.z);
                    box.transform.localPosition = rig.transform.localPosition;
                    box.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    if (!rig.mainSkin.material.name.Contains("fected"))
                    {
                        box.GetComponent<Renderer>().material.color = ColorLib.EnchantedBlue;
                    }
                    else
                    {
                        box.GetComponent<Renderer>().material.color = ColorLib.DarkRed;
                    }
                    Material boxMat = box.GetComponent<Renderer>().material;
                    Color boxColor = boxMat.color;
                    boxColor.a = 0.2f;
                    boxMat.color = boxColor;
                    UnityEngine.Object.Destroy(box, Time.deltaTime);
                }
            }
        }
        public static GameObject BoyKisserball;
        public static bool grabbed = false;
        public static Rigidbody ballRb;
        private const byte SeroxenCubeEvent = 77;
        private const byte LaquishaEvent = 78;
        public static bool eventSubscribed = false;
        public static void laquishanetwork()
        {
            Vector3 position = new Vector3(-63.5511f, 12.2094f, -82.6264f);

            object[] content = new object[]
            {
            position,
            };

            RaiseEventOptions options = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others
            };

            PhotonNetwork.RaiseEvent(
                LaquishaEvent,
                content,
                options,
                SendOptions.SendReliable
            );

            laquisha(position);
        }
        private static void laquisha(Vector3 pos)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            cube.transform.position = pos;
            cube.transform.Rotate(0f, 180f, 0f);

            Renderer r = cube.GetComponent<Renderer>();
            r.material.shader = Shader.Find("Universal Render Pipeline/Lit");
            r.material.color = Color.white;
            r.material.mainTexture = LoadTextureFromURL("https://cdn.discordapp.com/attachments/1404406911771869284/1488348035921608714/Screenshot_2026-03-30_200123.png?ex=69cc739f&is=69cb221f&hm=4c800e3fad97946157c22e8ec4fb3ebd347e3bb9d7ce8b1458690fa3eeecd32b", "67.jpg");
        }
        public static void OnEvent2(EventData photonEvent)
        {
            if (photonEvent.Code != LaquishaEvent)
                return;

            object[] data = (object[])photonEvent.CustomData;

            Vector3 position = (Vector3)data[0];

            laquisha(position);
        }
        public static void BoyKisserBall()
        {
            if (!PhotonNetwork.InRoom)
                return;

            Transform hand = GorillaTagger.Instance.rightHandTransform;

            if (ControllerInputPoller.instance.rightGrab)
            {
                Vector3 position = hand.position;
                Vector3 direction = -hand.forward;
                float force = 15f;

                object[] content = new object[]
                {
            position,
            direction,
            force
                };

                RaiseEventOptions options = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others
                };

                PhotonNetwork.RaiseEvent(
                    SeroxenCubeEvent,
                    content,
                    options,
                    SendOptions.SendReliable
                );

                SpawnSeroxenCube(position, direction, force);
            }
        }

        public static void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code != SeroxenCubeEvent)
                return;

            object[] data = (object[])photonEvent.CustomData;

            Vector3 position = (Vector3)data[0];
            Vector3 direction = (Vector3)data[1];
            float force = (float)data[2];

            SpawnSeroxenCube(position, direction, force);
        }

        private static void SpawnSeroxenCube(Vector3 position, Vector3 direction, float force)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(UnityEngine.Random.Range(0.001f, 0.5f), UnityEngine.Random.Range(0.001f, 0.5f), UnityEngine.Random.Range(0.001f, 0.5f));
            cube.transform.position = position;

            Rigidbody rb = cube.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.velocity = direction * force;

            Renderer r = cube.GetComponent<Renderer>();
            r.material.shader = Shader.Find("Universal Render Pipeline/Lit");
            r.material.color = Color.white;
            r.material.mainTexture = LoadTextureFromURL("https://madoka.fit/seroxen.png", "seroxenlauncher.jpg");

            UnityEngine.Object.Destroy(cube, 20f);
        }
        public static void testlo()
        {
            GTPlayer.Instance.StartCoroutine(Intro());
        }

        private static IEnumerator Intro()
        {
            Transform head = GorillaTagger.Instance.headCollider.transform;

            GameObject whiteBack = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(whiteBack.GetComponent<Rigidbody>());
            UnityEngine.Object.DestroyImmediate(whiteBack.GetComponent<BoxCollider>());
            whiteBack.transform.localScale = new Vector3(6f, 4f, 0.01f);
            whiteBack.transform.position = head.position;
            whiteBack.transform.rotation = head.rotation;

            Renderer wbRend = whiteBack.GetComponent<Renderer>();
            wbRend.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            wbRend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            wbRend.receiveShadows = false;
            SetTransparentURP(wbRend.material);
            wbRend.material.color = new Color(0f, 0f, 0f, 0f);

            GameObject backdrop = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(backdrop.GetComponent<Rigidbody>());
            UnityEngine.Object.DestroyImmediate(backdrop.GetComponent<BoxCollider>());
            backdrop.transform.localScale = new Vector3(1f, 1f, 0.01f);
            backdrop.transform.position = head.position;
            backdrop.transform.rotation = head.rotation * Quaternion.Euler(0f, 0f, 180f);

            Renderer bdRend = backdrop.GetComponent<Renderer>();
            bdRend.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            bdRend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            bdRend.receiveShadows = false;
            bdRend.material.mainTexture = LoadTextureFromURL(
                "https://cdn.discordapp.com/attachments/1373831054896529410/1487994697317220403/New_Project_2.png?ex=69cb2a8d&is=69c9d90d&hm=a5acd67a404b0a674fcf0d05b46aa00ca1b90518eb21cafc9cdc2dec7d07b2a1",
                "intro.jpg");
            SetTransparentURP(bdRend.material);
            bdRend.material.color = new Color(1f, 1f, 1f, 0f);

            const int COUNT = 60;
            const float DURATION = 5f;
            const float FADE_START = 3.5f;
            const float SPREAD = 2.5f;

            Color[] palette = { ColorLib.DarkDodgerBlue, ColorLib.NeonLime };

            var objs = new GameObject[COUNT];
            var rends = new Renderer[COUNT];
            var vels = new Vector3[COUNT];
            var offsets = new Vector3[COUNT];
            var baseCol = new Color[COUNT];

            for (int i = 0; i < COUNT; i++)
            {
                float size = UnityEngine.Random.Range(0.018f, 0.065f);
                var p = GameObject.CreatePrimitive(
                    UnityEngine.Random.value > 0.5f ? PrimitiveType.Sphere : PrimitiveType.Cube);

                UnityEngine.Object.Destroy(p.GetComponent<Rigidbody>());
                UnityEngine.Object.DestroyImmediate(p.GetComponent<Collider>());
                p.transform.localScale = Vector3.one * size;

                offsets[i] = new Vector3(
                    UnityEngine.Random.Range(-SPREAD, SPREAD),
                    UnityEngine.Random.Range(-SPREAD * 0.6f, SPREAD * 0.6f),
                    UnityEngine.Random.Range(0.5f, 2.5f));

                p.transform.position = head.position + head.rotation * offsets[i];
                p.transform.rotation = UnityEngine.Random.rotation;

                Color c = palette[UnityEngine.Random.Range(0, palette.Length)];
                c.a = 0f;

                Renderer r = p.GetComponent<Renderer>();
                r.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                r.receiveShadows = false;
                SetTransparentURP(r.material);
                r.material.color = c;

                vels[i] = new Vector3(
                    UnityEngine.Random.Range(-0.3f, 0.3f),
                    UnityEngine.Random.Range(-0.15f, 0.15f),
                    UnityEngine.Random.Range(-0.1f, 0.1f));

                baseCol[i] = c;
                objs[i] = p;
                rends[i] = r;
            }

            float elapsed = 0f;

            while (elapsed < DURATION)
            {
                elapsed += Time.deltaTime;

                float globalAlpha = elapsed < 0.8f
                    ? Mathf.Clamp01(elapsed / 0.8f)
                    : elapsed > FADE_START
                        ? Mathf.Lerp(1f, 0f, (elapsed - FADE_START) / (DURATION - FADE_START))
                        : 1f;

                if (backdrop != null)
                {
                    backdrop.transform.position = head.position + head.forward * 1.5f;
                    backdrop.transform.rotation = head.rotation * Quaternion.Euler(0f, 0f, 180f);
                    bdRend.material.color = new Color(1f, 1f, 1f, globalAlpha);
                }

                if (whiteBack != null)
                {
                    whiteBack.transform.position = head.position + head.forward * 1.51f;
                    whiteBack.transform.rotation = head.rotation;
                    wbRend.material.color = new Color(0f, 0f, 0f, globalAlpha);
                }

                for (int i = 0; i < COUNT; i++)
                {
                    if (objs[i] == null) continue;

                    offsets[i] += vels[i] * Time.deltaTime;
                    objs[i].transform.position = head.position + head.rotation * offsets[i];
                    objs[i].transform.Rotate(Vector3.one * (30f + i * 3f) * Time.deltaTime);

                    float pulse = 0.6f + 0.4f * Mathf.Sin(Time.time * 2f + i * 0.8f);
                    Color c = baseCol[i];
                    c.a = globalAlpha * pulse;
                    rends[i].material.color = c;
                }

                yield return null;
            }

            if (backdrop != null) UnityEngine.Object.Destroy(backdrop);
            if (whiteBack != null) UnityEngine.Object.Destroy(whiteBack);
            foreach (var p in objs)
                if (p != null) UnityEngine.Object.Destroy(p);
        }

        private static void SetTransparentURP(Material mat)
        {
            mat.SetFloat("_Surface", 1f);
            mat.SetFloat("_Blend", 0f);
            mat.SetFloat("_SrcBlend", (float)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetFloat("_DstBlend", (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetFloat("_ZWrite", 0f);
            mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            mat.renderQueue = 3000;
        }
        private struct RigEntry
        {
            public GameObject Root;
            public Renderer[] Edges;
            public Material Mat;
        }

        private static readonly Dictionary<VRRig, RigEntry> _cache =
            new Dictionary<VRRig, RigEntry>();

        private static Shader _shader;
        private static Shader ESPShader =>
            _shader != null ? _shader : (_shader = Shader.Find("GUI/Text Shader"));

        private const float W = 0.50f;
        private const float H = 0.90f;
        private const float THICKNESS = 0.0032f;
        private const float LOCAL_Y = -0.50f;

        private static readonly Vector3[] _corners = new Vector3[8]
        {
        new Vector3(-W/2, 0, -W/2),
        new Vector3( W/2, 0, -W/2),
        new Vector3( W/2, 0,  W/2),
        new Vector3(-W/2, 0,  W/2),
        new Vector3(-W/2, H, -W/2),
        new Vector3( W/2, H, -W/2),
        new Vector3( W/2, H,  W/2),
        new Vector3(-W/2, H,  W/2),
        };

        private static readonly int[,] _edges = new int[12, 2]
        {
        {0,1},{1,2},{2,3},{3,0},
        {4,5},{5,6},{6,7},{7,4},
        {0,4},{1,5},{2,6},{3,7}
        };
        public static bool Wireframe3D = true;

        public static void DrawWireframeESP()
        {
            if (NetworkSystem.Instance == null) return;

            foreach (var kv in _cache)
            {
                if (kv.Value.Root != null)
                    Object.Destroy(kv.Value.Root);
                if (kv.Value.Mat != null)
                    Object.Destroy(kv.Value.Mat);
            }
            _cache.Clear();

            foreach (NetPlayer netPlayer in NetworkSystem.Instance.PlayerListOthers)
            {
                VRRig rig = RigManager.getVRRig(netPlayer);
                if (rig == null || rig.isOfflineVRRig || rig.isMyPlayer || rig.isLocal)
                    continue;

                BuildWireframe(rig);

                if (_cache.TryGetValue(rig, out RigEntry entry) && entry.Mat != null)
                {
                    bool infected = rig.mainSkin.material.name.Contains("fected");

                    Color col = infected
                        ? ColorLib.DarkRed
                        : ColorLib.DarkDodgerBlueTransparent;
                    col.a = 0.85f;

                    for (int i = 0; i < entry.Edges.Length; i++)
                    {
                        if (entry.Edges[i] != null)
                            entry.Edges[i].material.color = col;
                    }

                    float depth = Wireframe3D
                        ? rig.transform.localScale.z * 0.5f
                        : 0.001f;

                    UpdateEdgeDepth(entry, depth);
                }
            }
        }

        public static void ClearAll2()
        {
            foreach (var kv in _cache)
            {
                if (kv.Value.Root != null)
                    Object.Destroy(kv.Value.Root);
                if (kv.Value.Mat != null)
                    Object.Destroy(kv.Value.Mat);
            }
            _cache.Clear();
        }

        private static void BuildWireframe(VRRig rig)
        {
            int ignoreLayer = LayerMask.NameToLayer("Ignore Raycast");
            if (ignoreLayer < 0) ignoreLayer = 2;

            var mat = new Material(ESPShader);
            Color col = rig.mainSkin.material.name.Contains("fected")
                ? ColorLib.DarkRed
                : ColorLib.DarkDodgerBlueTransparent;
            col.a = 0.5f;
            mat.color = col;

            var root = new GameObject("WireframeESP");
            root.transform.SetParent(rig.transform, false);
            root.transform.localPosition = new Vector3(0f, LOCAL_Y, 0f);
            root.transform.localRotation = Quaternion.identity;
            root.transform.localScale = Vector3.one;

            float depth = rig.transform.localScale.z * 0.5f;
            var edges = new Renderer[12];

            for (int i = 0; i < 12; i++)
            {
                Vector3 start = CornerWithDepth(_corners[_edges[i, 0]], depth);
                Vector3 end = CornerWithDepth(_corners[_edges[i, 1]], depth);
                Vector3 dir = end - start;
                float len = dir.magnitude;

                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                var col2 = cube.GetComponent<Collider>();
                if (col2 != null) { col2.enabled = false; Object.Destroy(col2); }

                cube.transform.SetParent(root.transform, false);
                cube.transform.localPosition = (start + end) * 0.5f;
                cube.transform.localRotation = len > 0f
                    ? Quaternion.LookRotation(dir.normalized, Vector3.up)
                    : Quaternion.identity;
                cube.transform.localScale = new Vector3(THICKNESS, THICKNESS, Mathf.Max(len, 0.001f));
                cube.layer = ignoreLayer;

                var rend = cube.GetComponent<Renderer>();
                rend.material = mat;
                rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                rend.receiveShadows = false;
                edges[i] = rend;
            }

            _cache[rig] = new RigEntry { Root = root, Edges = edges, Mat = mat };
        }

        private static void UpdateEdgeDepth(RigEntry entry, float depth)
        {
            for (int i = 0; i < 12; i++)
            {
                if (entry.Edges[i] == null) continue;

                Vector3 start = CornerWithDepth(_corners[_edges[i, 0]], depth);
                Vector3 end = CornerWithDepth(_corners[_edges[i, 1]], depth);
                Vector3 dir = end - start;
                float len = dir.magnitude;

                var t = entry.Edges[i].transform;
                t.localPosition = (start + end) * 0.5f;
                t.localRotation = len > 0f
                    ? Quaternion.LookRotation(dir.normalized, Vector3.up)
                    : Quaternion.identity;
                t.localScale = new Vector3(THICKNESS, THICKNESS, Mathf.Max(len, 0.001f));
            }
        }

        private static Vector3 CornerWithDepth(Vector3 corner, float depth)
            => new Vector3(corner.x, corner.y, Mathf.Sign(corner.z) * depth * 0.5f);

        public static GameObject trailOrigin;
        public static GameObject trailOrigin_;
        public static TrailRenderer trailComponent;
        public static TrailRenderer trailComponent_;
        public static Color cool = ColorLib.DarkDodgerBlue;
        public static Color cool_ = ColorLib.NeonLime;
        public static Color cool2 = ColorLib.DarkDodgerBlue;
        public static Color cool2_ = ColorLib.NeonLime;
        public static void HandTrails()
        {
            if (trailOrigin == null)
            {
                trailOrigin = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                trailOrigin.transform.parent = GorillaTagger.Instance.rightHandTransform;
                trailOrigin.transform.localPosition = new Vector3(0f, -0.1f, 0f);
                trailOrigin.GetComponent<Renderer>().enabled = false;
                trailOrigin.GetComponent<Collider>().enabled = false;
                trailComponent = trailOrigin.AddComponent<TrailRenderer>();
                trailComponent.time = 0.2f;
                trailComponent.startWidth = 0.03f;
                trailComponent.endWidth = 0f;
                trailComponent.material = new Material(Shader.Find("GUI/Text Shader"));
            }
            if (trailOrigin_ == null)
            {
                trailOrigin_ = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                trailOrigin_.transform.parent = GorillaTagger.Instance.leftHandTransform;
                trailOrigin_.transform.localPosition = new Vector3(0f, -0.1f, 0f);
                trailOrigin_.GetComponent<Renderer>().enabled = false;
                trailOrigin_.GetComponent<Collider>().enabled = false;
                trailComponent_ = trailOrigin_.AddComponent<TrailRenderer>();
                trailComponent_.time = 0.2f;
                trailComponent_.startWidth = 0.03f;
                trailComponent_.endWidth = 0f;
                trailComponent_.material = new Material(Shader.Find("GUI/Text Shader"));
            }
            trailComponent.startColor = cool;
            trailComponent.endColor = cool_;
            trailComponent_.startColor = cool2;
            trailComponent_.endColor = cool2_;
        }
        public static void WristTrails()
        {
            if (trailOrigin == null)
            {
                trailOrigin = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                trailOrigin.transform.parent = GorillaTagger.Instance.offlineVRRig.rightHandTransform;
                trailOrigin.transform.localPosition = new Vector3(0f, 0f, 0f);
                trailOrigin.GetComponent<Renderer>().enabled = false;
                trailOrigin.GetComponent<Collider>().enabled = false;
                trailComponent = trailOrigin.AddComponent<TrailRenderer>();
                trailComponent.time = 0.2f;
                trailComponent.startWidth = 0.03f;
                trailComponent.endWidth = 0f;
                trailComponent.material = new Material(Shader.Find("GUI/Text Shader"));
            }
            if (trailOrigin_ == null)
            {
                trailOrigin_ = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                trailOrigin_.transform.parent = GorillaTagger.Instance.offlineVRRig.leftHandTransform;
                trailOrigin_.transform.localPosition = new Vector3(0f, 0f, 0f);
                trailOrigin_.GetComponent<Renderer>().enabled = false;
                trailOrigin_.GetComponent<Collider>().enabled = false;
                trailComponent_ = trailOrigin_.AddComponent<TrailRenderer>();
                trailComponent_.time = 0.2f;
                trailComponent_.startWidth = 0.03f;
                trailComponent_.endWidth = 0f;
                trailComponent_.material = new Material(Shader.Find("GUI/Text Shader"));
            }
            trailComponent.startColor = cool;
            trailComponent.endColor = cool_;
            trailComponent_.startColor = cool2;
            trailComponent_.endColor = cool2_;
        }
        public static void ClearHandTrails()
        {
            UnityEngine.Object.Destroy(trailOrigin);
            trailOrigin = null;
            UnityEngine.Object.Destroy(trailOrigin_);
            trailOrigin_ = null;
            UnityEngine.Object.Destroy(trailComponent);
            trailComponent = null;
            UnityEngine.Object.Destroy(trailComponent_);
            trailComponent_ = null;
        }
        public static void BreakLights()
        {
            BetterDayNightManager.instance.AnimateLightFlash(2, 0f, 0f, 2f);
        }
        public static void BreakLightDisable()
        {
            BetterDayNightManager.instance.AnimateLightFlash(2, 2f, 2f, 2f);
        }
        private static Dictionary<VRRig, LineRenderer> _spineRenderers = new Dictionary<VRRig, LineRenderer>();
        private static Dictionary<VRRig, LineRenderer[]> _boneRenderers = new Dictionary<VRRig, LineRenderer[]>();
        private static Dictionary<VRRig, LineRenderer[]> _armRenderers = new Dictionary<VRRig, LineRenderer[]>();

        public static int[] bones = new int[]
        {
    4, 3, 5, 4, 19, 18, 20, 19, 3, 18, 21, 20, 22, 21, 25, 21, 29, 21, 31, 29, 27, 25, 24, 22, 6, 5, 7, 6, 10, 6, 14, 6, 16, 14, 12, 10, 9, 7
        };

        private static float _rainbowHue = 0f;

        public static void DrawBones()
        {
            _rainbowHue += Time.deltaTime * 0.5f;
            if (_rainbowHue > 1f) _rainbowHue -= 1f;

            foreach (NetPlayer netPlayer in NetworkSystem.Instance.PlayerListOthers)
            {
                VRRig rig = RigManager.getVRRig(netPlayer);
                if (rig == null || rig.isOfflineVRRig || rig.isMyPlayer) continue;

                if (!_spineRenderers.ContainsKey(rig))
                    _spineRenderers[rig] = CreateLineRenderer(Color.white, 0.02f);

                if (!_boneRenderers.ContainsKey(rig))
                {
                    int boneCount = bones.Length / 2;
                    LineRenderer[] lrs = new LineRenderer[boneCount];
                    for (int i = 0; i < boneCount; i++)
                        lrs[i] = CreateLineRenderer(Color.white, 0.02f);
                    _boneRenderers[rig] = lrs;
                }

                if (!_armRenderers.ContainsKey(rig))
                {
                    _armRenderers[rig] = new LineRenderer[]
                    {
                CreateLineRenderer(Color.white, 0.025f),
                CreateLineRenderer(Color.white, 0.025f)
                    };
                }

                Transform[] skBones = rig.mainSkin.bones;
                if (skBones == null || skBones.Length == 0) continue;

                int totalLines = 1 + (bones.Length / 2) + 2;

                float hue = Mathf.Repeat(_rainbowHue + (0f / totalLines), 1f);
                if (_spineRenderers.TryGetValue(rig, out var spineLr))
                {
                    Vector3 headPos = rig.head.rigTarget.transform.position;
                    spineLr.SetPosition(0, headPos + Vector3.up * 0.16f);
                    spineLr.SetPosition(1, headPos + Vector3.down * 0.4f);

                    spineLr.material.color = Color.HSVToRGB(hue, 1f, 1f);
                }

                if (_boneRenderers.TryGetValue(rig, out var boneLrs))
                {
                    for (int i = 0; i < boneLrs.Length; i++)
                    {
                        int idx = i * 2;
                        if (bones[idx] >= skBones.Length || bones[idx + 1] >= skBones.Length) continue;
                        boneLrs[i].SetPosition(0, skBones[bones[idx]].position);
                        boneLrs[i].SetPosition(1, skBones[bones[idx + 1]].position);

                        boneLrs[i].material.color = Color.HSVToRGB(hue, 1f, 1f);
                    }
                }

                if (_armRenderers.TryGetValue(rig, out var armLrs))
                {
                    if (bones[10] < skBones.Length && bones[11] < skBones.Length)
                    {
                        armLrs[0].SetPosition(0, skBones[bones[10]].position);
                        armLrs[0].SetPosition(1, skBones[bones[11]].position);

                        armLrs[0].material.color = Color.HSVToRGB(hue, 1f, 1f);
                    }
                    if (bones[6] < skBones.Length && bones[7] < skBones.Length)
                    {
                        armLrs[1].SetPosition(0, skBones[bones[6]].position);
                        armLrs[1].SetPosition(1, skBones[bones[7]].position);

                        armLrs[1].material.color = Color.HSVToRGB(hue, 1f, 1f);
                    }
                }
            }

            CleanupRigs();
        }

        private static void CleanupRigs()
        {
            var activeRigs = new HashSet<VRRig>();
            foreach (NetPlayer p in NetworkSystem.Instance.PlayerListOthers)
            {
                VRRig r = RigManager.getVRRig(p);
                if (r != null) activeRigs.Add(r);
            }

            foreach (var key in new List<VRRig>(_spineRenderers.Keys))
            {
                if (!activeRigs.Contains(key))
                {
                    UnityEngine.Object.Destroy(_spineRenderers[key].gameObject);
                    _spineRenderers.Remove(key);
                }
            }

            foreach (var key in new List<VRRig>(_boneRenderers.Keys))
            {
                if (!activeRigs.Contains(key))
                {
                    foreach (var lr in _boneRenderers[key])
                        UnityEngine.Object.Destroy(lr.gameObject);
                    _boneRenderers.Remove(key);
                }
            }

            foreach (var key in new List<VRRig>(_armRenderers.Keys))
            {
                if (!activeRigs.Contains(key))
                {
                    foreach (var lr in _armRenderers[key])
                        UnityEngine.Object.Destroy(lr.gameObject);
                    _armRenderers.Remove(key);
                }
            }
        }

        private static LineRenderer CreateLineRenderer(Color color, float width)
        {
            GameObject go = new GameObject("BoneLine");
            LineRenderer lr = go.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.startWidth = width;
            lr.endWidth = width;
            lr.material = new Material(Shader.Find("GUI/Text Shader"));
            lr.material.color = color;
            lr.useWorldSpace = true;
            return lr;
        }
        public static float rainbowHuetr = 0f;
        public static void Tracers()
        {
            if (PhotonNetwork.CurrentRoom == null) return;

            foreach (NetPlayer netPlayer in NetworkSystem.Instance.AllNetPlayers)
                {
                    Theme theme = Themes.List[Main.ThemeValue];
                    Vector3 start = handboi ? GorillaTagger.Instance.offlineVRRig.rightHandTransform.position : GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.position;
                    GameObject lines = new GameObject("Line");
                    LineRenderer lr = lines.AddComponent<LineRenderer>();
                rainbowHuetr += Time.deltaTime * 0.5f;
                    if (rainbowHuetr > 1f) rainbowHuetr -= 1f;

                int totalLines = 1 + (bones.Length / 2) + 2;

                float hue = Mathf.Repeat(_rainbowHue + (0f / totalLines), 1f);
                lr.material.color = Color.HSVToRGB(hue, 1f, 1f);
                    lr.startWidth = 0.01f;
                    lr.endWidth = 0.01f;
                    lr.positionCount = 2;
                    lr.useWorldSpace = true;
                    lr.SetPosition(0, start);
                    lr.SetPosition(1, RigManager.getVRRig(netPlayer).transform.position);
                    lr.material.shader = Shader.Find("GUI/Text Shader");
                    GameObject.Destroy(lr, Time.deltaTime);
                    GameObject.Destroy(lines, Time.deltaTime);
                }
            
        }
        public static void ScintillaTracers()
        {
            if (PhotonNetwork.CurrentRoom == null) return;

            foreach (NetPlayer netPlayer in NetworkSystem.Instance.AllNetPlayers)
                {
                    Vector3 start = Vector3.zero;
                    GameObject lines = new GameObject("Line2");
                    LineRenderer lr = lines.AddComponent<LineRenderer>();
                    rainbowHuetr += Time.deltaTime * 0.5f;
                    if (rainbowHuetr > 1f) rainbowHuetr -= 1f;

                    int totalLines = 1 + (bones.Length / 2) + 2;
                 
                    float hue = Mathf.Repeat(_rainbowHue + (0f / totalLines), 1f);
                    lr.material.color = Color.HSVToRGB(hue, 1f, 1f);
                    lr.startWidth = 0.01f;
                    lr.endWidth = 0.01f;
                    lr.positionCount = 2;
                    lr.useWorldSpace = true;
                    lr.SetPosition(0, start);
                    lr.SetPosition(1, RigManager.getVRRig(netPlayer).transform.position);
                    lr.material.shader = Shader.Find("GUI/Text Shader");
                    GameObject.Destroy(lr, Time.deltaTime);
                    GameObject.Destroy(lines, Time.deltaTime);
                
            }
        }
        public static bool handboi = false;
        public static void test()
        {
            TagEffectsLibrary.PlayEffect(
                GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform,
                true,
                GorillaLocomotion.GTPlayer.Instance.scale,
                TagEffectsLibrary.EffectType.THIRD_PERSON,
                GetTagEffectFromNetworkView(GorillaTagger.Instance.myVRRig),
                GetTagEffectFromNetworkView(GorillaTagger.Instance.myVRRig),
                GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.rotation
                );
        }
        public static void test2()
        {
            TagEffectsLibrary.PlayEffect(
                GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform,
                true,
                GorillaLocomotion.GTPlayer.Instance.scale,
                TagEffectsLibrary.EffectType.HIGH_FIVE,
                GetTagEffectFromNetworkView(GorillaTagger.Instance.myVRRig),
                GetTagEffectFromNetworkView(GorillaTagger.Instance.myVRRig),
                GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.rotation
                );
        }
        public static void test3()
        {
            TagEffectsLibrary.PlayEffect(
                GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform,
                true,
                GorillaLocomotion.GTPlayer.Instance.scale,
                TagEffectsLibrary.EffectType.FIRST_PERSON,
                GetTagEffectFromNetworkView(GorillaTagger.Instance.myVRRig),
                GetTagEffectFromNetworkView(GorillaTagger.Instance.myVRRig),
                GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.rotation
                );
        }
        public static TagEffectPack GetTagEffectFromNetworkView(NetworkView view)
        {
            if (view == null) return null;

            VRRig rig = view.GetComponent<VRRig>();
            if (rig == null) return null;

            return rig.GetComponent<TagEffectPack>();
        }
        public static int GetFpsOfPlayer(VRRig v)
        {
            return Traverse.Create(v).Field("fps").GetValue<int>();
        }
        public static void NameTagESP()
        {
            foreach (NetPlayer netPlayer in NetworkSystem.Instance.PlayerListOthers)
            {
                VRRig rig = RigManager.getVRRig(netPlayer);
                if (rig.isOfflineVRRig || rig.isMyPlayer || rig.isLocal)
                    continue;

                Transform existing = rig.transform.Find("Name Mod");
                GameObject root = existing ? existing.gameObject : new GameObject("Name Mod");

                root.transform.SetParent(rig.transform, false);
                root.transform.position = rig.headConstraint.position + Vector3.up * 0.5f;
                root.transform.LookAt(Camera.main.transform.position);
                root.transform.Rotate(0f, 180f, 0f);
                root.transform.localScale = Vector3.one * 0.06f;

                TextMeshPro tmp = root.GetComponent<TextMeshPro>();
                if (tmp == null)
                    tmp = root.AddComponent<TextMeshPro>();

                int fps = GetFpsOfPlayer(rig);
                string fpsColor =
                    fps < 20 ? "#ff4c4c" :
                    fps < 45 ? "#ffd84c" :
                    fps < 75 ? "#6bff6b" :
                               "#4cb7ff";

                tmp.text =
                    $"<b>{rig.Creator.NickName}</b>\n" +
                    $"<size=80%>FPS: <color={fpsColor}>{fps}</color></size>";

                tmp.fontSize = 12f;
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.enableWordWrapping = false;
                tmp.color = Color.white;
                tmp.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");

                tmp.ForceMeshUpdate();

                Transform bg = root.transform.Find("BG");
                GameObject background;

                if (bg == null)
                {
                    background = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    background.name = "BG";
                    background.transform.SetParent(root.transform, false);
                    GameObject.Destroy(background.GetComponent<Collider>());
                }
                else
                {
                    background = bg.gameObject;
                }

                Vector2 size = tmp.GetRenderedValues(false);
                background.transform.localScale = new Vector3(size.x + 5f, size.y + 3f, 1f);
                background.transform.localPosition = new Vector3(0f, size.y / 2f - 1f, 0.01f);

                Renderer renderer = background.GetComponent<Renderer>();
                renderer.material.SetFloat("_Mode", 3f);
                renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                renderer.material.SetInt("_ZWrite", 0);
                renderer.material.DisableKeyword("_ALPHATEST_ON");
                renderer.material.EnableKeyword("_ALPHABLEND_ON");
                renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                renderer.material.renderQueue = 3000;
                Color c = Color.black;
                c.a = 0.6f;
                renderer.material.color = c;
                
                //renderer.material = new Material(Shader.Find("Unlit/Color"));

                Renderer textRenderer = tmp.renderer;
                textRenderer.material.shader = Shader.Find("TextMeshPro/Distance Field");
                textRenderer.material.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.25f);
                textRenderer.material.SetColor(ShaderUtilities.ID_OutlineColor, new Color(0f, 0f, 0f, 0.8f));

                GameObject.Destroy(root, Time.deltaTime + 0.5f);
            }
        }
        public static void DayTime()
        {
            BetterDayNightManager.instance.SetTimeOfDay(3);
        }
        public static void MorningTime()
        {
            BetterDayNightManager.instance.SetTimeOfDay(1);
        }
        public static void NightTime()
        {
            BetterDayNightManager.instance.SetTimeOfDay(0);
        }
        public static void EnableAllRain()
        {
            for (int i = 0; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
            {
                BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.Raining;
            }
        }
        public static void DisableAllRain()
        {
            for (int i = 0; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
            {
                BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.None;
            }
        }
        public static void ChamsSelf()
        {
            if (PhotonNetwork.CurrentRoom != null)
            {
                 VRRig.LocalRig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                 Color c = Color.Lerp(Color.red, Color.black, Mathf.PingPong(Time.time, 1));
                 c.a = 0.3f;
                 VRRig.LocalRig.mainSkin.material.color = c;
            }
            else
            {
                 VRRig.LocalRig.mainSkin.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                 Color c = GorillaTagger.Instance.offlineVRRig.playerColor;
                 c.a = 1f;
                 VRRig.LocalRig.mainSkin.material.color = c;
            }
        }
        public static void Chams()
        {
            if (PhotonNetwork.CurrentRoom != null)
            {
                foreach (NetPlayer netPlayer in NetworkSystem.Instance.PlayerListOthers)
                {
                    VRRig rig = RigManager.getVRRig(netPlayer);
                    if (!rig.isOfflineVRRig && !rig.isMyPlayer)
                    {
                        rig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                        Color c = Color.Lerp(Color.red, Color.black, Mathf.PingPong(Time.time, 1));
                        c.a = 0.3f; // 0 = invisible, 1 = solid
                        rig.mainSkin.material.color = c;
                    }
                }
            }
            else
            {
                foreach (NetPlayer netPlayer in NetworkSystem.Instance.PlayerListOthers)
                {
                    VRRig i = RigManager.getVRRig(netPlayer);
                    i.mainSkin.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                    i.mainSkin.GetComponent<Renderer>().material.color = i.playerColor;
                    Color c = i.playerColor;
                    c.a = 1f;
                    i.mainSkin.material.color = c;
                }
            }
        }
        public static void FlipChams()
        {
            foreach (NetPlayer netPlayer in NetworkSystem.Instance.AllNetPlayers)
            {
                VRRig i = RigManager.getVRRig(netPlayer);
                i.mainSkin.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                i.mainSkin.GetComponent<Renderer>().material.color = i.playerColor;
                Color c = i.playerColor;
                c.a = 1f;
                i.mainSkin.material.color = c;
            }
        }
    }
}
