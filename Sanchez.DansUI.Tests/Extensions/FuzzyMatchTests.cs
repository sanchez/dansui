using Sanchez.DansUI.Extensions;

using System.Collections.Generic;

using Xunit;

namespace Sanchez.DansUI.Tests.Extensions
{
    public class FuzzyMatchTests
    {
        public static IEnumerable<object[]> FuzzyMatchTestInputs()
        {
            yield return new object[] { "hello", "he", 10 };
            yield return new object[] { "hello", "w", -1 };
            yield return new object[] { "hello", "lo", 7 };
            yield return new object[] { "left", "right", -3 };
            yield return new object[] { "Goulburn", "Gburn", 24 };

            // match start: (3 + 2 + 3 + 10) = 18
            // match end: (3 + 3 + 10) = 10
            yield return new object[] { "lLLL", "LLL", 18 };

            yield return new object[] { "alLLL", "LLL", 16 };
            yield return new object[] { "Apple", "A", 5 };
            yield return new object[] { "Apple", "Ae", 6 };
            yield return new object[] { "water", "ae", 2 };
            yield return new object[] { "water", "at", 7 };
        }

        [Theory]
        [MemberData(nameof(FuzzyMatchTestInputs))]
        public void FuzzyMatchTest(string input, string pattern, int expected)
        {
            var actual = input.FuzzyMatch(pattern);
            Assert.Equal(expected, actual);
        }
    }
}
