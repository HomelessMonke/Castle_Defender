using UnityEngine;

namespace Game
{
    public static class GameSpeed
    {
        public static float TimeMultiplier = 1f;

        static bool boosted;

        public static void ToggleSpeed()
        {
            boosted = !boosted;
            Time.timeScale = boosted ? 2f : 1f;
            TimeMultiplier = Time.timeScale;
        }

        public static void ResetSpeed()
        {
            if (!boosted) return;
            boosted = !boosted;
            Time.timeScale = 1f;
            TimeMultiplier = Time.timeScale;
        }

        public static void TogglePause(bool paused)
        {
            Time.timeScale = paused ? 0f : TimeMultiplier;
        }
    }
}