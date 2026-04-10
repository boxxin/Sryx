
using GorillaNetworking;
using StupidTemplate.Classes;
using StupidTemplate.Mods;
using StupidTemplate.Patches.Internal;
using UnityEngine.InputSystem;
using static StupidTemplate.Menu.Main;
using static StupidTemplate.Mods.Movement;
using static StupidTemplate.Mods.Settings.Movement;
using static StupidTemplate.Mods.Visual;
using static StupidTemplate.Settings;

namespace StupidTemplate.Menu
{
    public class Buttons
    {
        public static ButtonInfo[][] buttonsCat = new ButtonInfo[][]
        {
            new ButtonInfo[] {
                new ButtonInfo { buttonText = "Credits", method =() => currentCategory = 1, isTogglable = false},
                new ButtonInfo { buttonText = "Config", method =() => currentCategory = 2, isTogglable = false},
                new ButtonInfo { buttonText = "Main", method =() => currentCategory = 3, isTogglable = false},
                new ButtonInfo { buttonText = "Player", method =() => currentCategory = 4, isTogglable = false},
                new ButtonInfo { buttonText = "Safety", method =() => currentCategory = 5, isTogglable = false},
                new ButtonInfo { buttonText = "Visual", method =() => currentCategory = 6, isTogglable = false},
                new ButtonInfo { buttonText = "Abuse", method =() => currentCategory = 7, isTogglable = false},
                new ButtonInfo { buttonText = "Advantage", method =() => currentCategory = 8, isTogglable = false},
                new ButtonInfo { buttonText = "Overpowered", method =() => currentCategory = 9, isTogglable = false},
            }
        };
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] {
                new ButtonInfo { buttonText = "Credits", method =() => currentCategory = 1, isTogglable = false},
                new ButtonInfo { buttonText = "Config", method =() => currentCategory = 2, isTogglable = false},
                new ButtonInfo { buttonText = "Main", method =() => currentCategory = 3, isTogglable = false},
                new ButtonInfo { buttonText = "Player", method =() => currentCategory = 4, isTogglable = false},
                new ButtonInfo { buttonText = "Safety", method =() => currentCategory = 5, isTogglable = false},
                new ButtonInfo { buttonText = "Visual", method =() => currentCategory = 6, isTogglable = false},
                new ButtonInfo { buttonText = "Abuse", method =() => currentCategory = 7, isTogglable = false},
                new ButtonInfo { buttonText = "Advantage", method =() => currentCategory = 8, isTogglable = false},
                new ButtonInfo { buttonText = "Overpowered", method =() => currentCategory = 9, isTogglable = false},
            },
            new ButtonInfo[] { // cred [1]
                new ButtonInfo { buttonText = "Back", method =() => currentCategory = 0, isTogglable = false},
                new ButtonInfo { buttonText = "boxxin: menu owner", isTogglable = false, justtext = true},
                new ButtonInfo { buttonText = "divine: motinal support", isTogglable = false, justtext = true},
                new ButtonInfo { buttonText = "axo: ideas & tester", isTogglable = false, justtext = true},
                new ButtonInfo { buttonText = "g3if: i skidded from him", isTogglable = false, justtext = true},
                new ButtonInfo { buttonText = "right tes<color=orange>t</color>ical", isTogglable = false, justtext = true},
                new ButtonInfo { buttonText = "left testical", isTogglable = false, justtext = true},
            },

            new ButtonInfo[] { // Settings [2]
                new ButtonInfo { buttonText = "Back", method =() => currentCategory = 0, isTogglable = false},
                new ButtonInfo { buttonText = "Customization", method =() => currentCategory = 10, isTogglable = false},
                new ButtonInfo { buttonText = "Gun Lib", method =() => currentCategory = 11, isTogglable = false},
                // got lazy
                new ButtonInfo { buttonText = "Tracers Position", enableMethod =() => handboi = true, disableMethod =() => handboi = false},
                new ButtonInfo { buttonText = "Stump Text",  method =() => idk.StumpText(), isTogglable = true},
                new ButtonInfo { buttonText = "2d Wireframe ESP", method =() => Visual.Wireframe3D = !Visual.Wireframe3D, isTogglable = false},
                new ButtonInfo { buttonText = "Intro Test", method =() => testlo(), isTogglable = false},
            },

            new ButtonInfo[] { // Main Mods [3]
                new ButtonInfo { buttonText = "Back", method =() => currentCategory = 0, isTogglable = false},
                new ButtonInfo { buttonText = "Quit App", method =() => UnityEngine.Application.Quit(), isTogglable = false},
                new ButtonInfo { buttonText = "Disconnect", method =() => NetworkSystem.Instance.ReturnToSinglePlayer(), isTogglable = false},
                new ButtonInfo { buttonText = "leg slef", method =() => idk.lagself(), isTogglable = true},
                new ButtonInfo { buttonText = "Enable Boards", method =() => idk.Boards(), isTogglable = true, enabled = true},
                new ButtonInfo { buttonText = "Get All RPC's", method =() => idk.GetAllRPCS(), isTogglable = false},
                new ButtonInfo { buttonText = "Unlock All Cosmetics", method =() => idk.UnlockAllCosmetics(), isTogglable = false},
                new ButtonInfo { buttonText = "Anti AFK", enableMethod =() => PhotonNetworkController.Instance.disableAFKKick = true, disableMethod =() => PhotonNetworkController.Instance.disableAFKKick = false},
            },

            new ButtonInfo[] { // Player / Movement Mods [4]
                new ButtonInfo { buttonText = "Back", method =() => currentCategory = 0, isTogglable = false},
                new ButtonInfo { buttonText = "Platforms", method =() => Platforms()},
                new ButtonInfo { buttonText = "Fly [<color=green>A</color>]", method =() => Fly()},
                new ButtonInfo { buttonText = "Change Fly Speed", Up =() => Speedss.ChangeFlySpeed(true), Down =() => Speedss.ChangeFlySpeed(false), isIncremental = true, isTogglable = false},
                new ButtonInfo { buttonText = "Velocity Fly [<color=green>B</color>]", method =() => VelocityFly()},
                new ButtonInfo { buttonText = "Change Vel Fly Speed", Up =() => Speedss.ChangeVelFlySpeed(true), Down =() => Speedss.ChangeVelFlySpeed(false), isIncremental = true, isTogglable = false},
                new ButtonInfo { buttonText = "Pc Flight", method =() => WASDFly()},
                new ButtonInfo { buttonText = "Noclip [<color=green>RT/K</color>]", method =() => Noclip()},
                new ButtonInfo { buttonText = "Bomb [<color=green>RG/X</color>]", method =() => Bomb() , disableMethod = DisableBomb},
                new ButtonInfo { buttonText = "Speed Boost", method =() => SpeedBoost() },
                new ButtonInfo { buttonText = "Speed Boost [<color=green>G</color>]", method =() => GripSpeedBoost() },
                new ButtonInfo { buttonText = "Change Max Jump Speed", method =() => ChangeMaxJumpSpeed(), isTogglable = false},
                new ButtonInfo { buttonText = "Change Jump Multi", method =() => ChangeJumpMulti(), isTogglable = false},
                new ButtonInfo { buttonText = "Predictions", enableMethod = CreateVelocityTrackers, method = press, disableMethod = DestroyVelocityTrackers},
                new ButtonInfo { buttonText = "Pull Mod", method =() => PullMod() },
                new ButtonInfo { buttonText = "Strong Pull Mod", method =() => PullMod2() },
                new ButtonInfo { buttonText = "45 HZ", method =() => hz(45)},
                new ButtonInfo { buttonText = "60 HZ", method =() => hz(60)},
                new ButtonInfo { buttonText = "Ghost [<color=green>B</color>]", method =() => GhostMonek(), isTogglable = true},
                new ButtonInfo { buttonText = "Invis [<color=green>P</color>]", method =() => normalinvis(), isTogglable = true},
                new ButtonInfo { buttonText = "Perma Invis", enableMethod =() => permainvis(), disableMethod =() => nopermainvis(), isTogglable = true},
            },

            new ButtonInfo[] { // Safety Mods [5] got lazy again
                new ButtonInfo { buttonText = "Back", method =() => currentCategory = 0, isTogglable = false},
                new ButtonInfo { buttonText = "Anti Report [<color=green>Disconenct</color>]", method =() => Safety.AntiReportDisconnect()},
                new ButtonInfo { buttonText = "Anti Report [<color=green>Lag</color>]", method =() => Safety.AntiReportLag()},
                new ButtonInfo { buttonText = "Anti RPC Kick", method =() => Safety.AntiRPCKick()},
                new ButtonInfo { buttonText = "Anti Ban [<color=yellow>W?</color>]", method =() => Safety.AntiBan()},
                new ButtonInfo { buttonText = "Bypass VC Mutes", method =() => Safety.BypassVCBan()},
            },

            new ButtonInfo[] { // visual mods [6]
                new ButtonInfo { buttonText = "Back", method =() => currentCategory = 0, isTogglable = false},
                new ButtonInfo { buttonText = "Day Time", method =() => DayTime(), isTogglable = false},
                new ButtonInfo { buttonText = "Morning Time", method =() => MorningTime(), isTogglable = false},
                new ButtonInfo { buttonText = "Night Time", method =() => NightTime(), isTogglable = false},
                new ButtonInfo { buttonText = "Toggle Weather", enableMethod =() => EnableAllRain(),disableMethod =() => DisableAllRain(), isTogglable = true},
                new ButtonInfo { buttonText = "Break Textures", method =() => BreakLights(), disableMethod =() => BreakLightDisable(), isTogglable = true},
                new ButtonInfo { buttonText = "Scintilla Tracers", method =() => ScintillaTracers(), isTogglable = true},
                new ButtonInfo { buttonText = "Tracers", method =() => Tracers(), isTogglable = true},
                new ButtonInfo { buttonText = "Bone ESP", method =() => DrawBones(), isTogglable = true},
                new ButtonInfo { buttonText = "Name Tags", method =() => NameTagESP(), isTogglable = true},
                new ButtonInfo { buttonText = "Box ESP", method =() => DrawBoxESP(), isTogglable = true},
                new ButtonInfo { buttonText = "Seroxen Launcher", method =() => BoyKisserBall(), isTogglable = true},
                new ButtonInfo { buttonText = "Swastika ESP", method =() => DrawSwastikaESP(), disableMethod =() => CleanUpSwaster(), isTogglable = true},
                new ButtonInfo { buttonText = "Jew ESP", method =() => DrawStarOfDavid(), disableMethod =() => CleanUpStar(), isTogglable = true},
                new ButtonInfo { buttonText = "Seroxen ESP", method =() => DrawSeroxenESP(), disableMethod =() => CleanUpSeroxenESP(), isTogglable = true},
                new ButtonInfo { buttonText = "Wireframe ESP", method =() => DrawWireframeESP(), disableMethod =() => ClearAll2(), isTogglable = true},
                new ButtonInfo { buttonText = "Spam Clap", method =() => test2(), isTogglable = true},
                new ButtonInfo { buttonText = "Spam Tag Thing", method =() => test(), isTogglable = true},
                new ButtonInfo { buttonText = "Chams Self", method =() => ChamsSelf(), disableMethod =()=> FlipChams(), isTogglable = true},
                new ButtonInfo { buttonText = "Chams", method =() => Chams(), disableMethod =()=> FlipChams(), isTogglable = true},
                new ButtonInfo { buttonText = "Hand Trails", enableMethod =() => HandTrails(),disableMethod =() => ClearHandTrails(), isTogglable = true},
                new ButtonInfo { buttonText = "Wrist Trails", enableMethod =() => WristTrails(),disableMethod =() => ClearHandTrails(), isTogglable = true},
                new ButtonInfo { buttonText = "Short Gun", method =() => GunLibrary.Fire(() => Overpowered.texst(GunLibrary.LockedRig), true)},
                new ButtonInfo { buttonText = "Short All", method =() => Overpowered.yeller()},
                new ButtonInfo { buttonText = "2D All", method =() => Overpowered.yeller2()},
                new ButtonInfo { buttonText = "Reverse 2D All", method =() => Overpowered.yeller3()},
                new ButtonInfo { buttonText = "Fix Scale All", method =() => Overpowered.yellerfix()},
            },

            new ButtonInfo[] { // Abuse Mods [7]
                new ButtonInfo { buttonText = "Back", method =() => currentCategory = 0, isTogglable = false},
                new ButtonInfo { buttonText = "Bring Bug To Right Hand [<color=green>RG</color>]", method =() => idk.GetThrowableBugWithOwnership(GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.position, ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed), isTogglable = true},
                new ButtonInfo { buttonText = "Bring Bug To Left Hand [<color=green>LG</color>]", method =() => idk.GetThrowableBugWithOwnership(GorillaLocomotion.GTPlayer.Instance.LeftHand.controllerTransform.position,ControllerInputPoller.instance.leftGrab || Mouse.current.leftButton.isPressed), isTogglable = true},
                new ButtonInfo { buttonText = "Bring Bug To Head [<color=green>RG</color>]", method =() => idk.GetThrowableBugWithOwnership(GorillaLocomotion.GTPlayer.Instance.headCollider.transform.position, ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed), isTogglable = true},
                new ButtonInfo { buttonText = "Bring Bug To Body [<color=green>RG</color>]", method =() => idk.GetThrowableBugWithOwnership(GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.position, ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed), isTogglable = true},
                new ButtonInfo { buttonText = "Water Splash Hands [<color=green>RG</color>]", method =() => idk.WaterHands(), isTogglable = true},
                new ButtonInfo { buttonText = "Water Splash Head [<color=green>RG</color>]", method =() => idk.WaterHead(), isTogglable = true},
                new ButtonInfo { buttonText = "Water Cum [<color=green>RG</color>]", method =() => idk.Cum(), isTogglable = true},
                new ButtonInfo { buttonText = "Water Splash Aura [<color=green>RG</color>]", method =() => idk.WaterAura(), isTogglable = true},
                new ButtonInfo { buttonText = "Reverse Water Splash Aura [<color=green>RG</color>]", method =() => idk.ReverseWaterAura(), isTogglable = true},
                new ButtonInfo { buttonText = "Reverse Water Splash Aura V2 [<color=green>RG</color>]", method =() => idk.ReverseWaterAuraV2(), isTogglable = true},
                new ButtonInfo { buttonText = "Bring All In Party To DIDDYPARTY", method =() => idk.JoinRoomWithPartyMembers("DIDDYPARTY"), isTogglable = false},
                new ButtonInfo { buttonText = "Bring All In Party To EPSTEINISLAND", method =() => idk.JoinRoomWithPartyMembers("EPSTEINISLAND"), isTogglable = false},
                new ButtonInfo { buttonText = "laqusha", method =() => laquishanetwork(), isTogglable = false},

            },

            new ButtonInfo[] { // Advantage Mods [8]
                new ButtonInfo { buttonText = "Back", method =() => currentCategory = 0, isTogglable = false},
                new ButtonInfo { buttonText = "Tag Self [<color=green>RT</color>]", method =() => Advantage.TagSelf(), isTogglable = true},
                new ButtonInfo { buttonText = "Tag All", method =() => Advantage.TagAll(), isTogglable = false},
                new ButtonInfo { buttonText = "Tag Gun", method =() => GunLibrary.Fire(() => Advantage.TagGunInstant(GunLibrary.LockedRig), true)},
                new ButtonInfo { buttonText = "Tag Aura [<color=green>RG</color>]", method =() => Advantage.TagAura(), isTogglable = true},
            },

            new ButtonInfo[] { // OP Mods [9]
                new ButtonInfo { buttonText = "Back", method =() => currentCategory = 0, isTogglable = false},
                new ButtonInfo { buttonText = "App Quit Gun", method =() => GunLibrary.Fire(() => Overpowered.appq(GunLibrary.LockedRig), true)},
                new ButtonInfo { buttonText = "App Quit All", method =() => Overpowered.RaiseRandomEvent180()},
                new ButtonInfo { buttonText = "Lag Gun", method =() => GunLibrary.Fire(() => Overpowered.LagPlayer(GunLibrary.LockedRig), true)},
                new ButtonInfo { buttonText = "Lag All", method =() => Overpowered.LagAll()},
                new ButtonInfo { buttonText = "Lag Aura [<color=green>RG</color>]", method =() => Overpowered.LagAura()},
                new ButtonInfo { buttonText = "Tiny Lag Gun", method =() => GunLibrary.Fire(() => Overpowered.tinylag(GunLibrary.LockedRig), true)},
                new ButtonInfo { buttonText = "Tiny Lag All", method =() => Overpowered.LagtinyAll()},
                new ButtonInfo { buttonText = "Tiny Lag Aura [<color=green>RG</color>]", method =() => Overpowered.TinyLagAura()},
                new ButtonInfo { buttonText = "Spike Lag Gun", method =() => GunLibrary.Fire(() => Overpowered.CrashPlayer(GunLibrary.LockedRig), true)},
                new ButtonInfo { buttonText = "Spike Lag All", method =() => Overpowered.CrashAll()},
                new ButtonInfo { buttonText = "Spike Lag Aura [<color=green>RG</color>]", method =() => Overpowered.CrashAura()},
                new ButtonInfo { buttonText = "Freeze Server", enableMethod =() => SerializePatch.OverrideSerialization = () => false, method =() => Overpowered.FreezeServer(), disableMethod =() => SerializePatch.OverrideSerialization = null },
                new ButtonInfo { buttonText = "Freeze Spike Server", enableMethod =() => SerializePatch.OverrideSerialization = () => false, method =() => Overpowered.FreezeServer(0f, 22), disableMethod =() => SerializePatch.OverrideSerialization = null, isTogglable = false },
                new ButtonInfo { buttonText = "Mega Freeze Spike Server (almost insta kick lol)", enableMethod =() => SerializePatch.OverrideSerialization = () => false, method =() => Overpowered.FreezeServer(0f, 99), disableMethod =() => SerializePatch.OverrideSerialization = null, isTogglable = false },
            },

            new ButtonInfo[] { // Customization [10]
                new ButtonInfo { buttonText = "Back To Settings", method =() => currentCategory = 2, isTogglable = false},
                new ButtonInfo
                {
                    buttonText = "Change Theme",
                    overlapText = "Change Theme: " + Themes.List[ThemeValue].Name,
                    Up =() => ChangeTheme(true),
                    Down =() => ChangeTheme(false),
                    isIncremental = true,
                    isTogglable = false
                },
                new ButtonInfo
                {
                    buttonText = "Change Menu Scale",
                    overlapText = "Change Menu Scale: x" + menuScaleMulti,
                    Up =() => ChangeScale(true),
                    Down =() => ChangeScale(false),
                    isIncremental = true,
                    isTogglable = false
                },
                new ButtonInfo
                {
                    buttonText = "Change Button Sound",
                    overlapText = "Change Button Sound: " + Sounds.List[Sounds.soundValue].Name,
                    Up =() => Sounds.ChangeSound(true),
                    Down =() => Sounds.ChangeSound(false),
                    isIncremental = true,
                    isTogglable = false
                },
                new ButtonInfo { buttonText = "Toggle Open Sound", method =() => opensound = !opensound, isTogglable = false},
                new ButtonInfo
                {
                    buttonText = "Change Open Sound",
                    overlapText = "Change Open Sound: " + Sounds.ListOpenSounds[Sounds.soundValueOpen].Name,
                    Up =() => Sounds.ChangeOpenAndCloseSound(true),
                    Down =() => Sounds.ChangeOpenAndCloseSound(false),
                    isIncremental = true,
                    isTogglable = false
                },
                new ButtonInfo { buttonText = "Toggle Drop Menu", method =() => drop = !drop, isTogglable = false},
                new ButtonInfo
                {
                    buttonText = "Change Animation Type",
                    overlapText = "Change Animation Type: " + animationType.ToString(),
                    Up =() => ChangeAnimationType(true),
                    Down =() => ChangeAnimationType(false),
                    isIncremental = true,
                    isTogglable = false
                },
                new ButtonInfo
                {
                    buttonText = "Change Easing Type",
                    overlapText = "Change Easing Type: " + easingType.ToString(),
                    Up =() => ChangeEasingType(true),
                    Down =() => ChangeEasingType(false),
                    isIncremental = true,
                    isTogglable = false
                },
                new ButtonInfo { buttonText = "Toggle OG Wide Menu [<color=pink>Scintilla</color>]", method =() => scintillawidemenu = !scintillawidemenu, isTogglable = false},
                new ButtonInfo { buttonText = "Toggle L_0xLilLean", method =() => L_0 = !L_0, isTogglable = false},
                new ButtonInfo { buttonText = "Toggle Willy Wiper", method =() => ww = !ww, isTogglable = false},
                new ButtonInfo { buttonText = "Toggle Side Bar", method =() => sidebarenable = !sidebarenable, isTogglable = false},
                new ButtonInfo { buttonText = "Change Side Bar Position", method =() => IsCatLeft = !IsCatLeft, isTogglable = false},
                new ButtonInfo { buttonText = "Change Side Bar Rotation [<color=red>BROKEN</color>]", method =() => IsCatRotated = !IsCatRotated, isTogglable = false},
                new ButtonInfo { buttonText = "Toggle Side Page Buttons", method =() => sidebuttons = !sidebuttons, isTogglable = false},
                new ButtonInfo { buttonText = "Toggle Rounding", method =() => rounding = !rounding, isTogglable = false},
                new ButtonInfo { buttonText = "Toggle Outline Menu", method =() => outlining = !outlining, isTogglable = false},
                new ButtonInfo { buttonText = "Toggle Outline Buttons", method =() => outliningButtons = !outliningButtons, isTogglable = false},
                new ButtonInfo { buttonText = "Toggle Right Hand", method =() => rightHanded = !rightHanded, isTogglable = false},
                new ButtonInfo { buttonText = "Toggle Notifications", method =() => s2.NotifiLib.Disablenotifcations = false,disableMethod =() => s2.NotifiLib.Disablenotifcations = true, isTogglable = true, enabled = !s2.NotifiLib.Disablenotifcations},
                new ButtonInfo { buttonText = "Toggle Disconnect Button", method =() =>  disconnectButton = !disconnectButton, isTogglable = false},
            },
            new ButtonInfo[] { // Gun Lib [11]
                new ButtonInfo { buttonText = "Back To Settings", method =() => currentCategory = 2, isTogglable = false},
                new ButtonInfo { buttonText = "Disable Gun Line", method =() => GunLibrary.line = !GunLibrary.line, isTogglable = false},
                new ButtonInfo { buttonText = "Test Gun",  method =() => GunLibrary.Fire(null, true), isTogglable = true},
                new ButtonInfo
                {
                    buttonText = "Change Gun Style",
                    overlapText = "Change Gun Style: " + GunLibrary.Shape.ToString(),
                    Up =() => GunLibrary.NextShape(),
                    Down =() => GunLibrary.PrevShape(),
                    isIncremental = true,
                    isTogglable = false
                },
                new ButtonInfo
                {
                    buttonText = "Change Gun Scale",
                    overlapText = "Change Gun Scale: x" + GunLibrary.gunsizemulti,
                    Up =() => GunLibrary.ChangeGunScale(true),
                    Down =() => GunLibrary.ChangeGunScale(false),
                    isIncremental = true,
                    isTogglable = false
                },
            }
        };
    }
}
