using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;

namespace StupidTemplate.Patches.Internal
{
    [HarmonyPatch(typeof(PhotonNetwork), "RunViewUpdate")]
    public class SerializePatch
    {
        public static event Action OnSerialize;
        public static bool Prefix()
        {
            if (!PhotonNetwork.InRoom)
            {
                return true;
            }
            try
            {
                Action onSerialize = SerializePatch.OnSerialize;
                if (onSerialize != null)
                {
                    onSerialize.Invoke();
                }
            }
            catch
            {
            }
            if (SerializePatch.OverrideSerialization == null)
            {
                return true;
            }
            bool result;
            try
            {
                result = SerializePatch.OverrideSerialization.Invoke();
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public static Func<bool> OverrideSerialization;
    }
}
