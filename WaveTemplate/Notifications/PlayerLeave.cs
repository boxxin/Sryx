using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using StupidTemplate.Notifications;
using UnityEngine;

namespace StupidTemplate.Patches
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerLeftRoom")]
    public class LeavePatch : MonoBehaviour
    {
        private static void Prefix(Player otherPlayer)
        {
            if (otherPlayer != PhotonNetwork.LocalPlayer && otherPlayer != a)
            {
                s2.NotifiLib.SendNotification("plasma.room > left: " + otherPlayer.NickName + "", s2.NotifiLib.NotifiReason.RoomLeft);
                a = otherPlayer;
            }
        }

        private static Player a;
    }
}