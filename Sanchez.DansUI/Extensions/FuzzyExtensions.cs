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

            var lowerA = a.ToLower();
            var lowerB = b.ToLower();

            var d = new int[lowerA.Length + 1, lowerB.Length + 1];

            for (var i = 0; i <= lowerA.Length; i++)
                d[i, 0] = i;
            for (var i = 0; i <= lowerB.Length; i++)
                d[0, i] = i;

            for (var i = 1; i <= lowerA.Length; i++)
            {
                for (var j = 1; j <= lowerB.Length; j++)
                {
                    var cost = (lowerA[i - 1] == lowerB[j - 1]) ? 0 : 1;
                    d[i, j] = Min(
                        d[i - 1, j] + 1,
                        d[i, j - 1] + 1,
                        d[i - 1, j - 1] + cost
                    );
                }
            }

            return d[lowerA.Length, lowerB.Length];
        }
    }
}
