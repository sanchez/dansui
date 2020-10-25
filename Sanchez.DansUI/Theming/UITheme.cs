using System.Drawing;

namespace Sanchez.DansUI.Theming
{
    public class UITheme
    {
        public UITheme(Color primary, Color secondary, Color navigation, Color paper)
        {
            Primary = primary;
            Secondary = secondary;
            Navigation = navigation;
            Paper = paper;
        }

        public Color Primary { get; }
        public Color Secondary { get; }
        public Color Navigation { get; }
        public Color Paper { get; }

        public Color Warning { get; set; } = Color.Orange;
        public Color Error { get; set; } = Color.DarkRed;
        public Color Success { get; set; } = Color.LimeGreen;
    }
}
