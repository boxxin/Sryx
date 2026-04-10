using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace plamsa.Patches.Internal
{
    [HarmonyPatch(typeof(VRRig), nameof(VRRig.IsItemAllowed))]
    public class CosmeticPatch
    {
        public static bool enabled;

        public static void Postfix(VRRig __instance, ref bool __result)
        {
            if (enabled)
                __result = true;
        }
    }
}
