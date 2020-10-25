using Sanchez.DansUI.Extensions;
using System.Collections.Generic;
using System.Drawing;
using Xunit;

namespace Sanchez.DansUI.Tests.Extensions
{
    public class ColorExtensionsTests
    {
        public static IEnumerable<object[]> GetBackgroundForegroundPairs()
        {
            yield return new object[] { Color.White, Color.Black };
        }

        [Theory]
        [MemberData(nameof(GetBackgroundForegroundPairs))]
        public void TextColoring(Color background, Color expected)
        {
            var actual = background.GetTextColoring();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> GetColorDarknessPairs()
        {
            yield return new object[] { Color.White, false };
            yield return new object[] { Color.WhiteSmoke, false };
            yield return new object[] { Color.Black, true };
            yield return new object[] { Color.DarkSlateBlue, true };
            yield return new object[] { Color.BlueViolet, true };
            yield return new object[] { Color.Chartreuse, false };
        }

        [Theory]
        [MemberData(nameof(GetColorDarknessPairs))]
        public void DarknessTesting(Color color, bool expected)
        {
            var actual = color.IsDarkColor();
            Assert.Equal(expected, actual);
        }
    }
}
