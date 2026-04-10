using femboy.Classes;
using PlayFab.ClientModels;
using StupidTemplate.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace StupidTemplate.Classes
{
    public class Speed
    {
        public float flyspeed = 75f;
        public float velflyspeed = 750000f;
    }
    public class Speedss
    {
        public static int speedValue = 0;
        public static void ChangeFlySpeed(bool forward)
        {
            if (forward && speedValue >= (flyspeeds.Length - 1)) speedValue = 0;
            else if (!forward && speedValue <= 0) speedValue = (flyspeeds.Length - 1);
            else speedValue = speedValue + (forward ? 1 : -1);
        }
        public static Speed[] flyspeeds = new Speed[]
        {
            new Speed() { flyspeed = 75f },
            new Speed() { flyspeed = 100f },
            new Speed() { flyspeed = 200f },
            new Speed() { flyspeed = 25f },
            new Speed() { flyspeed = 10f },
        };

        public static int velspeedValue = 0;
        public static void ChangeVelFlySpeed(bool forward)
        {
            if (forward && velspeedValue >= (velflyspeeds.Length - 1)) velspeedValue = 0;
            else if (!forward && velspeedValue <= 0) velspeedValue = (velflyspeeds.Length - 1);
            else velspeedValue = velspeedValue + (forward ? 1 : -1);
        }
        public static Speed[] velflyspeeds = new Speed[]
        {
            new Speed() { velflyspeed = 750000f },
            new Speed() { velflyspeed = 1000000f },
            new Speed() { velflyspeed = 20000000f },
            new Speed() { velflyspeed = 250000f },
            new Speed() { velflyspeed = 100000f },
        };
    }
    public class Sound
    {
        public string Name;
        public bool soundgtag = false;
        public string soundUrl;
        public string soundUrlOpen;
        public string soundUrlClose;
        public int soundId = 169;
        public bool wav = false;
    }
    public class Sounds
    {
        public static int soundValue = 0;
        public static int soundValueOpen = 0;
        public static void ChangeSound(bool forward)
        {
            int direction = forward ? 1 : -1;
            soundValue = (soundValue + direction + List.Length) % List.Length;

            Main.GetIndex("Change Button Sound").overlapText = "Change Button Sound: " + List[soundValue].Name;
        }
        public static void ChangeOpenAndCloseSound(bool forward)
        {
            int direction = forward ? 1 : -1;
            soundValueOpen = (soundValueOpen + direction + ListOpenSounds.Length) % ListOpenSounds.Length;

            Main.GetIndex("Change Open Sound").overlapText = "Change Open Sound: " + ListOpenSounds[soundValueOpen].Name;
        }
        public static Sound[] List = new Sound[]
        {
            new Sound() { soundUrl = "https://raw.githubusercontent.com/boxxin/clcik/main/365837__swordmaster767__click.mp3", Name = "sryx"},
            new Sound() { soundUrl = "https://files.catbox.moe/k7pe48.mp3", Name = "chqser"},
            new Sound() { soundUrl = "https://raw.githubusercontent.com/boxxin/clcik/main/493551__original_sound__user-interface-clicks-and-buttons-1.mp3", Name = "pop"},
            new Sound() { soundUrl = "https://raw.githubusercontent.com/boxxin/clcik/main/721502__baggonotes__button_click1.mp3", Name = "idk"},
            new Sound() { soundUrl = "https://raw.githubusercontent.com/boxxin/clcik/main/750607__deadrobotmusic__notification-sound-1.mp3", Name = "bwom"},
            new Sound() { soundUrl = "https://raw.githubusercontent.com/boxxin/clcik/main/creamy.mp3", Name = "creamy"},
            new Sound() { soundUrl = "https://raw.githubusercontent.com/divinelol/template/main/click.wav", wav = true, Name = "tmobile"},
            new Sound() { soundgtag = true, soundId = 84, Name = "pop 2"},
            new Sound() { soundgtag = true, soundId = 76, Name = "toy"},
            new Sound() { soundgtag = true, soundId = 4, Name = "carpet"},
            new Sound() { soundgtag = true, soundId = 66, Name = "key"},
            new Sound() { soundgtag = true, soundId = 67, Name = "button"},
            new Sound() { soundgtag = true, soundId = 169, Name = "slider"},
        };
        public static Sound[] ListOpenSounds = new Sound[]
        {
            new Sound()
            {
                Name = "chqser",
                soundUrlOpen = "https://files.catbox.moe/8wva6h.mp3",
                soundUrlClose = "https://files.catbox.moe/74t5ho.mp3",
            },
            new Sound()
            {
                Name = "chqser wii slidefly",
                soundUrlOpen = "https://files.catbox.moe/xawux8.mp3",
                soundUrlClose = "https://files.catbox.moe/r6ai0j.mp3",
            },
            new Sound()
            {
                Name = "chqser wii modal",
                soundUrlOpen = "https://files.catbox.moe/5tsh6e.mp3",
                soundUrlClose = "https://files.catbox.moe/3a2juv.mp3",
            },
            new Sound()
            {
                Name = "chqser wii detail",
                soundUrlOpen = "https://files.catbox.moe/gneimo.mp3",
                soundUrlClose = "https://files.catbox.moe/5i7i5b.mp3",
            },
            new Sound()
            {
                Name = "shit default sound",
                soundUrlOpen = "https://raw.githubusercontent.com/divinelol/template/main/open2.wav",
                soundUrlClose = "https://raw.githubusercontent.com/divinelol/template/main/close.wav",
                wav = true
            },
        };
    }

    public class Theme
    {
        public string Name = "Main";
        public float Speed = 0.66f;
        public bool tryy = false;
        public bool tryy2 = false;
        public Color[] Colors;
        public Color Color;
    }

    public class Themes
    {
        public static Color PastelPink = new Color(0.98f, 0.72f, 0.82f);
        public static Color PastelBlue = new Color(0.70f, 0.88f, 0.98f);
        public static Color PastelGreen = new Color(0.78f, 0.98f, 0.76f);
        public static Color PastelYellow = new Color(0.98f, 0.92f, 0.70f);
        public static Color PastelPurple = new Color(0.88f, 0.76f, 0.98f);
        public static Color PastelOrange = new Color(0.98f, 0.82f, 0.70f);
        public static Color PastelLavender = new Color(0.82f, 0.78f, 0.98f);
        public static Color PastelMint = new Color(0.72f, 0.98f, 0.90f);
        public static Color PastelPeach = new Color(0.98f, 0.85f, 0.78f);
        public static Color PastelCoral = new Color(0.98f, 0.74f, 0.72f); 
        public static Color PastelRed = new Color(0.98f, 0.60f, 0.60f);
        public static Theme[] List = new Theme[]
{
    #region Sryx
    new Theme()
    {
        Name = "Sryx",
        Colors = new Color[]
        {
            ColorLib.PastelGreen,
            ColorLib.PastelBlue,
        }
    },
    #endregion
    #region PastelPink
    new Theme()
    {
        Name = "PastelPink",
        Colors = new Color[]
        {
            PastelPink,
            PastelPink,
        }
    },
    #endregion
    #region PastelBlue
    new Theme()
    {
        Name = "PastelBlue",
        Colors = new Color[]
        {
            PastelBlue,
            PastelBlue,
        }
    },
    #endregion
    #region PastelGreen
    new Theme()
    {
        Name = "PastelGreen",
        Colors = new Color[]
        {
            PastelGreen,
            PastelGreen,
        }
    },
    #endregion
    #region PastelYellow
    new Theme()
    {
        Name = "PastelYellow",
        Colors = new Color[]
        {
            PastelYellow,
            PastelYellow,
        }
    },
    #endregion
    #region PastelPurple
    new Theme()
    {
        Name = "PastelPurple",
        Colors = new Color[]
        {
            PastelPurple,
            PastelPurple,
        }
    },
    #endregion
    #region PastelOrange
    new Theme()
    {
        Name = "PastelOrange",
        Colors = new Color[]
        {
            PastelOrange,
            PastelOrange,
        }
    },
    #endregion
    #region PastelLavender
    new Theme()
    {
        Name = "PastelLavender",
        Colors = new Color[]
        {
            PastelLavender,
            PastelLavender,
        }
    },
    #endregion
    #region PastelMint
    new Theme()
    {
        Name = "PastelMint",
        Colors = new Color[]
        {
            PastelMint,
            PastelMint,
        }
    },
    #endregion
    #region PastelPeach
    new Theme()
    {
        Name = "PastelPeach",
        Colors = new Color[]
        {
            PastelPeach,
            PastelPeach,
        }
    },
    #endregion
    #region PastelCoral
    new Theme()
    {
        Name = "PastelCoral",
        Colors = new Color[]
        {
            PastelCoral,
            PastelCoral,
        }
    },
    #endregion
    #region PastelRed
    new Theme()
    {
        Name = "PastelRed",
        Colors = new Color[]
        {
            PastelRed,
            PastelRed,
        }
    },
    #endregion
    #region U.S.A.
    new Theme()
    {
        Name = "U.S.A.",
        Colors = new Color[]
        {
             new Color32(33, 20, 57, 255),
             new Color32(55, 40, 79, 255),
        }
    },
    #endregion
    #region Sus
    new Theme()
    {
        Name = "sus",
        Colors = new Color[]
        {
            Colors.Blend(ColorLib.HyperNeonGreen, ColorLib.SoftBlue),
            ColorLib.UltraDarkPurple,
        }
    },
    #endregion
    #region Rainbow Outline
    new Theme()
    {
        Name = "Rainbow Outline",
        tryy = true,
        Colors = new Color[]
        {
            ColorLib.UltraDarkGrey2,
            ColorLib.UltraDarkGrey2,
        }
    },
    #endregion
    #region Seroxen
    new Theme()
    {
        Name = "Seroxen",
        tryy2 = true,
        Speed = 1f,
        Colors = new Color[]
        {
            ColorLib.RadiantPink,
            ColorLib.Cyan,
            ColorLib.DeepPurple,
        }
    },
    #endregion
    #region Vibrant
    new Theme()
    {
        Name = "Vibrant",
        Colors = new Color[]
        {
            ColorLib.ElectricLime,
            ColorLib.HotMagenta,
            ColorLib.SolarOrange,
            ColorLib.NeonTurquoise,
            ColorLib.RadiantPink,
            ColorLib.ShockingBlue
        }
    },
    #endregion
    #region Mystic
    new Theme()
    {
        Name = "Mystic",
        Colors = new Color[]
        {
            ColorLib.MysticPurple,
            ColorLib.EnchantedBlue,
            ColorLib.FairyPink,
            ColorLib.DragonGreen,
            ColorLib.ShadowBlack
        }
    },
    #endregion
    #region Pastel
    new Theme()
    {
        Name = "Pastel",
        Colors = new Color[]
        {
            ColorLib.PastelMint,
            ColorLib.PastelPeach,
            ColorLib.PastelLavender,
            ColorLib.PastelSky,
            ColorLib.PastelRose
        }
    },
    #endregion
    #region Neon RGB
    new Theme()
    {
        Name = "Neon RGB",
        Colors = new Color[]
        {
            ColorLib.NeonLime,
            ColorLib.NeonViolet,
            ColorLib.NeonCyan,
            ColorLib.NeonCoral,
            ColorLib.NeonAmber
        }
    },
    #endregion
    #region Sryx Dark
    new Theme()
    {
        Name = "Sryx Dark",
        Colors = new Color[]
        {
            Colors.Blend(Color.black, Color.gray * 0.6f),
            Colors.Blend(Color.black, Color.gray * 0.4f)
        }
    },
    #endregion
    #region Nightfall
    new Theme()
    {
        Name = "Nightfall",
        Colors = new Color[]
        {
            Colors.Blend(Color.black, Color.blue * 0.7f),
            Colors.Blend(Color.black, Color.cyan * 0.3f)
        }
    },
    #endregion
    #region Eclipse
    new Theme()
    {
        Name = "Eclipse",
        Colors = new Color[]
        {
           Colors.Blend(Color.black, Color.magenta * 0.5f),
           Colors.Blend(Color.black, Color.red * 0.4f)
        }
    },
    #endregion
    #region Iron Vein
    new Theme()
    {
        Name = "Iron Vein",
        Colors = new Color[]
        {
            Colors.Blend(Color.black, Color.gray * 0.8f),
            Colors.Blend(Color.black, Color.red * 0.3f)
        }
    },
    #endregion
    #region Vanta
    new Theme()
    {
        Name = "Vanta",
        Colors = new Color[]
        {
            Color.black,
            Colors.Blend(Color.black, Color.gray * 0.2f)
        }
    },
    #endregion
    #region Phantom Glow
    new Theme()
    {
        Name = "Phantom Glow",
        Colors = new Color[]
        {
            Colors.Blend(Color.black, Color.green * 0.6f),
            Colors.Blend(Color.black, Color.cyan * 0.4f)
        }
    },
    #endregion
    #region Crimson Dusk
    new Theme()
    {
        Name = "Crimson Dusk",
        Colors = new Color[]
        {
            Colors.Blend(Color.black, Color.red * 0.6f),
            Colors.Blend(Color.black, Color.magenta * 0.5f)
        }
    },
    #endregion
    #region Deep Abyss
    new Theme()
    {
        Name = "Deep Abyss",
        Colors = new Color[]
        {
            Colors.Blend(Color.black, Color.blue * 0.8f),
            Colors.Blend(Color.black, Color.blue * 0.4f)
        }
    },
    #endregion
    #region Smokewraith
    new Theme()
    {
         Name = "Smokewraith",
         Colors = new Color[]
         {
             Colors.Blend(Color.black, Color.gray * 0.7f),
             Colors.Blend(Color.black, Color.white * 0.2f)
         }
    },
    #endregion
    #region Twilight
    new Theme()
    {
        Name = "Twilight",
        Colors = new Color[]
        {
            Colors.Blend(Colors.Blend(Color.blue, Color.magenta), Color.white),
            Colors.Blend(Color.blue, Color.magenta)
        }
    },
    #endregion
    #region Juul
    new Theme()
    {
        Name = "Juul",
        Colors = new Color[]
        {
            Colors.Blend(Colors.Blend(Color.green, Color.cyan), Color.white),
            Colors.Blend(Color.magenta, Color.white)
        }
    },
    #endregion
    #region Shadow
    new Theme()
    {
        Name = "Shadow",
        Colors = new Color[]
        {
            Colors.Blend(Color.gray, Color.black, Color.black),
            Colors.Blend(Color.gray, Color.black, Color.black, Color.black),
        }
    },
    #endregion
    #region Deep Sea
    new Theme()
    {
        Name = "Deep Sea",
        Colors = new Color[]
        {
            Colors.Blend(Color.blue, Color.cyan),
            Color.blue
        }
    },
    #endregion
    #region Rose
    new Theme()
    {
        Name = "Rose",
        Colors = new Color[]
        {
            Colors.Blend(Colors.Blend(Color.magenta, Color.magenta, Color.red), Color.white),
            Colors.Blend(Color.magenta, Color.magenta, Color.red)
        }
    },
    #endregion
    #region Inferno
    new Theme()
    {
        Name = "Inferno",
        Colors = new Color[]
        {
            Color.red,
            Colors.Blend(Color.black, Color.red)
        }
    },
    #endregion
    #region Sunset
    new Theme()
    {
        Name = "Sunset",
        Colors = new Color[]
        {
            Colors.Blend(Color.red, Color.yellow),
            Colors.Blend(Color.magenta, Color.red, Color.red)
        }
    },
    #endregion
    #region Forest
    new Theme()
    {
        Name = "Forest",
        Colors = new Color[]
        {
            Color.green,
            Colors.Blend(Color.green, Color.black)
        }
    },
    #endregion
    #region Frost
    new Theme()
    {
        Name = "Frost",
        Colors = new Color[]
        {
            Colors.Blend(Color.cyan, Color.white),
            Color.cyan
        }
    },
    #endregion
    #region Midnight
    new Theme()
    {
        Name = "Midnight",
        Colors = new Color[]
        {
            Colors.Blend(Color.blue, Color.black),
            Color.black
        }
    },
    #endregion
    #region Coral
    new Theme()
    {
        Name = "Coral",
        Colors = new Color[]
        {
            Colors.Blend(Color.red, Color.magenta, Color.white),
            Colors.Blend(Color.red, Color.yellow)
        }
    },
    #endregion
    #region Amber
    new Theme()
    {
        Name = "Amber",
        Colors = new Color[]
        {
            Colors.Blend(Color.yellow, Color.red),
            Colors.Blend(Color.red, Color.black)
        }
    },
    #endregion
    #region Storm
    new Theme()
    {
        Name = "Storm",
        Colors = new Color[]
        {
            Colors.Blend(Color.gray, Color.blue),
            Colors.Blend(Color.black, Color.blue)
        }
    },
    #endregion
    #region Neon
    new Theme()
    {
        Name = "Neon",
        Colors = new Color[]
        {
            Colors.Blend(Color.magenta, Color.cyan),
            Color.magenta
        }
    },
    #endregion
    #region Aurora
    new Theme()
    {
        Name = "Aurora",
        Colors = new Color[]
        {
            Colors.Blend(Color.green, Color.cyan, Color.white),
            Colors.Blend(Color.blue, Color.magenta)
        }
    },
    #endregion
    #region Copper
    new Theme()
    {
        Name = "Copper",
        Colors = new Color[]
        {
            Colors.Blend(Color.red, Color.yellow, Color.black),
            Colors.Blend(Color.red, Color.black, Color.black)
        }
    },
    #endregion
    #region Sakura
    new Theme()
    {
        Name = "Sakura",
        Colors = new Color[]
        {
            Colors.Blend(Color.magenta, Color.white, Color.white),
            Colors.Blend(Color.magenta, Color.red, Color.white)
        }
    },
    #endregion
    #region Venom
    new Theme()
    {
        Name = "Venom",
        Colors = new Color[]
        {
            Colors.Blend(Color.green, Color.yellow),
            Colors.Blend(Color.green, Color.black)
        }
    },
    #endregion
    #region RGB
    new Theme()
    {
        Name = "RGB",
        Speed = 1.5f,
        Colors = new Color[]
        {
            Color.red,
            Colors.Blend(Color.red, Color.yellow),
            Color.yellow,
            Colors.Blend(Color.yellow, Color.green),
            Color.green,
            Colors.Blend(Color.green, Color.blue),
            Color.blue,
            Colors.Blend(Color.blue, Color.magenta),
            Color.magenta,
            Colors.Blend(Color.magenta, Color.red),
        }
    },
    #endregion
    #region Ocean Breeze
    new Theme()
    {
        Name = "Ocean Breeze",
        Speed = 0.5f,
        Colors = new Color[]
        {
            Colors.Blend(Color.cyan, Color.white, Color.white),
            Colors.Blend(Color.cyan, Color.blue),
            Colors.Blend(Color.blue, Color.blue, Color.cyan)
        }
    },
    #endregion
    #region Lava
    new Theme()
    {
        Name = "Lava",
        Speed = 0.8f,
        Colors = new Color[]
        {
            Colors.Blend(Color.red, Color.yellow, Color.yellow),
            Colors.Blend(Color.red, Color.red, Color.yellow),
            Colors.Blend(Color.red, Color.black),
            Colors.Blend(Color.red, Color.red, Color.black)
        }
    },
    #endregion
    #region Purple Haze
    new Theme()
    {
        Name = "Purple Haze",
        Speed = 0.55f,
        Colors = new Color[]
        {
            Colors.Blend(Color.magenta, Color.blue, Color.white),
            Colors.Blend(Color.magenta, Color.magenta, Color.blue),
            Colors.Blend(Color.blue, Color.magenta)
        }
    },
    #endregion
    #region Cotton Candy
    new Theme()
    {
        Name = "Cotton Candy",
        Speed = 0.6f,
        Colors = new Color[]
        {
            Colors.Blend(Color.cyan, Color.white, Color.white),
            Colors.Blend(Color.magenta, Color.white, Color.white),
            Colors.Blend(Color.cyan, Color.magenta, Color.white)
        }
    },
    #endregion
    #region Toxic
    new Theme()
    {
        Name = "Toxic",
        Speed = 0.9f,
        Colors = new Color[]
        {
            Colors.Blend(Color.green, Color.yellow, Color.yellow),
            Colors.Blend(Color.green, Color.green, Color.yellow),
            Colors.Blend(Color.green, Color.black)
        }
    },
    #endregion
    #region Galaxy
    new Theme()
    {
        Name = "Galaxy",
        Speed = 0.4f,
        Colors = new Color[]
        {
            Colors.Blend(Color.blue, Color.black, Color.black),
            Colors.Blend(Color.magenta, Color.blue, Color.black),
            Colors.Blend(Color.cyan, Color.blue),
            Colors.Blend(Color.magenta, Color.black)
        }
    },
    #endregion
    #region Mint
    new Theme()
    {
        Name = "Mint",
        Speed = 0.5f,
        Colors = new Color[]
        {
            Colors.Blend(Color.cyan, Color.green, Color.white),
            Colors.Blend(Color.cyan, Color.white, Color.white),
            Colors.Blend(Color.green, Color.cyan)
        }
    },
    #endregion
    #region Crimson
    new Theme()
    {
        Name = "Crimson",
        Speed = 0.7f,
        Colors = new Color[]
        {
            Colors.Blend(Color.red, Color.red, Color.black),
            Colors.Blend(Color.red, Color.magenta),
            Color.red
        }
    },
    #endregion
    #region Electric
    new Theme()
    {
        Name = "Electric",
        Speed = 1.2f,
        Colors = new Color[]
        {
            Colors.Blend(Color.cyan, Color.white),
            Colors.Blend(Color.blue, Color.cyan, Color.white),
            Colors.Blend(Color.blue, Color.magenta),
            Colors.Blend(Color.cyan, Color.blue)
        }
    },
    #endregion
    #region Golden Hour
    new Theme()
    {
        Name = "Golden Hour",
        Speed = 0.45f,
        Colors = new Color[]
        {
            Colors.Blend(Color.yellow, Color.white, Color.white),
            Colors.Blend(Color.yellow, Color.red),
            Colors.Blend(Color.red, Color.magenta, Color.yellow)
        }
    },
    #endregion
    #region Nebula
    new Theme()
    {
        Name = "Nebula",
        Speed = 0.6f,
        Colors = new Color[]
        {
            Colors.Blend(Color.magenta, Color.blue, Color.blue),
            Colors.Blend(Color.blue, Color.cyan),
            Colors.Blend(Color.magenta, Color.red),
            Colors.Blend(Color.blue, Color.black)
        }
    },
    #endregion
    #region Synthwave
    new Theme()
    {
        Name = "Synthwave",
        Speed = 1.0f,
        Colors = new Color[]
        {
            Colors.Blend(Color.magenta, Color.magenta, Color.blue),
            Colors.Blend(Color.cyan, Color.blue),
            Colors.Blend(Color.magenta, Color.red, Color.red),
            Colors.Blend(Color.cyan, Color.magenta)
        }
    },
    #endregion
    #region Blood Moon
    new Theme()
    {
        Name = "Blood Moon",
        Speed = 0.5f,
        Colors = new Color[]
        {
            Colors.Blend(Color.red, Color.black, Color.black),
            Colors.Blend(Color.red, Color.red, Color.black),
            Colors.Blend(Color.red, Color.magenta, Color.black)
        }
    },
    #endregion
    #region Arctic
    new Theme()
    {
        Name = "Arctic",
        Speed = 0.4f,
        Colors = new Color[]
        {
            Color.white,
            Colors.Blend(Color.cyan, Color.white, Color.white),
            Colors.Blend(Color.blue, Color.cyan, Color.white)
        }
    },
    #endregion
    #region Emerald
    new Theme()
    {
        Name = "Emerald",
        Speed = 0.6f,
        Colors = new Color[]
        {
            Colors.Blend(Color.green, Color.cyan),
            Colors.Blend(Color.green, Color.green, Color.cyan),
            Colors.Blend(Color.green, Color.black)
        }
    },
    #endregion
    #region Hyper
    new Theme()
    {
        Name = "Hyper",
        Speed = 1.8f,
        Colors = new Color[]
        {
            Color.red,
            Colors.Blend(Color.magenta, Color.red),
            Color.magenta,
            Colors.Blend(Color.blue, Color.magenta),
            Color.blue,
            Colors.Blend(Color.cyan, Color.blue),
            Color.cyan,
            Colors.Blend(Color.green, Color.cyan),
            Color.green,
            Colors.Blend(Color.yellow, Color.green),
            Color.yellow,
            Colors.Blend(Color.red, Color.yellow)
        }
    },
    #endregion
    #region Volcano
    new Theme()
    {
        Name = "Volcano",
        Speed = 0.75f,
        Colors = new Color[]
        {
            Colors.Blend(Color.red, Color.yellow),
            Colors.Blend(Color.red, Color.red, Color.black),
            Colors.Blend(Color.black, Color.red),
            Color.black
        }
    },
    #endregion
    #region Peacock
    new Theme()
    {
        Name = "Peacock",
        Speed = 0.65f,
        Colors = new Color[]
        {
            Colors.Blend(Color.cyan, Color.blue, Color.white),
            Colors.Blend(Color.green, Color.cyan),
            Colors.Blend(Color.blue, Color.magenta),
            Colors.Blend(Color.cyan, Color.green)
        }
    },
    #endregion
    #region Phantom
    new Theme()
    {
        Name = "Phantom",
        Speed = 0.5f,
        Colors = new Color[]
        {
            Colors.Blend(Color.gray, Color.white),
            Colors.Blend(Color.gray, Color.gray, Color.black),
            Colors.Blend(Color.black, Color.gray),
            Colors.Blend(Color.gray, Color.white, Color.white)
        }
    },
    #endregion
    #region Cherry Blossom
    new Theme()
    {
        Name = "Cherry Blossom",
        Speed = 0.5f,
        Colors = new Color[]
        {
            Colors.Blend(Color.magenta, Color.white, Color.white, Color.white),
            Colors.Blend(Color.red, Color.magenta, Color.white, Color.white),
            Colors.Blend(Color.magenta, Color.white, Color.white)
        }
    },
    #endregion
};
    }
}