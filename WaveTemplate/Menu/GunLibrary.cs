using GorillaLocomotion;
using StupidTemplate.Menu;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Viveport;
using static BepInEx.UnityInput;

namespace StupidTemplate.Classes
{
    public static class GunLibrary
    {
        public static float TrailSpeed = 14f;
        public static VRRig LockedRig = null;
        public static bool HasLock = false;

        public enum BeamShape
        {
            Laser,            
            Sine,            
            Helix,            
            Crackling,       
            Thunderbolt,      
            BraidedRope,      
            SpringCoil,      
            Bullwhip,         
            FlatRibbon,      
            HardZigzag,      
            Pendulum,         
            SlowTwirl,     
            DoubleHelix,      
            ChainLinks,      
            OrbitalDrift,     
            TighteningVortex, 
            TriAxisSine,      
            FireCrackle,      
            GravityArc,      
            Lasso,           
        }

        public enum ColourTheme
        {
            Prismatic,        
            MenuColour,       
            Bichrome,         
            GhostFade,        
            Stroboscope,    
            HyperNeon,        
            ArcticIce,      
            Magma,            
            GoldenShimmer,    
            DarkVoid,         
            GlitchRGB,      
            NorthernLights,   
            ToxicGreen,     
            Haemorrhage,      
            HighVoltage,    
            DeepOcean,        
            Dusk,            
            MatrixCode,       
            Bubblegum,        
            WraithSmoke,     
        }
        public enum GunPose
        {
            Default,
            Forward,
            Left,
            Right,
            Backward,
        }

        public static GunPose Pose { get; private set; } = GunPose.Default;
        public static BeamShape Shape { get; private set; } = BeamShape.Laser;
        public static ColourTheme Theme { get; private set; } = ColourTheme.Prismatic;

        private static readonly int PoseCount = Enum.GetValues(typeof(GunPose)).Length;
        private static readonly int ShapeCount = Enum.GetValues(typeof(BeamShape)).Length;
        private static readonly int ThemeCount = Enum.GetValues(typeof(ColourTheme)).Length;

        public static void NextPose() => Pose = (GunPose)(((int)Pose + 1) % PoseCount);
        public static void PrevPose() => Pose = (GunPose)(((int)Pose - 1 + PoseCount) % PoseCount);
        public static void SetPose(GunPose p) => Pose = p;
        public static void NextShape()
        {
            Shape = (BeamShape)(((int)Shape + 1) % ShapeCount);
            Main.GetIndex("Change Gun Style").overlapText = "Change Gun Style: " + Shape.ToString();
        }
        public static void PrevShape()
        {
            Shape = (BeamShape)(((int)Shape - 1 + ShapeCount) % ShapeCount);
            Main.GetIndex("Change Gun Style").overlapText = "Change Gun Style: " + Shape.ToString();
        }
        public static void NextTheme()
        {
            Theme = (ColourTheme)(((int)Theme + 1) % ThemeCount);
        }
        public static void PrevTheme()
        {
            Theme = (ColourTheme)(((int)Theme - 1 + ThemeCount) % ThemeCount);
        }
        public static void SetShape(BeamShape s) => Shape = s;
        public static void SetTheme(ColourTheme t) => Theme = t;

        private static GameObject _root;
        private static LineRenderer _beam;
        private static Vector3[] _pts;
        private static Vector3 _tip;
        private static Vector3 _prevOrigin;

        private static GameObject _reticleSphere;

        private static readonly Dictionary<VRRig, GameObject> _esp =
            new Dictionary<VRRig, GameObject>();
        private static readonly List<Material> _matPool = new List<Material>();

        private static float _clock = 0f;

        private const int SEGS = 52;

        private static Vector3 _reticleVel;
        private static Vector3 _reticleSmoothed; 
        private static Vector3 _beamTipVel;
        private static Vector3 _beamTipSmoothed;

        private static Vector3 _beamOriginVel;
        private static Vector3 _beamOriginSmoothed;
        private static Vector3 GetPoseDirection()
        {
            var hand = GTPlayer.Instance.RightHand.controllerTransform;
            switch (Pose)
            {
                case GunPose.Default: return -hand.up;
                case GunPose.Forward: return hand.forward;
                case GunPose.Left: return -hand.right;
                case GunPose.Right: return hand.right;
                case GunPose.Backward: return -hand.forward;
                default: return -hand.up;
            }
        }
        public static void Fire(Action onHit, bool lockOn)
        {
            bool grip = ControllerInputPoller.instance.rightControllerGripFloat > 0.1f
                     || Current.GetMouseButton(1);

            if (!grip)
            {
                if (_root != null) Teardown();
                return;
            }

            _clock += Time.deltaTime;

            Vector3 origin = GTPlayer.Instance.RightHand.controllerTransform.position;
            Vector3 dir = GetPoseDirection();
            Vector3 visual = origin;

            bool didHit = false;
            RaycastHit hit = default;

            if (Mouse.current.rightButton.isPressed)
            {
                var cam = GameObject.Find("Shoulder Camera")?.GetComponent<Camera>();
                if (cam != null)
                {
                    Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
                    dir = ray.direction;
                    didHit = Physics.Raycast(origin, dir, out hit, 120f);
                    visual = origin + dir;
                }
            }
            else
            {
                didHit = Physics.Raycast(origin, dir, out hit);
            }

            if (_root == null)
                Bootstrap(visual);

            bool triggerHeld = ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f
                            || Mouse.current.leftButton.isPressed;

            if (HasLock && LockedRig != null)
            {
                if (!triggerHeld)
                {
                    DropLock();
                }
                else
                {
                    Vector3 lockPos = LockedRig.transform.position;
                    _tip = Vector3.Lerp(_tip, lockPos, Time.deltaTime * 25f);
                    SnapReticleToLock(lockPos);

                    Vector3 deltaLock = visual - _prevOrigin;
                    _prevOrigin = visual; 
                    TickBeam(visual, _tip);

                    onHit?.Invoke();
                    return;
                }
            }

            if (!didHit)
            {
                RetractBeam(visual);
                HideReticle();
                return;
            }

            Vector3 rawTip = hit.point;
            _tip = Vector3.Lerp(_tip, hit.point, Time.deltaTime * 14f);

            if (triggerHeld)
                ResolveTrigger(onHit, lockOn, hit, ref _tip);

            TickReticle(_tip);

            Vector3 delta = visual - _prevOrigin;
            _prevOrigin = visual;
            TickBeam(visual, _tip);
        }

        private static void Bootstrap(Vector3 start)
        {
            _root = new GameObject("PlasmaProjector_Root");

            _beam = NewLR("Beam", false, SEGS, 0.013f);

            _reticleSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _reticleSphere.transform.SetParent(_root.transform, false);
            _reticleSphere.transform.localScale = Vector3.one * 0.08f;
            UnityEngine.Object.Destroy(_reticleSphere.GetComponent<Collider>());
            var reticleMat = new Material(Shader.Find("Sprites/Default"));
            _matPool.Add(reticleMat);
            _reticleSphere.GetComponent<Renderer>().material = reticleMat;
            _reticleSphere.SetActive(false);

            _pts = new Vector3[SEGS];
            for (int i = 0; i < SEGS; i++) _pts[i] = start;
            _tip = start;
            _prevOrigin = start;
        }

        private static void Teardown()
        {
            if (_root != null) UnityEngine.Object.Destroy(_root);
            _root = null;
            _beam = null;
            _reticleSphere = null;
            _pts = null;
            _clock = 0f;
            GorillaTagger.Instance.offlineVRRig.enabled = true;

            foreach (var m in _matPool)
                if (m != null) UnityEngine.Object.Destroy(m);
            _matPool.Clear();
        }
        private static Vector3[] _velocities;
        public static bool line = true;
        private static void TickBeam(Vector3 from, Vector3 to)
        {
            if (line)
            {
                if (_beam == null || _pts == null) return;

                if (_velocities == null || _velocities.Length != SEGS)
                    _velocities = new Vector3[SEGS];

                _beamOriginSmoothed = Vector3.SmoothDamp(
                    _beamOriginSmoothed,
                    from,
                    ref _beamOriginVel,
                    0.02f);

                _beamTipSmoothed = Vector3.SmoothDamp(
                    _beamTipSmoothed,
                    to,
                    ref _beamTipVel,
                    0.02f);

                Vector3 span = _beamTipSmoothed - _beamOriginSmoothed;
                float dist = span.magnitude;

                Vector3 perp = dist > 0.001f
                    ? Vector3.Cross(span.normalized, Vector3.up).normalized
                    : Vector3.right;

                for (int i = 0; i < SEGS; i++)
                {
                    float t = i / (float)(SEGS - 1);

                    Vector3 basePos = Vector3.Lerp(_beamOriginSmoothed, _beamTipSmoothed, t);

                    Vector3 offset = BeamOffset(Shape, i, t, perp, dist);

                    Vector3 target = basePos + offset;

                    _pts[i] = Vector3.SmoothDamp(
                        _pts[i],
                        target,
                        ref _velocities[i],
                        0.04f,
                        999f,
                        Time.deltaTime
                    );
                }

                _beam.positionCount = SEGS;
                _beam.SetPositions(_pts);
            }
        }
        private static void RetractBeam(Vector3 hand)
        {
            if (line)
            {
                if (_beam == null || _pts == null) return;
                float spd = Mathf.Clamp(TrailSpeed, 5f, 60f);
                for (int i = 0; i < _pts.Length; i++)
                    _pts[i] = Vector3.Lerp(_pts[i], hand, Time.deltaTime * spd);
                _pts[0] = _pts[_pts.Length - 1] = hand;
                _beam.SetPositions(_pts);
                Paint(_beam, Theme, 0f, BeamWidth(Shape));
            }
        }
        public static float gunsizemulti = 0.18f;
        private static void TickReticle(Vector3 center)
        {
            if (_reticleSphere == null) return;

            _reticleSphere.SetActive(true);

            _reticleSmoothed = Vector3.SmoothDamp(
                _reticleSmoothed,
                center,
                ref _reticleVel,
                    0.055f,
                    999f,
                Time.deltaTime
            );

            _reticleSphere.transform.position = _reticleSmoothed;

            _reticleSphere.transform.localScale = Vector3.one * gunsizemulti;

            var r = _reticleSphere.GetComponent<Renderer>();
            r.material.color = GetGunColor();
        }

        public static void ChangeGunScale(bool forward)
        {
            gunsizemulti += forward ? 0.1f : -0.1f;
            Main.GetIndex("Change Gun Scale").overlapText = "Change Gun Scale: x" + gunsizemulti;
        }
        private static void SnapReticleToLock(Vector3 center)
        {
            if (_reticleSphere == null) return;

            _reticleSmoothed = center;
            _reticleVel = Vector3.zero;

            _reticleSphere.SetActive(true);
            _reticleSphere.transform.position = center;

            _reticleSphere.transform.localScale = Vector3.one * gunsizemulti;

            _reticleSphere.GetComponent<Renderer>().material.color = GetGunColor();
        }

        private static void HideReticle()
        {
            if (_reticleSphere != null)
                _reticleSphere.SetActive(false);
        }

        private static void ResolveTrigger(Action onHit, bool lockOn, RaycastHit hit, ref Vector3 tip)
        {
            if (lockOn)
            {
                var rig = hit.collider.GetComponentInParent<VRRig>();

                if (rig != null && rig != GorillaTagger.Instance.offlineVRRig && !rig.isLocal)
                {
                    if (LockedRig != null && LockedRig != rig)
                    {
                        LockedRig = null;
                        HasLock = false;
                    }
                    LockedRig = rig;
                    HasLock = true;
                }
                else if (rig == null)
                {
                    DropLock();
                }

                if (LockedRig != null && !LockedRig.isLocal && HasLock)
                {
                    tip = LockedRig.transform.position;
                    onHit?.Invoke();
                }
            }
            else
            {
                DropLock();
                onHit?.Invoke();
            }
        }

        private static void DropLock()
        {
            if (LockedRig == null && !HasLock) return;
            LockedRig = null;
            HasLock = false;
        }

        private static Vector3 BeamOffset(BeamShape s, int i, float t,
                                          Vector3 perp, float dist)
        {
            Vector3 up = Vector3.up;
            float C = _clock;

            switch (s)
            {
                case BeamShape.Laser: return Vector3.zero;
                case BeamShape.Sine: return perp * (Mathf.Sin(t * 14f + C * 6f) * 0.032f);
                case BeamShape.Helix:
                    {
                        float a = t * 10f * Mathf.PI + C * 7f;
                        return (perp * Mathf.Cos(a) + up * Mathf.Sin(a)) * 0.028f;
                    }
                case BeamShape.Crackling:
                    return perp * (Mathf.PerlinNoise(i * 0.13f, C * 9f) - 0.5f) * 0.058f
                         + up * (Mathf.PerlinNoise(i * 0.13f, C * 9f + 5f) - 0.5f) * 0.028f;
                case BeamShape.Thunderbolt:
                    return perp * (Mathf.PerlinNoise(i * 0.52f, C * 24f) - 0.5f) * 0.115f
                         + up * (Mathf.PerlinNoise(i * 0.52f, C * 24f + 88f) - 0.5f) * 0.055f;
                case BeamShape.BraidedRope:
                    return perp * Mathf.Sin(t * 30f + C * 11f) * 0.012f
                         + up * Mathf.Cos(t * 22f + C * 9f) * 0.005f;
                case BeamShape.SpringCoil:
                    {
                        float a = t * 26f * Mathf.PI + C * 10f;
                        return (perp * Mathf.Cos(a) + up * Mathf.Sin(a)) * 0.016f;
                    }
                case BeamShape.Bullwhip:
                    return perp * (Mathf.Sin(t * 9f + C * 8f) * 0.052f * (t * t));
                case BeamShape.FlatRibbon:
                    return perp * Mathf.Sin(t * Mathf.PI) * 0.032f;
                case BeamShape.HardZigzag:
                    return perp * ((i % 4 < 2 ? 1f : -1f) * 0.040f);
                case BeamShape.Pendulum:
                    return up * (Mathf.Sin(C * 3.2f) * 0.044f * Mathf.Sin(t * Mathf.PI));
                case BeamShape.SlowTwirl:
                    {
                        float a = C * 3.2f + t * 8f;
                        return (perp * Mathf.Cos(a) + up * Mathf.Sin(a)) * 0.024f;
                    }
                case BeamShape.DoubleHelix:
                    {
                        float a = t * 10f * Mathf.PI + C * 5f;
                        float strand = (i % 2 == 0) ? a : a + Mathf.PI;
                        return (perp * Mathf.Cos(strand) + up * Mathf.Sin(strand)) * 0.024f;
                    }
                case BeamShape.ChainLinks:
                    {
                        float link = Mathf.Abs(Mathf.Sin(t * 15f));
                        return perp * link * 0.020f + up * link * 0.010f;
                    }
                case BeamShape.OrbitalDrift:
                    {
                        float a = C * 7f + t * 13f;
                        float r = 0.028f * Mathf.Sin(t * Mathf.PI);
                        return perp * Mathf.Cos(a) * r + up * Mathf.Sin(a) * r;
                    }
                case BeamShape.TighteningVortex:
                    {
                        float a = (1f - t) * 15f * Mathf.PI + C * 6f;
                        return (perp * Mathf.Cos(a) + up * Mathf.Sin(a)) * (0.044f * (1f - t));
                    }
                case BeamShape.TriAxisSine:
                    {
                        Vector3 side = Vector3.Cross(perp, up).normalized;
                        return perp * Mathf.Sin(t * 9f + C * 5.0f) * 0.024f
                             + up * Mathf.Sin(t * 7f + C * 3.8f) * 0.018f
                             + side * Mathf.Sin(t * 11f + C * 6.2f) * 0.013f;
                    }
                case BeamShape.FireCrackle:
                    return perp * UnityEngine.Random.Range(-0.048f, 0.048f)
                         + up * UnityEngine.Random.Range(-0.024f, 0.024f);
                case BeamShape.GravityArc:
                    return up * (Mathf.Sin(t * Mathf.PI) * dist * 0.22f);
                case BeamShape.Lasso:
                    {
                        float phase = Mathf.Repeat(C * 1.1f, 1f);
                        float expand = Mathf.Sin(phase * Mathf.PI);
                        float a = t * Mathf.PI * 2f + C * 4.5f;
                        return (perp * Mathf.Cos(a) + up * Mathf.Sin(a))
                             * (expand * 0.052f * Mathf.Sin(t * Mathf.PI));
                    }
                default: return Vector3.zero;
            }
        }

        private static float BeamWidth(BeamShape s)
        {
            switch (s)
            {
                case BeamShape.BraidedRope: return 0.022f;
                case BeamShape.FlatRibbon: return 0.030f;
                case BeamShape.DoubleHelix: return 0.010f;
                case BeamShape.ChainLinks: return 0.013f;
                case BeamShape.Thunderbolt: return 0.008f;
                case BeamShape.FireCrackle: return 0.007f;
                case BeamShape.GravityArc: return 0.016f;
                case BeamShape.Laser: return 0.008f;
                default: return 0.013f;
            }
        }

        private static void Paint(LineRenderer lr, ColourTheme theme,
                                  float hShift, float baseWidth)
        {
            if (lr == null) return;
            lr.colorGradient = MakeGradient(theme, hShift);
            float w = theme == ColourTheme.Stroboscope
                ? baseWidth * (0.5f + Mathf.Abs(Mathf.Sin(_clock * 9f)) * 1.0f)
                : baseWidth;
            lr.widthCurve = theme == ColourTheme.GhostFade
                ? AnimationCurve.Linear(0f, w, 1f, w * 0.08f)
                : AnimationCurve.Linear(0f, w, 1f, w);
        }

        private static Gradient MakeGradient(ColourTheme theme, float shift)
        {
            float C = _clock;
            switch (theme)
            {
                case ColourTheme.Prismatic: return Rainbow(C * 0.15f + shift);
                case ColourTheme.MenuColour: { Color c = Settings.backgroundColor.colors[0].color; return G2(c, c); }
                case ColourTheme.Bichrome: { Color c = Settings.backgroundColor.colors[0].color; return G2(c, Color.white); }
                case ColourTheme.GhostFade: { Color c = Settings.backgroundColor.colors[0].color; return G2(c, new Color(c.r, c.g, c.b, 0f)); }
                case ColourTheme.Stroboscope: return G2(Color.white, Color.white);
                case ColourTheme.HyperNeon: { Color n = Color.HSVToRGB(Mathf.Repeat(C * 0.13f + shift, 1f), 1f, 1f); return G2(n, new Color(n.r * .5f + .5f, n.g * .5f + .5f, n.b * .5f + .5f)); }
                case ColourTheme.ArcticIce: return G3(new Color(.78f, .95f, 1f), new Color(.38f, .80f, 1f), Color.white);
                case ColourTheme.Magma: return G3(new Color(1f, .07f, 0f), new Color(1f, .50f, 0f), new Color(1f, .93f, .28f));
                case ColourTheme.GoldenShimmer: { float s = .74f + Mathf.Abs(Mathf.Sin(C * 3.1f)) * .26f; return G2(new Color(1f, .82f * s, .08f), new Color(1f, .96f, .68f)); }
                case ColourTheme.DarkVoid: return G3(new Color(.09f, 0f, .19f), new Color(.36f, 0f, .76f), new Color(.05f, 0f, .10f));
                case ColourTheme.GlitchRGB: { Color g = new Color(UnityEngine.Random.value > .5f ? 1f : 0f, UnityEngine.Random.value > .4f ? 1f : 0f, UnityEngine.Random.value > .4f ? 1f : 0f); return G2(g, Color.white); }
                case ColourTheme.NorthernLights: return G3(Color.HSVToRGB(Mathf.Repeat(C * .065f + shift, 1f), .62f, .86f), Color.HSVToRGB(Mathf.Repeat(C * .065f + shift + .13f, 1f), .72f, 1f), Color.HSVToRGB(Mathf.Repeat(C * .065f + shift + .26f, 1f), .52f, .78f));
                case ColourTheme.ToxicGreen: return G2(new Color(.20f, 1f, .06f), new Color(.50f, 1f, .20f));
                case ColourTheme.Haemorrhage: return G2(new Color(.52f, .02f, .02f), new Color(.88f, .05f, .05f));
                case ColourTheme.HighVoltage: return G3(new Color(1f, 1f, .28f), Color.white, new Color(.88f, .88f, 1f));
                case ColourTheme.DeepOcean: return G3(new Color(.04f, .22f, .52f), new Color(.08f, .58f, .72f), new Color(.18f, .88f, .82f));
                case ColourTheme.Dusk: return G3(new Color(1f, .32f, .08f), new Color(1f, .16f, .52f), new Color(.52f, .08f, .78f));
                case ColourTheme.MatrixCode: return G2(new Color(0f, .88f, .06f), new Color(.38f, 1f, .38f));
                case ColourTheme.Bubblegum: return Rainbow(C * 0.18f + shift, 0.60f, 0.96f);
                case ColourTheme.WraithSmoke: { Color gr = new Color(.68f, .68f, .74f, 1f); return G2(gr, new Color(.68f, .68f, .74f, .03f)); }
                default: return Rainbow(C * 0.15f + shift);
            }
        }

        private static Gradient Rainbow(float h, float sat = 0.95f, float val = 1f)
        {
            var g = new Gradient();
            g.SetKeys(new GradientColorKey[]
            {
                new GradientColorKey(Color.HSVToRGB(Mathf.Repeat(h,        1f), sat, val), 0.00f),
                new GradientColorKey(Color.HSVToRGB(Mathf.Repeat(h + .25f, 1f), sat, val), 0.33f),
                new GradientColorKey(Color.HSVToRGB(Mathf.Repeat(h + .50f, 1f), sat, val), 0.66f),
                new GradientColorKey(Color.HSVToRGB(Mathf.Repeat(h + .75f, 1f), sat, val), 1.00f),
            },
            new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) });
            return g;
        }

        private static Gradient G2(Color a, Color b)
        {
            var g = new Gradient();
            g.SetKeys(
                new GradientColorKey[] { new GradientColorKey(a, 0f), new GradientColorKey(b, 1f) },
                new GradientAlphaKey[] { new GradientAlphaKey(a.a, 0f), new GradientAlphaKey(b.a, 1f) });
            return g;
        }

        private static Gradient G3(Color a, Color b, Color c)
        {
            var g = new Gradient();
            g.SetKeys(
                new GradientColorKey[] { new GradientColorKey(a, 0f), new GradientColorKey(b, .5f), new GradientColorKey(c, 1f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) });
            return g;
        }

        private static LineRenderer NewLR(string name, bool loop, int segs, float w)
        {
            var go = new GameObject(name);
            go.transform.SetParent(_root.transform, false);
            return BuildLR(go, loop, segs, w);
        }

        private static LineRenderer NewLROnParent(GameObject parent, string name,
                                                   bool loop, int segs, float w)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent.transform, false);
            return BuildLR(go, loop, segs, w);
        }

        private static LineRenderer BuildLR(GameObject go, bool loop, int segs, float w)
        {
            var lr = go.AddComponent<LineRenderer>();

            lr.useWorldSpace = true;
            lr.loop = loop;
            lr.positionCount = segs;
            lr.numCapVertices = 8;
            lr.numCornerVertices = 8;
            lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lr.receiveShadows = false;
            lr.widthMultiplier = w;
            lr.textureMode = LineTextureMode.Stretch;
            lr.alignment = LineAlignment.View;

            var mat = new Material(Shader.Find("Unlit/Color"));
            mat.color = GetGunColor();
            _matPool.Add(mat);

            lr.material = mat;

            return lr;
        }
        private static Color GetGunColor()
        {
            switch (Theme)
            {
                case ColourTheme.Prismatic:
                    return Color.cyan;

                case ColourTheme.Magma:
                    return new Color(1f, 0.3f, 0f);

                case ColourTheme.ArcticIce:
                    return new Color(0.6f, 0.9f, 1f);

                case ColourTheme.DarkVoid:
                    return new Color(0.4f, 0f, 0.8f);

                case ColourTheme.ToxicGreen:
                    return new Color(0.2f, 1f, 0.2f);

                case ColourTheme.Haemorrhage:
                    return new Color(0.8f, 0f, 0f);

                default:
                    return Color.white;
            }
        }
    }
}