using Photon.Pun;
using UnityEngine;

namespace StupidTemplate.Mods
{
    internal class Abuse
    {
        public static void RGBMonkey() // ill test this later
        {
            float col = Mathf.Repeat(Time.time * 0.2f, 1f);
            Color color = Color.HSVToRGB(col, 1f, 1f);
            if (PhotonNetwork.InRoom)
            {
                GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", 0, new object[]
                {
                    color.r,
                    color.g,
                    color.b
                });
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.InitializeNoobMaterialLocal(color.r, color.g, color.b);
            }
        }
    }
}
