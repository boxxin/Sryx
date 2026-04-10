
namespace StupidTemplate.Mods.Settings
{
    public class Movement
    {
        public static float maxjumpsped = 8f;
        public static float jumpmulti = 1.25f;
        public static int multiIndex = 5;
        public static int speedIndex = 5;
        public static void ChangeJumpMulti()
        {
            multiIndex++;
            if (multiIndex > 5)
            {
                multiIndex = 0;
            }
            switch (multiIndex)
            {
                case 0: jumpmulti = 0.1f; break;
                case 1: jumpmulti = 1f; break;
                case 2: jumpmulti = 3f; break;
                case 3: jumpmulti = 10f; break;
                case 4: jumpmulti = 15f; break;
                case 5: jumpmulti = 1.25f; break;
            }
        }
        public static void ChangeMaxJumpSpeed()
        {
            speedIndex++;
            if (speedIndex > 5)
            {
                speedIndex = 0;
            }
            switch (speedIndex)
            {
                case 0: maxjumpsped = 9f; break;
                case 1: maxjumpsped = 9.5f; break;
                case 2: maxjumpsped = 10f; break;
                case 3: maxjumpsped = 5f; break;
                case 4: maxjumpsped = 20f; break;
                case 5: maxjumpsped = 8f; break;
            }
        }
    }
}
