using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using GorillaLocomotion;
using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.Events;
using PlayFab.Internal;
using StupidTemplate.Classes;
using StupidTemplate.Notifications;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR;
using static StupidTemplate.Classes.RigManager;
using static StupidTemplate.Menu.Main;
using Debug = UnityEngine.Debug;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Object = UnityEngine.Object;

namespace StupidTemplate.Mods
{
    public class Safety
    {
        public static void RpcUnlimter()
        {
            Type type = GameObject.Find("GorillaReporter").GetComponent<MonkeAgent>().GetType();
            Traverse traverse = Traverse.Create(type);
            if (traverse.Field<int>("calls").Value != 0)
            {
                traverse.Field<int>("calls").Value = 0;
            }
            if (traverse.Field<int>("rpcCallLimit").Value != int.MaxValue)
            {
                traverse.Field<int>("rpcCallLimit").Value = int.MaxValue;
                GameObject.Find("GorillaReporter").GetComponent<MonkeAgent>().rpcCallLimit = int.MaxValue;
            }
            if (traverse.Field<int>("rpcErrorMax").Value != int.MaxValue)
            {
                traverse.Field<int>("rpcErrorMax").Value = int.MaxValue;
                GameObject.Find("GorillaReporter").GetComponent<MonkeAgent>().rpcErrorMax = int.MaxValue;
            }
            if (traverse.Field<int>("lowestActorNumber").Value != NetworkSystem.Instance.LocalPlayer.ActorNumber)
            {
                traverse.Field<int>("lowestActorNumber").Value = NetworkSystem.Instance.LocalPlayer.ActorNumber;
            }
            Traverse traverse2 = Traverse.Create(typeof(FXSystem));
            Traverse traverse3 = traverse2.Method("CheckCallSpam", new object[]
            {
                new FXSystemSettings(),
                0,
                0.0
            });
            CallLimitType<CallLimiter> value = traverse3.Field("callLimitType").GetValue<CallLimitType<CallLimiter>>();
            Traverse.Create(StructWrapperUtility.Get<CallLimiter>(value).GetType()).Field<bool>("blockCall").Value = false;
        }

        private static Hashtable rpcFilterByViewId = new Hashtable();
        public static void Flush()
        {
            rpcFilterByViewId[0] = GorillaTagger.Instance.myVRRig.ViewID;
            MethodInfo method = typeof(MonkeAgent).GetMethod("RefreshRPCs", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(MonkeAgent.instance, null);
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                CachingOption = (EventCaching)6,
                TargetActors = new int[]
                {
                    PhotonNetwork.LocalPlayer.ActorNumber
                }
            };
            PhotonNetwork.NetworkingClient.OpRaiseEvent(200, rpcFilterByViewId, raiseEventOptions, SendOptions.SendReliable);
        }
        public static void BypassVCBan()
        {
            GorillaTagger.moderationMutedTime = -1f;
            GorillaTelemetry.PostNotificationEvent("Unmute");
            GorillaTagger.Instance.myRecorder.TransmitEnabled = true;
            if (KIDManager.Instance != null)
            {
                GameObject.Destroy(KIDManager.Instance);
            }
        }
        public static void AntiBan() 
        {
            if (PhotonNetwork.InRoom)
            {
                if (!IsRPCPatched)
                {
                    MonkeAgent.instance.rpcErrorMax = int.MaxValue;
                    MonkeAgent.instance.rpcCallLimit = int.MaxValue;
                    MonkeAgent.instance.logErrorMax = int.MaxValue;
                    PhotonNetwork.MaxResendsBeforeDisconnect = int.MaxValue;
                    PhotonNetwork.QuickResends = int.MaxValue;
                    PhotonNetwork.SendAllOutgoingCommands();
                    IsRPCPatched = true;
                }
                else
                {
                    LoadBalancingPeer peer = PhotonNetwork.NetworkingClient.LoadBalancingPeer;
                    Type type = peer.GetType();
                    FieldInfo field = type.GetField("ResentReliableCommands", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (field != null)
                    {
                        field.SetValue(peer, 0);
                    }
                    MethodInfo methodClearQueue = type.GetMethod("ClearReliableChannel", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (methodClearQueue != null)
                    {
                        methodClearQueue.Invoke(peer, null);
                    }
                    peer = null;
                    type = null;
                    field = null;
                    methodClearQueue = null;

                    if (DateTime.UtcNow - lastAntiBanCall < antiBanInterval) return;
                    lastAntiBanCall = DateTime.UtcNow;
                    if (!PhotonNetwork.IsConnected)
                    {
                        mockContext = null;
                        typeof(PlayFabAuthenticationAPI).GetField("_authenticationContext", BindingFlags.Static | BindingFlags.NonPublic)?.SetValue(null, null);
                        PlayFabHttp.ClearAllEvents();
                        return;
                    }
                    if (!PlayFabAuthenticationAPI.IsEntityLoggedIn()) return;
                    if (mockContext == null)
                    {
                        mockContext = new PlayFabAuthenticationContext
                        {
                            ClientSessionTicket = Guid.NewGuid().ToString(),
                            EntityId = "MOCK_" + Guid.NewGuid().ToString("N").Substring(0, 8),
                            PlayFabId = "MOCK_" + Guid.NewGuid().ToString("N").Substring(0, 8),
                            EntityToken = "MOCK_" + Guid.NewGuid().ToString(),
                            EntityType = "_GorillaPlayer"
                        };
                    }
                    try
                    {
                        PlayFabAuthenticator.instance.mothershipAuthenticator.MaxMetaLoginAttempts = int.MaxValue;
                        PlayFabAuthenticator.instance.mothershipAuthenticator.BeginLoginFlow();
                        PlayFabAuthenticatorSettings.TitleId = "A-X^0";
                        PlayFabClientAPI.ExecuteCloudScript(new PlayFab.ClientModels.ExecuteCloudScriptRequest
                        {
                            FunctionName = "A-x^0",
                            GeneratePlayStreamEvent = false,
                            AuthenticationContext = mockContext,
                            FunctionParameter = new Dictionary<string, object>
                        {
                            { "AuthenticateWithPlayFab", true },
                            { "OnSerialize", false },
                            { "OnEnable", false }
                        }

                        }, result => { }, error => { });
                        PlayFabHttp.CreateInstance();
                        PlayFabHttp.ClearAllEvents();
                        typeof(PlayFabEvents).GetMethod("Init", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, null);
                        typeof(PlayFabAuthenticationAPI).GetField("_authenticationContext", BindingFlags.Static | BindingFlags.NonPublic)?.SetValue(null, mockContext);
                        PlayFabSettings.RequestTimeout = 30000;
                        PlayFabSettings.CompressApiData = true;
                    }
                    catch { }
                }
            }
        }

        public static void AntiRPCKick()
        {
            try
            {
                AntiRPCKicker();

                Type gorillaNotType = typeof(MonkeAgent);
                MonkeAgent gorillaInstance = MonkeAgent.instance;

                if (gorillaInstance == null)
                {
                    Debug.LogWarning("MonkeAgent.instance is null — cannot clear RPCs.");
                    return;
                }

                MonkeAgent.instance.rpcErrorMax = int.MaxValue;
                MonkeAgent.instance.rpcCallLimit = int.MaxValue;
                MonkeAgent.instance.logErrorMax = int.MaxValue;
                PhotonNetwork.MaxResendsBeforeDisconnect = int.MaxValue;
                PhotonNetwork.QuickResends = int.MaxValue;

                ValueTuple<Type, object, string, bool>[] targets = new ValueTuple<Type, object, string, bool>[]
                {
                    (gorillaNotType, gorillaInstance, "rpcErrorMax", false),
                    (gorillaNotType, gorillaInstance, "rpcCallLimit", false),
                    (gorillaNotType, gorillaInstance, "logErrorMax", false),
                    (typeof(PhotonNetwork), null, "QuickResends", true),
                    (typeof(PhotonNetwork), null, "MaxResendsBeforeDisconnect", true)
                };

                foreach (ValueTuple<Type, object, string, bool> entry in targets)
                {
                    if (!TrySetIntMember(entry.Item1, entry.Item2, entry.Item3, int.MaxValue, entry.Item4))
                    {
                        Debug.LogWarning(string.Concat(new string[]
                        {
                            "Could not set '",
                            entry.Item3,
                            "' on ",
                            entry.Item1.FullName,
                            "."
                        }));
                    }
                }

                PhotonNetwork.NetworkingClient.OpRaiseEvent(200, new Hashtable()
                {
                    { 0, GorillaTagger.Instance.myVRRig.ViewID }
                }, new RaiseEventOptions
                {
                    CachingOption = (EventCaching)6,
                    TargetActors = new int[] { PhotonNetwork.LocalPlayer.ActorNumber }
                }, SendOptions.SendReliable);

                if (Time.time > rpcDel)
                {
                    try
                    {
                        rpcDel = Time.time + 0.47f;
                        PhotonNetwork.RemoveBufferedRPCs(int.MaxValue, null, null);
                        PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
                        PhotonNetwork.OpCleanActorRpcBuffer(PhotonNetwork.LocalPlayer.ActorNumber);
                        PhotonNetwork.OpCleanRpcBuffer(GorillaTagger.Instance.myVRRig.GetView);
                        MonkeAgent.instance.OnPlayerLeftRoom(PhotonNetwork.LocalPlayer);
                        PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOutgoingCommands();

                        Traverse yeah = Traverse.Create(typeof(PhotonNetwork));
                        yeah.Property("ResentReliableCommands").SetValue(0);

                        PhotonNetwork.NetworkingClient.Service();
                        PhotonNetwork.NetworkingClient.OpChangeGroups(null, new byte[] { 1, 2, 3, 4 });
                        PhotonNetwork.NetworkingClient.LoadBalancingPeer.TrafficStatsReset();

                        try
                        {
                            var system = AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "Assembly-CSharp").GetType("RoomSystem");
                            system.GetMethod("OnPlayerLeftRoom", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(null, new object[] { NetworkSystem.Instance.LocalPlayer });
                        }
                        catch { }

                        try
                        {
                            NetSystemState state = new NetSystemState();
                            PeerStateValue val = new PeerStateValue();
                            state.Equals(NetSystemState.Connecting);
                            val.Equals(PeerStateValue.Connected);
                            RunViewUpdate();
                        }
                        catch { }

                        PhotonNetwork.SendAllOutgoingCommands();
                    }
                    catch { }
                }

                MethodInfo refresh = gorillaNotType.GetMethod("RefreshRPCs", BindingFlags.NonPublic | BindingFlags.Instance);
                if (refresh != null)
                {
                    refresh.Invoke(gorillaInstance, null);
                }
            }
            catch { }
        }
        public static VRRig reportRig;
        public static void AntiReport(System.Action<VRRig, Vector3> onReport)
        {
            if (!NetworkSystem.Instance.InRoom) return;

            if (reportRig != null)
            {
                onReport?.Invoke(reportRig, reportRig.transform.position);
                reportRig = null;
                return;
            }

            foreach (NetPlayer netPlayer in NetworkSystem.Instance.PlayerListOthers)
            {
                foreach (GorillaPlayerScoreboardLine line in GorillaScoreboardTotalUpdater.allScoreboardLines)
                {
                    if (line.linePlayer != NetworkSystem.Instance.LocalPlayer) continue;
                    Transform report = line.reportButton.gameObject.transform;

                    VRRig vrrig = RigManager.getVRRig(netPlayer);
                    if (vrrig != null && !vrrig.isLocal)
                    {
                        float D1 = Vector3.Distance(vrrig.rightHandTransform.position, report.position);
                        float D2 = Vector3.Distance(vrrig.leftHandTransform.position, report.position);
                        if (D1 < 0.35f || D2 < 0.35f)
                        {
                            onReport?.Invoke(vrrig, report.transform.position);
                        }
                    }
                }
            }
        }

        public static float antiReportDelay;
        public static void AntiReportDisconnect()
        {
            AntiReport((vrrig, position) =>
            {
                NetworkSystem.Instance.ReturnToSinglePlayer();

                if (!(Time.time > antiReportDelay)) return;
                antiReportDelay = Time.time + 1f;
                NotifiLib.SendNotification("<color=grey>[</color><color=purple>ANTI-REPORT</color><color=grey>]</color> " + GetPlayerFromVRRig(vrrig).NickName + " attempted to report you, you have been disconnected.");
            });
        }
        public static void AntiReportLag()
        {
            AntiReport((vrrig, position) =>
            {
                Overpowered.CrashPlayer(vrrig);

                if (!(Time.time > antiReportDelay)) return;
                antiReportDelay = Time.time + 1f;
                NotifiLib.SendNotification("<color=grey>[</color><color=purple>ANTI-REPORT</color><color=grey>]</color> " + GetPlayerFromVRRig(vrrig).NickName + " attempted to report you, you have been disconnected.");
            });
        }
        private static void AntiRPCKicker()
        {
            for (int i = 0; i < 1300; i++)
            {
                ResendCachedRpc();
            }

            try
            {
                if (PhotonNetwork.NetworkingClient.LoadBalancingPeer.GetType().GetField("outgoingStreamQueue", BindingFlags.Instance | BindingFlags.NonPublic) != null)
                {
                    IList list = PhotonNetwork.NetworkingClient.LoadBalancingPeer.GetType().GetField("outgoingStreamQueue", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(PhotonNetwork.NetworkingClient.LoadBalancingPeer) as IList;
                    if (PhotonNetwork.NetworkingClient.LoadBalancingPeer.GetType().GetField("outgoingStreamQueue", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(PhotonNetwork.NetworkingClient.LoadBalancingPeer) as IList != null && list.Count > 0)
                    {
                        Debug.Log(list[list.Count - 1]);
                        cachedSerializedRpc = list[list.Count - 1] as byte[];
                    }
                }
            }
            catch
            {
                cachedSerializedRpc = null;
            }
        }
        #region stuff for safety

        public static bool IsRPCPatched = false;
        public static bool visAntiReport = false;
        private static DateTime lastAntiBanCall = DateTime.MinValue;
        private static readonly TimeSpan antiBanInterval = TimeSpan.FromSeconds(5);
        private static PlayFabAuthenticationContext mockContext;

        private static byte[] cachedSerializedRpc;
        private static void ResendCachedRpc()
        {
            if (cachedSerializedRpc != null)
            {
                try
                {
                    if (PhotonNetwork.NetworkingClient.LoadBalancingPeer.GetType().GetMethod("SendReliable", BindingFlags.Instance | BindingFlags.NonPublic) != null)
                    {
                        PhotonNetwork.NetworkingClient.LoadBalancingPeer.GetType().GetMethod("SendReliable", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(PhotonNetwork.NetworkingClient.LoadBalancingPeer, new object[] { cachedSerializedRpc });
                    }
                    else if (PhotonNetwork.NetworkingClient.LoadBalancingPeer.GetType().GetMethod("SendUnreliable", BindingFlags.Instance | BindingFlags.NonPublic) != null)
                    {
                        PhotonNetwork.NetworkingClient.LoadBalancingPeer.GetType().GetMethod("SendUnreliable", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(PhotonNetwork.NetworkingClient.LoadBalancingPeer, new object[] { cachedSerializedRpc });
                    }
                }
                catch
                {
                    Console.WriteLine("if u managed to get here then u broke the code or u retard");
                    SetTick(9999f);
                }
            }
        }
        public static void SetTick(float tickMultiplier)
        {
            if (GameObject.Find("PhotonMono") != null ? GameObject.Find("PhotonMono").GetComponent<PhotonHandler>() : null != null)
            {
                Traverse.Create(GameObject.Find("PhotonMono") != null ? GameObject.Find("PhotonMono").GetComponent<PhotonHandler>() : null).Field("nextSendTickCountOnSerialize").SetValue((int)(Time.realtimeSinceStartup * tickMultiplier));
                PhotonHandler.SendAsap = true;
            }
        }
        static float rpcDel;
        private static bool TrySetIntMember(Type type, object targetInstance, string name, int value, bool isStatic)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | (isStatic ? BindingFlags.Static : BindingFlags.Instance);
            FieldInfo fi = type.GetField(name, flags);
            if (fi != null && fi.FieldType == typeof(int))
            {
                fi.SetValue(isStatic ? null : targetInstance, value);
                return true;
            }
            else
            {
                PropertyInfo pi = type.GetProperty(name, flags);
                if (pi != null && pi.PropertyType == typeof(int) && pi.CanWrite)
                {
                    pi.SetValue(isStatic ? null : targetInstance, value, null);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static object RunViewUpdate()
        {
            return typeof(PhotonNetwork).GetMethod("RunViewUpdate", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null, null);
        }
        #endregion
    }
}
