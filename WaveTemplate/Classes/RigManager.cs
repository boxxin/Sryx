using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace StupidTemplate.Classes
{
    public class RigManager
    {
        public static PhotonView getView(object obj)
        {
            if (obj is VRRig) return (PhotonView)Traverse.Create((VRRig)obj).Field("photonView").GetValue();
            if (obj is Player) return (PhotonView)Traverse.Create(getVRRig((Player)obj)).Field("photonView").GetValue();
            if (obj is NetPlayer) return (PhotonView)Traverse.Create(getPlayer((NetPlayer)obj)).Field("photonView").GetValue();

            return null;
        }
        public static VRRig getVRRig(object obj)
        {
            if (obj is Player) { return GorillaGameManager.instance.FindPlayerVRRig(getNetPlayer((Player)obj)); }
            if (obj is NetPlayer) { return GorillaGameManager.instance.FindPlayerVRRig((NetPlayer)obj); }

            return null;
        }
        public static Player getPlayer(object obj)
        {
            if (obj is VRRig) return getView((VRRig)obj).Owner;
            if (obj is NetPlayer) return ((NetPlayer)obj).GetPlayerRef();

            return null;
        }
        public static NetPlayer getNetPlayer(object obj)
        {
            if (obj is VRRig) return (NetPlayer)Traverse.Create(getPlayer((VRRig)obj)).Field("netPlayer").GetValue();
            if (obj is Player) return (NetPlayer)Traverse.Create((Player)obj).Field("netPlayer").GetValue();

            return null;
        }
        public static VRRig GetVRRigFromPlayer(Player p) =>
            GorillaGameManager.instance.FindPlayerVRRig(p);
        public static VRRig GetClosestVRRig()
        {
            float num = float.MaxValue;
            VRRig outRig = null;
            foreach (VRRig rig in GameObject.FindObjectsOfType<VRRig>())
            {
                if (Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, rig.transform.position) < num)
                {
                    num = Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, rig.transform.position);
                    outRig = rig;
                }
            }
            return outRig;
        }

        public static PhotonView GetPhotonViewFromVRRig(VRRig p) =>
            (PhotonView)Traverse.Create(p).Field("photonView").GetValue();

        public static Player GetRandomPlayer(bool includeSelf)
        {
            if (includeSelf)
                return PhotonNetwork.PlayerList[Random.Range(0, PhotonNetwork.PlayerList.Length - 1)];
            else
                return PhotonNetwork.PlayerListOthers[Random.Range(0, PhotonNetwork.PlayerListOthers.Length - 1)];
        }

        public static Player GetPlayerFromVRRig(VRRig p) =>
            GetPhotonViewFromVRRig(p).Owner;

        public static Player GetPlayerFromID(string id)
        {
            Player found = null;
            foreach (Player target in PhotonNetwork.PlayerList)
            {
                if (target.UserId == id)
                {
                    found = target;
                    break;
                }
            }
            return found;
        }

        public static Color GetPlayerColor(VRRig Player)
        {
            switch (Player.setMatIndex)
            {
                case 1:
                    return Color.red;
                case 2:
                case 11:
                    return new Color32(255, 128, 0, 255);
                case 3:
                case 7:
                    return Color.blue;
                case 12:
                    return Color.green;
                default:
                    return Player.playerColor;
            }
        }
    }
}