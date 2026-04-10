using GorillaLocomotion.Swimming;
using GorillaNetworking;
using Photon.Pun;
using plamsa.Patches.Internal;
using StupidTemplate.Menu;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StupidTemplate.Mods
{
    internal class idk
    {
        public static string CosmeticsOwned;
        public static bool hasGivenCosmetics;
        public static void UnlockAllCosmetics()
        {
            CosmeticPatch.enabled = true;
            if (!PostGetData.CosmeticsInitialized || hasGivenCosmetics) return;
            hasGivenCosmetics = true;
            MethodInfo unlockItem = typeof(CosmeticsController).GetMethod("UnlockItem", BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var item in CosmeticsController.instance.allCosmetics.Where(item => !CosmeticsController.instance.concatStringCosmeticsAllowed.Contains(item.itemName)))
            {
                try
                {
                    unlockItem.Invoke(CosmeticsController.instance, new object[] { item.itemName, false });
                }
                catch
                {
                }
            }
        }
        public static void StumpText()
        {
            GameObject gameObject = new GameObject("STUMPOBJ");
            TextMeshPro textMeshPro = gameObject.AddComponent<TextMeshPro>();
            textMeshPro.text = "Sryx <color=grey>||</color> Version: " + PluginInfo.Version;
            textMeshPro.fontSize = 2f;
            textMeshPro.alignment = TextAlignmentOptions.Center;
            textMeshPro.color = Color.blue;
            textMeshPro.font = StupidTemplate.Settings.currentFontTMP;
            TextMeshPro component = gameObject.GetComponent<TextMeshPro>();
            component.ForceMeshUpdate(false, false);
            TMP_TextInfo textInfo = component.textInfo;
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                TMP_CharacterInfo tmp_CharacterInfo = textInfo.characterInfo[i];
                if (tmp_CharacterInfo.isVisible)
                {
                    float num = (Time.time * 0.5f + (float)i * 0.05f) % 1f;
                    Color color = Color.HSVToRGB(num, 1f, 1f);
                    int vertexIndex = tmp_CharacterInfo.vertexIndex;
                    int materialReferenceIndex = tmp_CharacterInfo.materialReferenceIndex;
                    Color32[] colors = textInfo.meshInfo[materialReferenceIndex].colors32;
                    colors[vertexIndex] = color;
                    colors[vertexIndex + 1] = color;
                    colors[vertexIndex + 2] = color;
                    colors[vertexIndex + 3] = color;
                }
            }
            for (int j = 0; j < textInfo.meshInfo.Length; j++)
            {
                textInfo.meshInfo[j].mesh.colors32 = textInfo.meshInfo[j].colors32;
                component.UpdateGeometry(textInfo.meshInfo[j].mesh, j);
            }
            Transform transform = gameObject.transform;
            transform.GetComponent<TextMeshPro>().renderer.material.shader = Shader.Find("GUI/Text Shader");
            transform.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            transform.position = new Vector3(-63.5511f, 12.2094f, -82.6264f);
            transform.LookAt(Camera.main.transform.position);
            transform.Rotate(0f, 180f, 0f);
            UnityEngine.Object.Destroy(gameObject, Time.deltaTime);
        }

        public static void GetAllRPCS()
        {
            string bar = "RPC's";
            int num = 0;
            foreach (string rpclist in PhotonNetwork.PhotonServerSettings.RpcList)
            {
                try
                {
                    bar += "\n--------------------------\n";
                    bar = bar + num.ToString() + rpclist;
                }
                catch { }
                num++;
            }
            bar += "\n--------------------------\n";
            File.WriteAllText("plasma/RPCs.txt", bar);
            string path = Path.Combine(Assembly.GetExecutingAssembly().Location, "plasma/RPCs.txt");
            path = path.Split("BepInEx\\", 0)[0] + "plasma/RPCs.txt";
        }
        public static bool RequestOwnershipOfThrowableBugAntiRepeat = false;
        public static void GetThrowableBugWithOwnership(Vector3 Position, bool input)
        {
            if (input)
            {
                foreach (ThrowableBug bug in Resources.FindObjectsOfTypeAll<ThrowableBug>())
                {
                    if (!RequestOwnershipOfThrowableBugAntiRepeat)
                    {
                        bug.ownerRig = VRRig.LocalRig;
                        bug.WorldShareableRequestOwnership();
                        bug.transform.position = Position;
                        RequestOwnershipOfThrowableBugAntiRepeat = true;
                    }
                }
            }
            else { RequestOwnershipOfThrowableBugAntiRepeat = false; }
        }
        public static void JoinRoomWithPartyMembers(string roomID)
        {
            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(roomID, JoinType.ForceJoinWithParty);
        }
        public static void lagself()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                 GameObject.Instantiate(Main.menu);
            }
            if (ControllerInputPoller.instance.leftGrab)
            {
                GameObject.Destroy(Main.menu);
            }
        }

        #region Water Shit
        private static void SendSplashEffectRPC(Vector3 p, Quaternion r)
        {
            WaterParameters waterParameters = ScriptableObject.CreateInstance<WaterParameters>();
            GorillaTagger.Instance?.myVRRig?.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { p, r, 1.3f, waterParameters.splashEffectScale, false, false });
        }
        public static void WaterHead()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                SendSplashEffectRPC(GorillaLocomotion.GTPlayer.Instance.headCollider.transform.position, GorillaLocomotion.GTPlayer.Instance.headCollider.transform.rotation);
            }
        }
        public static void Cum()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                SendSplashEffectRPC(GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.position, GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.rotation);
            }
        }
        public static void WaterAura()
        {
            float f = Time.time * 30f;
            Vector3 b = new Vector3(Mathf.Cos(f), 0f, Mathf.Sin(f)) * 1f;
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                SendSplashEffectRPC(GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.position + b, GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.rotation);
            }
        }
        public static void ReverseWaterAura()
        {
            float f = Time.time * -30f;
            Vector3 b = new Vector3(Mathf.Cos(f), 0f, Mathf.Sin(f)) * -1f;
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                SendSplashEffectRPC(GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.position + b, GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.rotation);
            }
        }
        public static void ReverseWaterAuraV2()
        {
            float f3 = Time.time * 30f;
            Vector3 b3 = new Vector3(0f, Mathf.Cos(f3), Mathf.Sin(f3)) * 1f;
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                SendSplashEffectRPC(GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.position + b3, GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.rotation);
            }
        }
        public static void WaterHands()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                SendSplashEffectRPC(GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.position, GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.rotation);
            }
            if (ControllerInputPoller.instance.leftGrab || Mouse.current.leftButton.isPressed)
            {
                SendSplashEffectRPC(GorillaLocomotion.GTPlayer.Instance.LeftHand.controllerTransform.position, GorillaLocomotion.GTPlayer.Instance.LeftHand.controllerTransform.rotation);
            }
        }
        #endregion

        #region Boards

        private static DateTime menuLoadTime = DateTime.Now;
        public static void Boards()
        {
            try { var tmp = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdBodyText").GetComponent<TextMeshPro>(); tmp.richText = true; TimeSpan uptime = DateTime.Now - menuLoadTime; string uptimeStr = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)uptime.TotalHours, uptime.Minutes, uptime.Seconds); string playerName = PhotonNetwork.LocalPlayer.NickName; string room = PhotonNetwork.InRoom ? PhotonNetwork.CurrentRoom.Name : "Not In Room"; int players = PhotonNetwork.InRoom ? PhotonNetwork.CurrentRoom.PlayerCount : 0; tmp.text = "=========================================================================\nName: " + playerName + "\nRoom: " + room + "\nPlayers: " + players + "\nUptime: " + uptimeStr + "\nStatus: <#00FF00>Undetected</color>\n\n<#FF0000>If You Want To Open The Menu On Pc Press Q</color>\n========================================================================="; } catch { }
            try { string[] currentSpin = { "-", "/", "|", "\\" }; int spinnerSpeed = Mathf.FloorToInt(Time.time * 3f) % currentSpin.Length; string spinner = currentSpin[spinnerSpeed]; GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdHeadingText").GetComponent<TextMeshPro>().text = $"[{spinner}] " + PluginInfo.Name + $" [{spinner}]"; } catch { }
            try { string[] currentSpin = { "-", "/", "|", "\\" }; int spinnerSpeed = Mathf.FloorToInt(Time.time * 3f) % currentSpin.Length; string spinner = currentSpin[spinnerSpeed]; GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText").GetComponent<TextMeshPro>().text = $"[{spinner}] " + PluginInfo.Name + $" [{spinner}]"; } catch { }
            try { string[] currentSpin = { "-", "/", "|", "\\" }; int spinnerSpeed = Mathf.FloorToInt(Time.time * 3f) % currentSpin.Length; string spinner = currentSpin[spinnerSpeed]; GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/MapInfo_TMP").GetComponent<TextMeshPro>().text = $"[{spinner}] " + PluginInfo.Name + $" [{spinner}]"; } catch { }
            try { var tmp = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData").GetComponent<TextMeshPro>(); tmp.richText = true; tmp.text = "==============================================\n\nWelcome To " + PluginInfo.Name + " Mod Menu! We Are A Free And Open Source Mod Menu\n<#FF0000>If You Have Problem On The Menu Or You Have A Suggestion, Check Out Our Discord !\nIf You Get Banned With 1 Mod On This Menu, Please Report The Detected Mod In The Discord !</color>\nYou Know Everything About The Menu\nNow Have Fun With " + PluginInfo.Name + "\n\n=============================================="; } catch { }
            if (Main.BoardMat == null) return;
            string[] Path = new string[]
            {
                "Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/GorillaComputerObject/ComputerUI/monitor/monitorScreen",
                "Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomBoundaryStones/BoundaryStoneSet_Forest/wallmonitorforestbg",
            };
            for (int i = 0; i < Path.Length; i++)
            {
                try
                {
                    GameObject obj = GameObject.Find(Path[i]);
                    if (obj != null)
                    {
                        Renderer ren = obj.GetComponent<Renderer>();
                        if (ren != null) ren.material = Main.BoardMat;
                    }
                }
                catch { }
            }
            Main.GetOtherBoards();
        }
        #endregion
    }
}
