using System;
using System.Collections.Generic;
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

        public static int FuzzyMatch(this string input, string pattern)
        {
            if (input.Length == 0 || pattern.Length == 0) return 0;

            Dictionary<char, IList<(bool IsUpper, int Position)>> inputDict = new();
            int i = 0;
            foreach (var c in input)
            {
                var lowerC = char.ToLower(c);
                if (!inputDict.ContainsKey(lowerC))
                    inputDict.Add(lowerC, new List<(bool IsUpper, int Position)>());
                var isUpper = lowerC != c;
                inputDict[lowerC].Add((isUpper, i++));
            }

            var patternProcessed = pattern.Select(x => (char.ToLower(x), char.IsUpper(x)));
            var patternInput = new LinkedList<(char C, bool IsUpper)>(patternProcessed);

            return FuzzyMatchInput(inputDict, patternInput.First, -1);
        }

        private static int FuzzyMatchInput(
            Dictionary<char, IList<(bool IsUpper, int Position)>> inputDict,
            LinkedListNode<(char C, bool IsUpper)> patternPosition,
            int inputPosition)
        {
            if (patternPosition == null) return 0;

            if (inputDict.TryGetValue(patternPosition.Value.C, out var entries))
            {
                var filteredEntries = entries
                    .Where(x => x.Position > inputPosition)
                    .Select(x =>
                    {
                        var matchBonus = 1; // A place to store some conditional match bonuses
                        if (x.IsUpper && patternPosition.Value.IsUpper)
                            matchBonus += 1; // Both upper case bonus
                        if (inputPosition == -1 && x.Position == 0)
                            matchBonus += 3; // Both beginning characters were matched
                        else if (x.Position == (inputPosition + 1))
                            matchBonus += 5; // Sequential match bonus
                        var futureResult = FuzzyMatchInput(inputDict, patternPosition.Next, x.Position);
                        return futureResult + matchBonus;
                    });
                if (filteredEntries.Any())
                    return filteredEntries.Max();
            }

            // There were no available matches left, go to the next character in the pattern and apply a penalty
            var skippedScore = FuzzyMatchInput(inputDict, patternPosition.Next, inputPosition);
            return skippedScore - 1;
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
