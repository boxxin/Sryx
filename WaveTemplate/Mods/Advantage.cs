using GorillaGameModes;
using GorillaLocomotion;
using Photon.Pun;
using StupidTemplate.Menu;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StupidTemplate.Mods
{
    internal class Advantage
    {
        public static void TagAura()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.leftButton.isPressed)
            {

                foreach (VRRig rig in from r in VRRigCache.ActiveRigs where r != VRRig.LocalRig select r)
                {
                    if (GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                    {
                        if (rig != GorillaTagger.Instance.offlineVRRig)
                        {
                            if (!rig.mainSkin.material.name.Contains("fected"))
                            {
                                if (Vector3.Distance(rig.transform.position, GTPlayer.Instance.transform.position) < 3f)
                                {
                                    Tag(rig);
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void TagSelf()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f || Mouse.current.leftButton.isPressed)
            {
                
                foreach (VRRig rig in from r in VRRigCache.ActiveRigs where r != VRRig.LocalRig select r)
                {
                    if (!GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                    {
                        if (rig.mainSkin.material.name.Contains("fected"))
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = false;
                            GorillaTagger.Instance.offlineVRRig.transform.position = rig.rightHandTransform.position;
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                        }
                    }
                    if (GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                    {
                        Main.GetIndex("Tag Self [RT]").enabled = false;
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                    }
                }
            }
        }
        public static void Tag(VRRig rig)
        {
            if (GorillaGameManager.instance is GorillaTagManager tagManager)
            {
                if (!tagManager.currentInfected.Contains(rig.Creator))
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        if (tagManager.isCurrentlyTag)
                        {
                        }
                        else
                        {
                            tagManager.currentInfected.Add(rig.Creator);
                        }
                    }
                    else
                    {
                        if (GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                        {
                            UpdateView(rig, () => GameMode.ReportTag(rig.Creator));
                        }
                    }
                }
                return;
            }
        }

        private static void UpdateView(VRRig rig, Action report)
        {
            var runViewUpdate = typeof(PhotonNetwork).GetMethod("RunViewUpdate", BindingFlags.Static | BindingFlags.NonPublic);
            runViewUpdate.Invoke(null, null);
            var offlineRig = GorillaTagger.Instance.offlineVRRig;
            var pos = rig.transform.position;
            offlineRig.transform.position = offlineRig.leftHand.rigTarget.position = offlineRig.rightHand.rigTarget.position = pos;
            runViewUpdate.Invoke(null, null);
            report();
        }
        public static void TagGunInstant(VRRig therig)
        {
            if (GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
            {
                if (!therig.mainSkin.material.name.Contains("fected"))
                {
                    Tag(therig);
                }
            }
        }
        public static void TagAll()
        {
            if (GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
            {
                foreach (VRRig rig in from r in VRRigCache.ActiveRigs where r != VRRig.LocalRig select r)
                {
                    if (!rig.isLocal)
                    {
                        Tag(rig);
                    }
                }
            }
        }
    }
}
