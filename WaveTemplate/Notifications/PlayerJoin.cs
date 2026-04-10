using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using StupidTemplate.Notifications;
using UnityEngine;

namespace StupidTemplate.Patches
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
    public class JoinPatch : MonoBehaviour
    {
        private static void Prefix(Player newPlayer)
        {
            if (newPlayer != oldnewplayer)
            {
                s2.NotifiLib.SendNotification("plasma.room > joined: " + newPlayer.NickName + "", s2.NotifiLib.NotifiReason.RoomJoined);
                oldnewplayer = newPlayer;
            }
        }

        private static Player oldnewplayer;
    }
}