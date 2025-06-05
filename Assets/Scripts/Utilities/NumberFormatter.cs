using System;

namespace Utilities
{
    public static class NumberFormatter
    {
        static string[] suffixes = { "", "k", "m" };

        public static string FormatNumber(int value)
        {
            if (value < 1000)
                return value.ToString();

            int suffixIndex = 0;
            double shortened = value;

            while (shortened >= 1000 && suffixIndex < suffixes.Length - 1)
            {
                shortened /= 1000.0;
                suffixIndex++;
            }

            if (shortened < 10)
                return shortened.ToString("0.#") + suffixes[suffixIndex];
            return Math.Round(shortened).ToString() + suffixes[suffixIndex];
        }
    }
}

