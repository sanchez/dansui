using System.Collections.Generic;

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

        public static int FuzzyMatch(this string input, string pattern)
        {
            // Current match values are
            const int MATCHED_CHARACTER = 1; // when a input and pattern characters match, case insensitive
            const int MATCHED_UPPER_CHARACTER = 1; // when a input and pattern character match and are both upper case
            const int MATCHED_BEGINNING_BONUS = 3; // when the first input character matches the first pattern character
            const int MATCHED_SEQUENTIAL_BONUS = 5; // when the next character in both sequences matches
            const int UNMATCHED_PATTERN_CHARACTER = -1; // when a character on the pattern is not matched with the input

            Dictionary<char, (bool IsUpper, int Position)> inputDict;

            // TODO: make this more efficient and do something with the ability to track upper/lower case
            //var inputDict = input.Select((x, i) => (Input: x, Position: i)).GroupBy(x => x.Input).ToDictionary(x => x.Key, x => x.Select(y => y.Position));

            return 0;
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
