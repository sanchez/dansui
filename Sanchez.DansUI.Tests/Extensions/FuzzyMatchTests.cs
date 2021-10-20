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
