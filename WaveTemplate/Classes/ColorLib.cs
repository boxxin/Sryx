using UnityEngine;

namespace femboy.Classes //stolen from my friends menu
{
    public class ColorLib
    {
        #region Non-Transparent Colors

        // Reds
        public static Color32 Red = new Color32(255, 0, 0, 255);
        public static Color32 DarkRed = new Color32(180, 0, 0, 255);
        public static Color32 Salmon = new Color32(250, 128, 114, 255);
        public static Color32 WineRed = new Color32(123, 0, 0, 255);
        public static Color32 IndianRed = new Color32(205, 92, 92, 255);
        public static Color32 Crimson = new Color32(220, 20, 60, 255);
        public static Color32 FireBrick = new Color32(178, 34, 34, 255);
        public static Color32 Coral = new Color32(255, 127, 80, 255);
        public static Color32 Tomato = new Color32(255, 99, 71, 255);
        public static Color32 Maroon = new Color32(128, 0, 0, 255);

        // Greens
        public static Color32 Green = new Color32(0, 255, 0, 255);
        public static Color32 Lime = new Color32(0, 128, 0, 255);
        public static Color32 DarkGreen = new Color32(0, 100, 0, 255);
        public static Color32 Olive = new Color32(128, 128, 0, 255);
        public static Color32 ForestGreen = new Color32(34, 139, 34, 255);
        public static Color32 SeaGreen = new Color32(46, 139, 87, 255);
        public static Color32 MediumSeaGreen = new Color32(60, 179, 113, 255);
        public static Color32 Aquamarine = new Color32(127, 255, 212, 255);
        public static Color32 MediumAquamarine = new Color32(102, 205, 170, 255);
        public static Color32 DarkSeaGreen = new Color32(143, 188, 143, 255);

        // Blues
        public static Color32 Blue = new Color32(0, 0, 255, 255);
        public static Color32 Navy = new Color32(0, 0, 128, 255);
        public static Color32 DarkBlue = new Color32(0, 0, 160, 255);
        public static Color32 RoyalBlue = new Color32(65, 105, 225, 255);
        public static Color32 DodgerBlue = new Color32(30, 144, 255, 255);
        public static Color32 DarkDodgerBlue = new Color32(8, 90, 177, 255);
        public static Color32 DeepSkyBlue = new Color32(0, 191, 255, 255);
        public static Color32 SkyBlue = new Color32(135, 206, 235, 255);
        public static Color32 SteelBlue = new Color32(70, 130, 180, 255);
        public static Color32 Cyan = new Color32(0, 255, 255, 255);

        // Yellows
        public static Color32 Yellow = new Color32(255, 255, 0, 255);
        public static Color32 Gold = new Color32(255, 215, 0, 255);
        public static Color32 LightYellow = new Color32(255, 255, 224, 255);
        public static Color32 LemonChiffon = new Color32(255, 250, 205, 255);
        public static Color32 Khaki = new Color32(240, 230, 140, 255);
        public static Color32 PaleGoldenrod = new Color32(238, 232, 170, 255);
        public static Color32 LightGoldenrodYellow = new Color32(250, 250, 210, 255);

        // Oranges
        public static Color32 Orange = new Color32(255, 165, 0, 255);
        public static Color32 DarkOrange = new Color32(255, 140, 0, 255);
        public static Color32 RedOrange = new Color32(255, 69, 0, 255);
        public static Color32 PeachPuff = new Color32(255, 218, 185, 255);
        public static Color32 DarkGoldenrod = new Color32(184, 134, 11, 255);
        public static Color32 Peru = new Color32(205, 133, 63, 255);
        public static Color32 OrangeRed = new Color32(255, 69, 0, 255);

        // Purples
        public static Color32 Magenta = new Color32(255, 0, 255, 255);
        public static Color32 Purple = new Color32(123, 3, 252, 255);
        public static Color32 Lavender = new Color32(230, 230, 250, 255);
        public static Color32 Plum = new Color32(221, 160, 221, 255);
        public static Color32 Indigo = new Color32(75, 0, 130, 255);
        public static Color32 MediumOrchid = new Color32(186, 85, 211, 255);
        public static Color32 SlateBlue = new Color32(106, 90, 205, 255);
        public static Color32 DarkSlateBlue = new Color32(72, 61, 139, 255);

        // Pinks
        public static Color32 Pink = new Color32(255, 192, 203, 255);
        public static Color32 LightSalmon = new Color32(255, 160, 122, 255);
        public static Color32 DarkSalmon = new Color32(233, 150, 122, 255);
        public static Color32 LightCoral = new Color32(240, 128, 128, 255);
        public static Color32 MistyRose = new Color32(255, 228, 225, 255);
        public static Color32 HotPink = new Color32(255, 105, 180, 255);
        public static Color32 DeepPink = new Color32(255, 20, 147, 255);

        // Browns
        public static Color32 Brown = new Color32(165, 42, 42, 255);
        public static Color32 RosyBrown = new Color32(188, 143, 143, 255);
        public static Color32 SaddleBrown = new Color32(139, 69, 19, 255);
        public static Color32 Sienna = new Color32(160, 82, 45, 255);
        public static Color32 Chocolate = new Color32(210, 105, 30, 255);
        public static Color32 SandyBrown = new Color32(244, 164, 96, 255);
        public static Color32 BurlyWood = new Color32(222, 184, 135, 255);
        public static Color32 Tan = new Color32(210, 180, 140, 255);

        // Whites
        public static Color32 White = new Color32(255, 255, 255, 255);
        public static Color32 Linen = new Color32(250, 240, 230, 255);
        public static Color32 OldLace = new Color32(253, 245, 230, 255);
        public static Color32 SeaShell = new Color32(255, 245, 238, 255);
        public static Color32 MintCream = new Color32(245, 255, 250, 255);

        // Blacks and Grays
        public static Color32 Black = new Color32(0, 0, 0, 255);
        public static Color32 Grey = new Color32(128, 128, 128, 255);
        public static Color32 LightGrey = new Color32(192, 192, 192, 255);
        public static Color32 DarkGrey = new Color32(80, 80, 80, 255);
        public static Color32 DarkerGrey = new Color32(40, 40, 40, 255);
        #endregion

        #region Transparent Colors

        // Reds
        public static Color32 RedTransparent = new Color32(255, 0, 0, 80);
        public static Color32 DarkRedTransparent = new Color32(180, 0, 0, 80);
        public static Color32 SalmonTransparent = new Color32(250, 128, 114, 80);
        public static Color32 IndianRedTransparent = new Color32(205, 92, 92, 80);
        public static Color32 CrimsonTransparent = new Color32(220, 20, 60, 80);
        public static Color32 WineRedTransparent = new Color32(123, 0, 0, 80);
        public static Color32 FireBrickTransparent = new Color32(178, 34, 34, 80);
        public static Color32 CoralTransparent = new Color32(255, 127, 80, 80);
        public static Color32 TomatoTransparent = new Color32(255, 99, 71, 80);
        public static Color32 MaroonTransparent = new Color32(128, 0, 0, 80);

        // Greens
        public static Color32 GreenTransparent = new Color32(0, 255, 0, 80);
        public static Color32 LimeTransparent = new Color32(0, 128, 0, 80);
        public static Color32 DarkGreenTransparent = new Color32(0, 100, 0, 80);
        public static Color32 OliveTransparent = new Color32(128, 128, 0, 80);
        public static Color32 ForestGreenTransparent = new Color32(34, 139, 34, 80);
        public static Color32 SeaGreenTransparent = new Color32(46, 139, 87, 80);
        public static Color32 MediumSeaGreenTransparent = new Color32(60, 179, 113, 80);
        public static Color32 AquamarineTransparent = new Color32(127, 255, 212, 80);
        public static Color32 MediumAquamarineTransparent = new Color32(102, 205, 170, 80);
        public static Color32 DarkSeaGreenTransparent = new Color32(143, 188, 143, 80);

        // Blues
        public static Color32 BlueTransparent = new Color32(0, 0, 255, 80);
        public static Color32 NavyTransparent = new Color32(0, 0, 128, 80);
        public static Color32 DarkBlueTransparent = new Color32(0, 0, 139, 80);
        public static Color32 RoyalBlueTransparent = new Color32(65, 105, 225, 80);
        public static Color32 DodgerBlueTransparent = new Color32(30, 144, 255, 80);
        public static Color32 DarkDodgerBlueTransparent = new Color32(8, 90, 177, 80);
        public static Color32 DeepSkyBlueTransparent = new Color32(0, 191, 255, 80);
        public static Color32 SkyBlueTransparent = new Color32(135, 206, 235, 80);
        public static Color32 SteelBlueTransparent = new Color32(70, 130, 180, 80);
        public static Color32 CyanTransparent = new Color32(0, 255, 255, 80);

        // Yellows
        public static Color32 YellowTransparent = new Color32(255, 255, 0, 80);
        public static Color32 GoldTransparent = new Color32(255, 215, 0, 80);
        public static Color32 LightYellowTransparent = new Color32(255, 255, 224, 80);
        public static Color32 LemonChiffonTransparent = new Color32(255, 250, 205, 80);
        public static Color32 KhakiTransparent = new Color32(240, 230, 140, 80);
        public static Color32 PaleGoldenrodTransparent = new Color32(238, 232, 170, 80);
        public static Color32 LightGoldenrodYellowTransparent = new Color32(250, 250, 210, 80);

        // Oranges
        public static Color32 OrangeTransparent = new Color32(255, 165, 0, 80);
        public static Color32 DarkOrangeTransparent = new Color32(255, 140, 0, 80);
        public static Color32 RedOrangeTransparent = new Color32(255, 69, 0, 80);
        public static Color32 PeachPuffTransparent = new Color32(255, 218, 185, 80);
        public static Color32 DarkGoldenrodTransparent = new Color32(184, 134, 11, 80);
        public static Color32 PeruTransparent = new Color32(205, 133, 63, 80);
        public static Color32 OrangeRedTransparent = new Color32(255, 69, 0, 80);

        // Purples
        public static Color32 MagentaTransparent = new Color32(255, 0, 255, 80);
        public static Color32 PurpleTransparent = new Color32(123, 3, 252, 80);
        public static Color32 LavenderTransparent = new Color32(230, 230, 250, 80);
        public static Color32 PlumTransparent = new Color32(221, 160, 221, 80);
        public static Color32 IndigoTransparent = new Color32(75, 0, 130, 80);
        public static Color32 MediumOrchidTransparent = new Color32(186, 85, 211, 80);
        public static Color32 SlateBlueTransparent = new Color32(106, 90, 205, 80);
        public static Color32 DarkSlateBlueTransparent = new Color32(72, 61, 139, 80);

        // Pinks
        public static Color32 PinkTransparent = new Color32(255, 192, 203, 80);
        public static Color32 LightSalmonTransparent = new Color32(255, 160, 122, 80);
        public static Color32 DarkSalmonTransparent = new Color32(233, 150, 122, 80);
        public static Color32 LightCoralTransparent = new Color32(240, 128, 128, 80);
        public static Color32 MistyRoseTransparent = new Color32(255, 228, 225, 80);
        public static Color32 HotPinkTransparent = new Color32(255, 105, 180, 80);
        public static Color32 DeepPinkTransparent = new Color32(255, 20, 147, 80);

        // Browns
        public static Color32 BrownTransparent = new Color32(165, 42, 42, 80);
        public static Color32 RosyBrownTransparent = new Color32(188, 143, 143, 80);
        public static Color32 SaddleBrownTransparent = new Color32(139, 69, 19, 80);
        public static Color32 SiennaTransparent = new Color32(160, 82, 45, 80);
        public static Color32 ChocolateTransparent = new Color32(210, 105, 30, 80);
        public static Color32 SandyBrownTransparent = new Color32(244, 164, 96, 80);
        public static Color32 BurlyWoodTransparent = new Color32(222, 184, 135, 80);
        public static Color32 TanTransparent = new Color32(210, 180, 140, 80);

        // Whites
        public static Color32 WhiteTransparent = new Color32(255, 255, 255, 80);
        public static Color32 LightWhiteTransparent = new Color32(255, 255, 255, 10);
        public static Color32 LinenTransparent = new Color32(250, 240, 230, 80);
        public static Color32 OldLaceTransparent = new Color32(253, 245, 230, 80);
        public static Color32 SeaShellTransparent = new Color32(255, 245, 238, 80);
        public static Color32 MintCreamTransparent = new Color32(245, 255, 250, 80);

        // Blacks and Grays
        public static Color32 BlackTransparent = new Color32(0, 0, 0, 80);
        public static Color32 GreyTransparent = new Color32(80, 80, 80, 80);
        public static Color32 LightGreyTransparent = new Color32(192, 192, 192, 80);
        public static Color32 DarkGreyTransparent = new Color32(40, 40, 40, 80);
        public static Color32 DarkerGreyTransparent = new Color32(40, 40, 40, 80);
        #endregion

        #region Extra Colors

        // Extra Reds
        public static Color32 Scarlet = new Color32(255, 36, 0, 255);
        public static Color32 Ruby = new Color32(224, 17, 95, 255);
        public static Color32 Cherry = new Color32(222, 49, 99, 255);

        // Extra Greens
        public static Color32 SpringGreen = new Color32(0, 255, 127, 255);
        public static Color32 Emerald = new Color32(80, 200, 120, 255);
        public static Color32 Jade = new Color32(0, 168, 107, 255);

        // Extra Blues
        public static Color32 MidnightBlue = new Color32(25, 25, 112, 255);
        public static Color32 BabyBlue = new Color32(137, 207, 240, 255);
        public static Color32 ElectricBlue = new Color32(125, 249, 255, 255);

        // Extra Purples
        public static Color32 Violet = new Color32(143, 0, 255, 255);
        public static Color32 Amethyst = new Color32(153, 102, 204, 255);
        public static Color32 Grape = new Color32(111, 45, 168, 255);

        // Extra Pinks
        public static Color32 Rose = new Color32(255, 0, 127, 255);
        public static Color32 Bubblegum = new Color32(255, 193, 204, 255);

        // Extra Browns
        public static Color32 Coffee = new Color32(111, 78, 55, 255);
        public static Color32 Bronze = new Color32(205, 127, 50, 255);

        // Extra Neons / Vibrant
        public static Color32 NeonGreen = new Color32(57, 255, 20, 255);
        public static Color32 NeonPink = new Color32(255, 16, 240, 255);
        public static Color32 NeonPurple = new Color32(199, 21, 133, 255);
        public static Color32 NeonBlue = new Color32(77, 77, 255, 255);
        public static Color32 NeonOrange = new Color32(255, 95, 31, 255);

        // Extra Neutral Tones
        public static Color32 Ivory = new Color32(255, 255, 240, 255);
        public static Color32 Silver = new Color32(192, 192, 192, 255);
        public static Color32 Charcoal = new Color32(54, 69, 79, 255);

        #endregion

        #region Extended Shade Variants

        // ===== RED SHADES =====
        public static Color32 UltraLightRed = new Color32(255, 210, 210, 255);
        public static Color32 LightRed = new Color32(255, 150, 150, 255);
        public static Color32 SoftRed = new Color32(255, 90, 90, 255);
        public static Color32 DeepRed = new Color32(200, 0, 0, 255);
        public static Color32 DarkRed2 = new Color32(140, 0, 0, 255);
        public static Color32 UltraDarkRed = new Color32(90, 0, 0, 255);

        // ===== GREEN SHADES =====
        public static Color32 UltraLightGreen = new Color32(210, 255, 210, 255);
        public static Color32 LightGreen = new Color32(150, 255, 150, 255);
        public static Color32 SoftGreen = new Color32(90, 220, 90, 255);
        public static Color32 DeepGreen = new Color32(0, 180, 0, 255);
        public static Color32 DarkGreen2 = new Color32(0, 120, 0, 255);
        public static Color32 UltraDarkGreen = new Color32(0, 70, 0, 255);

        // ===== BLUE SHADES =====
        public static Color32 UltraLightBlue = new Color32(210, 210, 255, 255);
        public static Color32 LightBlue2 = new Color32(150, 150, 255, 255);
        public static Color32 SoftBlue = new Color32(90, 90, 255, 255);
        public static Color32 DeepBlue = new Color32(0, 0, 200, 255);
        public static Color32 DarkBlue2 = new Color32(0, 0, 140, 255);
        public static Color32 UltraDarkBlue = new Color32(0, 0, 90, 255);

        // ===== YELLOW SHADES =====
        public static Color32 UltraLightYellow = new Color32(255, 255, 210, 255);
        public static Color32 LightYellow2 = new Color32(255, 255, 150, 255);
        public static Color32 SoftYellow = new Color32(240, 240, 90, 255);
        public static Color32 DeepYellow = new Color32(200, 200, 0, 255);
        public static Color32 DarkYellow = new Color32(150, 150, 0, 255);
        public static Color32 UltraDarkYellow = new Color32(100, 100, 0, 255);

        // ===== ORANGE SHADES =====
        public static Color32 UltraLightOrange = new Color32(255, 220, 190, 255);
        public static Color32 LightOrange = new Color32(255, 190, 120, 255);
        public static Color32 SoftOrange = new Color32(255, 150, 60, 255);
        public static Color32 DeepOrange = new Color32(210, 100, 0, 255);
        public static Color32 DarkOrange2 = new Color32(150, 70, 0, 255);
        public static Color32 UltraDarkOrange = new Color32(90, 40, 0, 255);

        // ===== PURPLE SHADES =====
        public static Color32 UltraLightPurple = new Color32(230, 200, 255, 255);
        public static Color32 LightPurple = new Color32(200, 150, 255, 255);
        public static Color32 SoftPurple = new Color32(170, 90, 255, 255);
        public static Color32 DeepPurple = new Color32(120, 0, 200, 255);
        public static Color32 DarkPurple = new Color32(80, 0, 140, 255);
        public static Color32 UltraDarkPurple = new Color32(50, 0, 90, 255);

        // ===== PINK SHADES =====
        public static Color32 UltraLightPink = new Color32(255, 220, 230, 255);
        public static Color32 LightPink2 = new Color32(255, 180, 200, 255);
        public static Color32 SoftPink = new Color32(255, 120, 170, 255);
        public static Color32 DeepPink2 = new Color32(210, 50, 120, 255);
        public static Color32 DarkPink = new Color32(150, 20, 80, 255);
        public static Color32 UltraDarkPink = new Color32(90, 0, 50, 255);

        // ===== BROWN SHADES =====
        public static Color32 UltraLightBrown = new Color32(210, 180, 150, 255);
        public static Color32 LightBrown = new Color32(180, 140, 100, 255);
        public static Color32 SoftBrown = new Color32(150, 110, 70, 255);
        public static Color32 DeepBrown = new Color32(110, 70, 40, 255);
        public static Color32 DarkBrown = new Color32(80, 50, 25, 255);
        public static Color32 UltraDarkBrown = new Color32(50, 30, 15, 255);

        // ===== GREY SHADES =====
        public static Color32 UltraLightGrey = new Color32(230, 230, 230, 255);
        public static Color32 LightGrey2 = new Color32(200, 200, 200, 255);
        public static Color32 SoftGrey = new Color32(160, 160, 160, 255);
        public static Color32 DeepGrey = new Color32(110, 110, 110, 255);
        public static Color32 DarkGrey2 = new Color32(70, 70, 70, 255);
        public static Color32 UltraDarkGrey = new Color32(30, 30, 30, 255);
        public static Color32 UltraDarkGrey2 = new Color32(10, 10, 10, 255);

        #endregion

        #region Pastel Shades

        public static Color32 PastelRed = new Color32(255, 179, 186, 255);
        public static Color32 PastelGreen = new Color32(186, 255, 201, 255);
        public static Color32 PastelBlue = new Color32(186, 225, 255, 255);
        public static Color32 PastelYellow = new Color32(255, 255, 186, 255);
        public static Color32 PastelPurple = new Color32(216, 191, 216, 255);
        public static Color32 PastelPink = new Color32(255, 209, 220, 255);
        public static Color32 PastelOrange = new Color32(255, 204, 153, 255);
        public static Color32 PastelBrown = new Color32(210, 180, 140, 255);

        #endregion

        #region Neon Extreme Shades

        public static Color32 HyperNeonRed = new Color32(255, 20, 20, 255);
        public static Color32 HyperNeonGreen = new Color32(0, 255, 60, 255);
        public static Color32 HyperNeonBlue = new Color32(0, 140, 255, 255);
        public static Color32 HyperNeonPurple = new Color32(200, 0, 255, 255);
        public static Color32 HyperNeonPink = new Color32(255, 0, 180, 255);
        public static Color32 HyperNeonOrange = new Color32(255, 80, 0, 255);
        public static Color32 HyperNeonYellow = new Color32(255, 255, 40, 255);

        #endregion

        #region Deep Rich Shades

        public static Color32 DeepCrimson = new Color32(120, 0, 20, 255);
        public static Color32 DeepEmerald = new Color32(0, 90, 40, 255);
        public static Color32 DeepOcean = new Color32(0, 40, 90, 255);
        public static Color32 DeepRoyalPurple = new Color32(60, 0, 110, 255);
        public static Color32 DeepRose = new Color32(130, 0, 60, 255);
        public static Color32 DeepAmber = new Color32(140, 80, 0, 255);

        #endregion

        #region Ice / Cool Shades

        public static Color32 IceBlue = new Color32(180, 240, 255, 255);
        public static Color32 IceCyan = new Color32(170, 255, 255, 255);
        public static Color32 IcePurple = new Color32(210, 200, 255, 255);
        public static Color32 FrostGreen = new Color32(200, 255, 240, 255);
        public static Color32 ArcticWhite = new Color32(240, 250, 255, 255);

        #endregion

        #region Warm Glow Shades

        public static Color32 WarmRed = new Color32(255, 90, 60, 255);
        public static Color32 WarmOrange = new Color32(255, 140, 60, 255);
        public static Color32 WarmYellow = new Color32(255, 220, 120, 255);
        public static Color32 WarmPink = new Color32(255, 140, 170, 255);
        public static Color32 WarmPurple = new Color32(200, 120, 255, 255);

        #endregion

        #region Teal Shades

        public static Color32 UltraLightTeal = new Color32(210, 255, 245, 255);
        public static Color32 LightTeal = new Color32(150, 240, 220, 255);
        public static Color32 SoftTeal = new Color32(90, 210, 190, 255);
        public static Color32 TealPlus = new Color32(0, 170, 150, 255);
        public static Color32 DeepTeal = new Color32(0, 130, 115, 255);
        public static Color32 DarkTeal = new Color32(0, 95, 85, 255);
        public static Color32 UltraDarkTeal = new Color32(0, 60, 55, 255);

        #endregion

        #region Cyan Shades

        public static Color32 UltraLightCyan = new Color32(220, 255, 255, 255);
        public static Color32 LightCyan2 = new Color32(160, 255, 255, 255);
        public static Color32 SoftCyan = new Color32(90, 235, 235, 255);
        public static Color32 CyanPlus = new Color32(0, 210, 210, 255);
        public static Color32 DeepCyan = new Color32(0, 160, 160, 255);
        public static Color32 DarkCyan = new Color32(0, 110, 110, 255);
        public static Color32 UltraDarkCyan = new Color32(0, 70, 70, 255);

        #endregion

        #region Neon Teal / Cyan

        public static Color32 NeonTeal = new Color32(0, 255, 200, 255);
        public static Color32 NeonTealBright = new Color32(40, 255, 220, 255);
        public static Color32 NeonCyan = new Color32(0, 255, 255, 255);
        public static Color32 NeonCyanGlow = new Color32(80, 255, 255, 255);
        public static Color32 ElectricTeal = new Color32(0, 255, 180, 255);
        public static Color32 PlasmaCyan = new Color32(0, 220, 255, 255);

        #endregion

        #region Pastel / Ice Teal Cyan

        public static Color32 PastelTeal = new Color32(180, 240, 230, 255);
        public static Color32 PastelCyan = new Color32(190, 255, 255, 255);
        public static Color32 IceTeal = new Color32(200, 255, 245, 255);
        public static Color32 IceCyan2 = new Color32(210, 255, 255, 255);
        public static Color32 FrostTeal = new Color32(170, 230, 225, 255);

        #endregion

        #region Deep Ocean Teal Cyan

        public static Color32 AbyssTeal = new Color32(0, 80, 90, 255);
        public static Color32 OceanTeal = new Color32(0, 110, 120, 255);
        public static Color32 DeepSeaCyan = new Color32(0, 95, 115, 255);
        public static Color32 TrenchTeal = new Color32(0, 60, 75, 255);
        public static Color32 MidnightCyan = new Color32(0, 50, 65, 255);

        #endregion

        #region Exotic Jewel Teal / Cyan

        public static Color32 TurquoiseBright = new Color32(64, 224, 208, 255);
        public static Color32 TurquoiseDeep = new Color32(0, 150, 140, 255);
        public static Color32 CaribbeanTeal = new Color32(0, 190, 170, 255);
        public static Color32 LagoonCyan = new Color32(0, 200, 210, 255);
        public static Color32 MysticTeal = new Color32(30, 170, 160, 255);

        #endregion

        #region Cyber / Sci-Fi Teal Cyan

        public static Color32 CyberTeal = new Color32(0, 255, 170, 255);
        public static Color32 CyberCyan = new Color32(0, 240, 255, 255);
        public static Color32 DigitalTeal = new Color32(0, 210, 180, 255);
        public static Color32 HologramCyan = new Color32(90, 255, 255, 255);
        public static Color32 LaserTeal = new Color32(0, 255, 150, 255);

        #endregion

        #region Extra

        // ===== Vibrant / Glow =====
        public static Color ElectricLime = new Color(204f / 255f, 1f, 0f, 1f);
        public static Color HotMagenta = new Color(1f, 0f, 200f / 255f, 1f);
        public static Color SolarOrange = new Color(1f, 130f / 255f, 30f / 255f, 1f);
        public static Color NeonTurquoise = new Color(0f, 1f, 210f / 255f, 1f);
        public static Color RadiantPink = new Color(1f, 105f / 255f, 230f / 255f, 1f);
        public static Color ShockingBlue = new Color(0f, 180f / 255f, 1f, 1f);

        // ===== Metallic / Rich =====
        public static Color Copper = new Color(184f / 255f, 115f / 255f, 51f / 255f, 1f);
        public static Color BronzeMetal = new Color(205f / 255f, 127f / 255f, 50f / 255f, 1f);
        public static Color RoseGold = new Color(1f, 183f / 255f, 178f / 255f, 1f);
        public static Color Platinum = new Color(229f / 255f, 228f / 255f, 226f / 255f, 1f);
        public static Color Graphite = new Color(54f / 255f, 69f / 255f, 79f / 255f, 1f);

        // ===== Mystic / Fantasy Shades =====
        public static Color MysticPurple = new Color(155f / 255f, 89f / 255f, 182f / 255f, 1f);
        public static Color EnchantedBlue = new Color(52f / 255f, 152f / 255f, 219f / 255f, 1f);
        public static Color FairyPink = new Color(1f, 182f / 255f, 193f / 255f, 1f);
        public static Color DragonGreen = new Color(0f, 128f / 255f, 102f / 255f, 1f);
        public static Color ShadowBlack = new Color(20f / 255f, 20f / 255f, 20f / 255f, 1f);

        // ===== Pastel Fun =====
        public static Color PastelMint = new Color(189f / 255f, 252f / 255f, 201f / 255f, 1f);
        public static Color PastelPeach = new Color(1f, 218f / 255f, 185f / 255f, 1f);
        public static Color PastelLavender = new Color(230f / 255f, 210f / 255f, 250f / 255f, 1f);
        public static Color PastelSky = new Color(175f / 255f, 238f / 255f, 238f / 255f, 1f);
        public static Color PastelRose = new Color(1f, 200f / 255f, 220f / 255f, 1f);

        // ===== Neon Extreme Additions =====
        public static Color NeonLime = new Color(150f / 255f, 1f, 0f, 1f);
        public static Color NeonViolet = new Color(200f / 255f, 0f, 1f, 1f);
        public static Color NeonCoral = new Color(1f, 64f / 255f, 64f / 255f, 1f);
        public static Color NeonAmber = new Color(1f, 191f / 255f, 0f, 1f);

        #endregion
    }
}