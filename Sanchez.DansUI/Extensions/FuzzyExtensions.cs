using System;
using System.Linq;

namespace Sanchez.DansUI.Extensions
{
    public static class FuzzyExtensions
    {
        public static int Min(params int[] vals)
        {
            if (vals.Length == 0) return 0;
            if (vals.Length == 1) return vals[0];

            var minValue = vals[0];
            for (var i = 1; i < vals.Length; i++)
            {
                if (vals[i] < minValue)
                    minValue = vals[i];
            }

            return minValue;
        }

        public static int LevenshteinDistance(this string a, string b)
        {
            if (a.Length == 0)
                return b.Length;
            if (b.Length == 0)
                return a.Length;

            var d = new int[a.Length + 1, b.Length + 1];

            for (var i = 0; i <= a.Length; i++)
                d[i, 0] = i;
            for (var i = 0; i <= b.Length; i++)
                d[0, i] = i;

            for (var i = 1; i <= a.Length; i++)
            {
                for (var j = 1; j <= b.Length; j++)
                {
                    var cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                    d[i, j] = Min(
                        d[i - 1, j] + 1,
                        d[i, j - 1] + 1,
                        d[i - 1, j - 1] + cost
                    );
                }
            }

            return d[a.Length, b.Length];
        }
    }
}
