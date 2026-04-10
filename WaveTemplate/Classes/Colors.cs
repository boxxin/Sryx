using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace StupidTemplate.Classes
{
    public class Colors
    {
        public static Color Blend(params Color[] colors)
        {
            if (colors == null || colors.Length == 0)
                throw new ArgumentException("At least one color is required.", "colors");

            Color color = Color.clear;
            foreach (Color color2 in colors)
            {
                color += color2;
            }
            return color / colors.Length;
        }
    }
}
