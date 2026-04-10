using GorillaNetworking;
using GorillaNetworking.Store;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace plamsa.Patches.Internal
{
    [HarmonyPatch(typeof(BundleManager), nameof(BundleManager.CheckIfBundlesOwned))]
    public class PostGetData
    {
        public static bool CosmeticsInitialized;
        private static void Postfix()
        {
            CosmeticsInitialized = true;
            StupidTemplate.Mods.idk.CosmeticsOwned = CosmeticsController.instance.concatStringCosmeticsAllowed;
        }
    }
}
