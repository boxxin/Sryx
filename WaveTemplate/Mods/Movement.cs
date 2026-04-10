using BepInEx;
using GorillaLocomotion;
using GorillaLocomotion.Climbing;
using StupidTemplate.Classes;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using static StupidTemplate.Menu.Main;
using static StupidTemplate.Mods.Settings.Movement;
using static StupidTemplate.Settings;
namespace StupidTemplate.Mods
{
    public class Movement
    {
        public static bool leftTouching = false;
        public static bool rightTouching = false;
        public static float startX = -1f;
        public static float startY = -1f;
        public static GameObject platl;
        public static GameObject platr;
        public static GameObject BombObject = null;
        public static float subThingy;
        public static float subThingyZ;
        public static Vector3 lastPosition = Vector3.zero;
        public static GameObject bruh1;
        public static GameObject bruh2;

        #region Movement
        public static void FixRigHandRotation()
        {
            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.rotation *= Quaternion.Euler(GorillaTagger.Instance.offlineVRRig.leftHand.trackingRotationOffset);
            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.rotation *= Quaternion.Euler(GorillaTagger.Instance.offlineVRRig.rightHand.trackingRotationOffset);
        }
        public static void Beyblade()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0.15f);
                try
                {
                    GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0.15f);
                }
                catch
                {
                }
                GorillaTagger.Instance.offlineVRRig.transform.rotation = Quaternion.Euler(GorillaTagger.Instance.offlineVRRig.transform.rotation.eulerAngles + new Vector3(0f, 10000000f, 0f));
                try
                {
                    GorillaTagger.Instance.myVRRig.transform.rotation = Quaternion.Euler(GorillaTagger.Instance.offlineVRRig.transform.rotation.eulerAngles + new Vector3(0f, 10000000f, 0f));
                }
                catch
                {
                }

                GorillaTagger.Instance.offlineVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.right * -1f;
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.right * 1f;
                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
                FixRigHandRotation();
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void WASDFly()
        {

            bool W = UnityInput.Current.GetKey(KeyCode.W);
            bool A = UnityInput.Current.GetKey(KeyCode.A);
            bool S = UnityInput.Current.GetKey(KeyCode.S);
            bool D = UnityInput.Current.GetKey(KeyCode.D);
            bool Space = UnityInput.Current.GetKey(KeyCode.Space);
            bool Ctrl = UnityInput.Current.GetKey(KeyCode.LeftControl);
            bool Shift = UnityInput.Current.GetKey(KeyCode.LeftShift);
            bool Alt = UnityInput.Current.GetKey(KeyCode.LeftAlt);

            bool LeftArrow = UnityInput.Current.GetKey(KeyCode.LeftArrow);
            bool RightArrow = UnityInput.Current.GetKey(KeyCode.RightArrow);
            bool UpArrow = UnityInput.Current.GetKey(KeyCode.UpArrow);
            bool DownArrow = UnityInput.Current.GetKey(KeyCode.DownArrow);

            if (!menu)
            {
                Transform parentTransform = GTPlayer.Instance.GetControllerTransform(false).parent;

                float turnSpeed = 250f;

                if (LeftArrow)
                    parentTransform.eulerAngles += new Vector3(0, -turnSpeed, 0) * Time.deltaTime;
                if (RightArrow)
                    parentTransform.eulerAngles += new Vector3(0, turnSpeed, 0) * Time.deltaTime;
                if (UpArrow)
                    parentTransform.eulerAngles += new Vector3(-turnSpeed, 0, 0) * Time.deltaTime;
                if (DownArrow)
                    parentTransform.eulerAngles += new Vector3(turnSpeed, 0, 0) * Time.deltaTime;

                if (Mouse.current.rightButton.isPressed)
                {
                    Quaternion currentRotation = parentTransform.rotation;
                    Vector3 euler = currentRotation.eulerAngles;

                    if (startX < 0)
                    {
                        startX = euler.y;
                        subThingy = Mouse.current.position.value.x / Screen.width;
                    }
                    if (startY < 0)
                    {
                        startY = euler.x;
                        subThingyZ = Mouse.current.position.value.y / Screen.height;
                    }

                    float newX = startY - (Mouse.current.position.value.y / Screen.height - subThingyZ) * 360 * 1.33f;
                    float newY = startX + (Mouse.current.position.value.x / Screen.width - subThingy) * 360 * 1.33f;

                    newX = newX > 180f ? newX - 360f : newX;
                    newX = Mathf.Clamp(newX, -90f, 90f);

                    parentTransform.rotation = Quaternion.Euler(newX, newY, euler.z);
                }
                else
                {
                    startX = -1;
                    startY = -1;
                }

                float speed = 5f;
                if (Shift)
                    speed *= 6f;
                else if (Alt)
                    speed /= 2;

                if (W)
                    GorillaTagger.Instance.rigidbody.transform.position += GTPlayer.Instance.GetControllerTransform(false).parent.forward * (Time.deltaTime * speed);

                if (S)
                    GorillaTagger.Instance.rigidbody.transform.position += GTPlayer.Instance.GetControllerTransform(false).parent.forward * (Time.deltaTime * -speed);

                if (A)
                    GorillaTagger.Instance.rigidbody.transform.position += GTPlayer.Instance.GetControllerTransform(false).parent.right * (Time.deltaTime * -speed);

                if (D)
                    GorillaTagger.Instance.rigidbody.transform.position += GTPlayer.Instance.GetControllerTransform(false).parent.right * (Time.deltaTime * speed);

                if (Space)
                    GorillaTagger.Instance.rigidbody.transform.position += new Vector3(0f, Time.deltaTime * speed, 0f);

                if (Ctrl)
                    GorillaTagger.Instance.rigidbody.transform.position += new Vector3(0f, Time.deltaTime * -speed, 0f);

                VRRig.LocalRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;
            }

            if (!W && !A && !S && !D && !Space && !Ctrl && lastPosition != Vector3.zero)
                GorillaTagger.Instance.rigidbody.transform.position = lastPosition;
            else
                lastPosition = GorillaTagger.Instance.rigidbody.transform.position;
        }
        public static void VelocityFly()
        {
            Speed speed = Speedss.velflyspeeds[Speedss.velspeedValue];
            if (ControllerInputPoller.instance.rightControllerSecondaryButton)
            {
                GTPlayer.Instance.GetComponent<Rigidbody>().AddForce(GTPlayer.Instance.headCollider.transform.forward * speed.velflyspeed * Time.deltaTime);
            }
        }
        public static void Fly()
        {
           Speed sped = Speedss.flyspeeds[Speedss.speedValue];
           if (ControllerInputPoller.instance.rightControllerPrimaryButton)
           {
               GTPlayer.Instance.transform.position += GTPlayer.Instance.headCollider.transform.forward * Time.deltaTime * sped.flyspeed;
               GTPlayer.Instance.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
           }
        }
        #endregion

        #region Player

        public static void permainvis()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(9999f, 9999f, 9999f);
        }
        public static void normalinvis()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                permainvis();
            }
            if (!ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                nopermainvis();
            }
        }
        public static void GhostMonek()
        {
            if (ControllerInputPoller.instance.rightControllerSecondaryButton)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
            }
            if (!ControllerInputPoller.instance.rightControllerSecondaryButton)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void nopermainvis()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = true;
            GorillaTagger.Instance.offlineVRRig.transform.position = Vector3.zero;
        }
        public static void PullMod()
        {
            if (leftTouching && !GTPlayer.Instance.IsHandTouching(true) || rightTouching && !GTPlayer.Instance.IsHandTouching(false))
            {
                Vector3 velocity = GTPlayer.Instance.GetComponent<Rigidbody>().linearVelocity;
                GTPlayer.Instance.transform.position += new Vector3(velocity.x * 0.035f, 0f, velocity.z * 0.035f);
            }
            leftTouching = GTPlayer.Instance.IsHandTouching(true);
            rightTouching = GTPlayer.Instance.IsHandTouching(false);
        }
        public static void PullMod2()
        {
            if (leftTouching && !GTPlayer.Instance.IsHandTouching(true) || rightTouching && !GTPlayer.Instance.IsHandTouching(false))
            {
                Vector3 velocity = GTPlayer.Instance.GetComponent<Rigidbody>().linearVelocity;
                GTPlayer.Instance.transform.position += new Vector3(velocity.x * 0.09f, 0f, velocity.z * 0.09f);
            }
            leftTouching = GTPlayer.Instance.IsHandTouching(true);
            rightTouching = GTPlayer.Instance.IsHandTouching(false);
        }
        private static float lastTime;
        public static void hz(int fps)
        {
            float targetDelta = 1f / fps;
            float elapsed = Time.realtimeSinceStartup - lastTime;

            if (elapsed < targetDelta)
            {
                int sleepMs = Mathf.FloorToInt((targetDelta - elapsed) * 1000);
                if (sleepMs > 0)
                    Thread.Sleep(sleepMs);
            }

            lastTime = Time.realtimeSinceStartup;
        }
        public static void press()
        {
            Vector3 headPos = GorillaTagger.Instance.headCollider.transform.position;
            bruh1.transform.position = headPos - GorillaTagger.Instance.leftHandTransform.position;
            bruh2.transform.position = headPos - GorillaTagger.Instance.rightHandTransform.position;
            GTPlayer.Instance.GetControllerTransform(true).transform.position -= bruh1.GetComponent<GorillaVelocityTracker>().GetAverageVelocity(true, 0) * 0.0180f;
            GTPlayer.Instance.GetControllerTransform(false).transform.position -= bruh2.GetComponent<GorillaVelocityTracker>().GetAverageVelocity(true, 0) * 0.0180f;
        }
        public static void CreateVelocityTrackers()
        {
            bruh1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Object.Destroy(bruh1.GetComponent<BoxCollider>());
            bruh1.GetComponent<Renderer>().enabled = false;
            bruh1.AddComponent<GorillaVelocityTracker>();

            bruh2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Object.Destroy(bruh2.GetComponent<BoxCollider>());
            bruh2.GetComponent<Renderer>().enabled = false;
            bruh2.AddComponent<GorillaVelocityTracker>();
        }

        public static void DestroyVelocityTrackers()
        {
            Object.Destroy(bruh1);
            Object.Destroy(bruh2);
        }

        #endregion

        #region Game
        public static void Bomb()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (BombObject == null)
                {
                    BombObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(BombObject.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(BombObject.GetComponent<SphereCollider>());
                    BombObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    BombObject.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
                }
                BombObject.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
            if (BombObject != null)
            {
                if (ControllerInputPoller.instance.leftControllerPrimaryButton)
                {
                    BombObject.GetComponent<Renderer>().material.color = Color.white;
                    Vector3 dir = (GorillaTagger.Instance.bodyCollider.transform.position - BombObject.transform.position);
                    dir.Normalize();
                    GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().linearVelocity += 25f * dir;
                    UnityEngine.Object.Destroy(BombObject);
                    BombObject = null;
                }
                else
                {
                    BombObject.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
                }
            }
        }

        public static void DisableBomb()
        {
            if (BombObject != null)
            {
                UnityEngine.Object.Destroy(BombObject);
                BombObject = null;
            }
        }
        public static bool noclipantirepeat = false;
        public static void Noclip()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f || UnityInput.Current.GetKey(KeyCode.K))
            {
                noclipantirepeat = false;
                foreach (MeshCollider colliders in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    colliders.enabled = false;
                }
            }
            else
            {
                if (noclipantirepeat == false)
                {
                    noclipantirepeat = true;
                    foreach (MeshCollider colliders in Resources.FindObjectsOfTypeAll<MeshCollider>())
                    {
                        colliders.enabled = true;
                    }
                }
            }
        }
        public static void ZeroGrav()
        {
            GTPlayer.Instance.bodyCollider.attachedRigidbody.useGravity = false;
        }
        public static void FixGrav()
        {
            GTPlayer.Instance.bodyCollider.attachedRigidbody.useGravity = true;
        }
        public static void GripSpeedBoost()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GTPlayer.Instance.jumpMultiplier = jumpmulti;
                GTPlayer.Instance.maxJumpSpeed = maxjumpsped;
            }
        }
        public static void GripSpeedBoost2()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GTPlayer.Instance.jumpMultiplier = 1.2f;
                GTPlayer.Instance.maxJumpSpeed = 9.67f;
            }
        }
        public static void SpeedBoost()
        {
            GTPlayer.Instance.jumpMultiplier = jumpmulti;
            GTPlayer.Instance.maxJumpSpeed = maxjumpsped;
        }

        public static void Platforms()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                if (platl == null)
                {
                    platl = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    platl.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    //platl.transform.position = TrueLeftHand().position;
                    //platl.transform.rotation = TrueLeftHand().rotation;
                    platl.transform.position = GorillaLocomotion.GTPlayer.Instance.LeftHand.controllerTransform.position;
                    platl.transform.rotation = GorillaLocomotion.GTPlayer.Instance.LeftHand.controllerTransform.rotation;

                    ColorChanger colorChanger = platl.AddComponent<ColorChanger>();
                    colorChanger.colors = backgroundColor;
                }
            }
            else
            {
                if (platl != null)
                {
                    Object.Destroy(platl);
                    platl = null;
                }
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                if (platr == null)
                {
                    platr = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    platr.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    //platr.transform.position = TrueRightHand().position;
                    //platr.transform.rotation = TrueRightHand().rotation;
                    platr.transform.position = GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.position;
                    platr.transform.rotation = GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.rotation;

                    ColorChanger colorChanger = platr.AddComponent<ColorChanger>();
                    colorChanger.colors = backgroundColor;
                }
            }
            else
            {
                if (platr != null)
                {
                    Object.Destroy(platr);
                    platr = null;
                }
            }
        }

        public static class RigMods
        {
            public static void FreezeAt(Vector3 pos)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = pos;
            }
            private static void FreezeAt(Vector3 pos, Quaternion rot)
            {
                FreezeAt(pos);
                GorillaTagger.Instance.offlineVRRig.transform.rotation = rot;
            }
            public static void Unfreeze()
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
            private static Vector3 _frozenPos;
            private static bool _frozenPosSet;
            public static void Freeze()
            {
                if (!_frozenPosSet)
                {
                    _frozenPos = GTPlayer.Instance.bodyCollider.transform.position;
                    _frozenPosSet = true;
                }
                FreezeAt(_frozenPos);
            }

            public static void FreezeCleanup() { _frozenPosSet = false; Unfreeze(); }
            public static float FloatHeight = 2.5f;
            public static void Float()
            {
                Vector3 current = GorillaTagger.Instance.offlineVRRig.transform.position;
                FreezeAt(new Vector3(current.x, FloatHeight, current.z));
            }


            public static void PinToCeiling()
            {
                Vector3 current = GorillaTagger.Instance.offlineVRRig.transform.position;
                if (Physics.Raycast(current, Vector3.up, out RaycastHit hit, 30f))
                    FreezeAt(new Vector3(current.x, hit.point.y - 0.1f, current.z));
                else
                    FreezeAt(new Vector3(current.x, current.y + 10f, current.z));
            }
            public static void SnapToGround()
            {
                Vector3 current = GorillaTagger.Instance.offlineVRRig.transform.position;
                if (Physics.Raycast(current, Vector3.down, out RaycastHit hit, 30f))
                    FreezeAt(new Vector3(current.x, hit.point.y, current.z));
            }
            public static Vector3 FollowOffset = new Vector3(0f, 0f, 1.2f);
            public static void FollowRig(VRRig target)
            {
                if (target == null) return;
                Vector3 targetPos = target.transform.position
                                  + target.transform.forward * FollowOffset.z
                                  + target.transform.up * FollowOffset.y
                                  + target.transform.right * FollowOffset.x;
                FreezeAt(targetPos);
            }
            public static float OrbitRadius = 1.8f;
            public static float OrbitSpeed = 60f;
            public static float OrbitHeight = 0f;
            private static float _orbitAngle = 0f;
            public static void OrbitRig(VRRig target)
            {
                if (target == null) return;
                _orbitAngle = Mathf.Repeat(_orbitAngle + OrbitSpeed * Time.deltaTime, 360f);
                float rad = _orbitAngle * Mathf.Deg2Rad;
                Vector3 targetPos = target.transform.position;
                Vector3 orbit = new Vector3(
                    targetPos.x + Mathf.Cos(rad) * OrbitRadius,
                    targetPos.y + OrbitHeight,
                    targetPos.z + Mathf.Sin(rad) * OrbitRadius);
                FreezeAt(orbit);
            }
            public static void OrbitCleanup() { _orbitAngle = 0f; Unfreeze(); }
            public static void TeleportTo(Vector3 destination)
            {
                GTPlayer.Instance.TeleportTo(destination, GTPlayer.Instance.bodyCollider.transform.rotation);
            }
            public static void TeleportToRig(VRRig target, float behindDistance = 1.0f)
            {
                if (target == null) return;
                Vector3 dest = target.transform.position - target.transform.forward * behindDistance;
                TeleportTo(dest);
            }
        }
        #endregion
    }
}
