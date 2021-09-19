using Sanchez.DansUI.Extensions;
using Sanchez.DansUI.Theming;

using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Sanchez.DansUI.Configuration
{
    public class ThemeLoader
    {
        protected UITheme _theme;

        public ThemeLoader(UITheme theme)
        {
            _theme = theme;
        }

        public string GenerateStyleContent()
        {
            var styles = GenerateVariables();

            var lines = new List<string>()
            {
                ":root {"
            };

            lines.AddRange(styles.Select(x => $"--{x.Item1}: {x.Item2.ToHexString()};"));

            lines.Add("}");

            return string.Join('\n', lines);
        }

        public ICollection<(string, Color)> GenerateVariables()
        {
            var totalCollection = new List<(string, Color)>()
            {
                ("primary", _theme.Primary),
                ("primary-background", _theme.Primary),
                ("primary-foreground", _theme.Primary.GetTextColoring()),

                ("secondary", _theme.Secondary),
                ("secondary-background", _theme.Secondary),
                ("secondary-foreground", _theme.Secondary.GetTextColoring()),

                ("nav-background", _theme.Navigation),
                ("nav-foreground", _theme.Navigation.GetTextColoring()),

                ("paper-background", _theme.Paper),
                ("paper-foreground", _theme.Paper.GetTextColoring()),

                ("success", _theme.Success)
            };

            totalCollection.AddRange(GenerateStandardStyle(_theme.Paper, "base"));
            totalCollection.AddRange(GenerateStandardStyle(_theme.Primary, "primary"));
            totalCollection.AddRange(GenerateStandardStyle(_theme.Secondary, "secondary"));

            totalCollection.AddRange(GenerateLinkStyle(_theme.Primary, "primary"));
            totalCollection.AddRange(GenerateLinkStyle(_theme.Secondary, "secondary"));

            totalCollection.AddRange(GenerateButtonStyle(_theme.Paper, "base"));
            totalCollection.AddRange(GenerateButtonStyle(_theme.Primary, "primary"));
            totalCollection.AddRange(GenerateButtonStyle(_theme.Secondary, "secondary"));

            totalCollection.AddRange(GenerateFieldStyle(_theme.Paper, _theme.Primary, _theme.Error, "primary"));

            totalCollection.AddRange(GenerateCardStyle(_theme.Paper, _theme.Secondary, "base"));

            totalCollection.AddRange(GenerateNotificationStyle(_theme.Paper, "base"));
            totalCollection.AddRange(GenerateNotificationStyle(_theme.Success, "success"));
            totalCollection.AddRange(GenerateNotificationStyle(_theme.Warning, "warning"));
            totalCollection.AddRange(GenerateNotificationStyle(_theme.Error, "error"));

            totalCollection.AddRange(GenerateModalStyle(_theme.Paper));

            totalCollection.AddRange(GenerateTableStyle(_theme.Primary));

            totalCollection.AddRange(GenerateDropDownStyle(_theme.Paper, _theme.Primary));

            return totalCollection;
        }

        protected ICollection<(string, Color)> GenerateStandardStyle(Color baseColor, string styleName)
        {
            var background = baseColor.RelativeBrightness(0.2f);
            return new List<(string, Color)>()
            {
                ($"base-{styleName}-background", background),
                ($"base-{styleName}-outline", background.RelativeBrightness(0.2f)),
                ($"base-{styleName}-color", background.GetTextColoring())
            };
        }

        protected ICollection<(string, Color)> GenerateLinkStyle(Color baseColor, string styleName)
        {
            return new List<(string, Color)>()
            {
                ($"link-{styleName}-color", baseColor),
                ($"link-{styleName}-hover", baseColor.RelativeBrightness(0.6f))
            };
        }

        protected ICollection<(string, Color)> GenerateButtonStyle(Color baseColor, string styleName)
        {
            var background = baseColor.RelativeBrightness(0.2f);
            return new List<(string, Color)>()
            {
                ($"button-{styleName}-background", background),
                ($"button-{styleName}-disabled", background.SetOpacity(0.3f)),
                ($"button-{styleName}-color-disabled", background.GetTextColoring().SetOpacity(0.3f)),
                ($"button-{styleName}-outline", background.RelativeBrightness(0.2f)),
                ($"button-{styleName}-color", background.GetTextColoring())
            };
        }

        protected ICollection<(string, Color)> GenerateFieldStyle(Color baseColor, Color primaryColor, Color errorColor, string styleName)
        {
            var background = baseColor.RelativeBrightness(0.1f);
            return new List<(string, Color)>()
            {
                ($"field-{styleName}-background", background),
                ($"field-{styleName}-outline", background.RelativeBrightness(0.2f)),
                ($"field-{styleName}-color", background.GetTextColoring()),
                ($"field-{styleName}-fixcolor", background.GetTextColoring().RelativeBrightness(0.4f)),
                ($"field-{styleName}-error-color", errorColor.RelativeBrightness(0.2f)),
                ($"field-{styleName}-enabled", primaryColor.RelativeBrightness(0.2f))
            };
        }

        protected ICollection<(string, Color)> GenerateCardStyle(Color baseColor, Color primaryColor, string styleName)
        {
            var background = baseColor.RelativeBrightness(0.1f);
            return new List<(string, Color)>()
            {
                ($"card-{styleName}-background", background),
                ($"card-{styleName}-footer", background.RelativeBrightness(0.1f)),
                ($"card-{styleName}-outline", background.RelativeBrightness(0.2f)),
                ($"card-{styleName}-shadow", background.RelativeBrightness(0.1f)),
                ($"card-{styleName}-color", background.GetTextColoring())
            };
        }

        protected ICollection<(string, Color)> GenerateNotificationStyle(Color color, string styleName)
        {
            return new List<(string, Color)>() {
                ($"notification-{styleName}-border-color", color.RelativeBrightness(0.2f)),
                ($"notification-{styleName}-color", color)
            };
        }

        protected ICollection<(string, Color)> GenerateModalStyle(Color baseColor)
        {
            return new List<(string, Color)>() {
                ($"modal-footer-background-color", baseColor.RelativeBrightness(0.1f))
            };
        }

        protected ICollection<(string, Color)> GenerateTableStyle(Color baseColor)
        {
            return new List<(string, Color)>()
            {
                ("table-header-background", baseColor.RelativeBrightness(0.1f).SetOpacity(0.3f)),
                ("table-row-hover-background", baseColor.RelativeBrightness(0.1f).SetOpacity(0.2f)),
                ("table-row-alt-background", baseColor.RelativeBrightness(0.1f).SetOpacity(0.1f))
            };
        }

        protected ICollection<(string, Color)> GenerateDropDownStyle(Color baseColor, Color primaryColor)
        {
            return new List<(string, Color)>() {
                ("dropdown-outline", primaryColor),
                ("dropdown-background", baseColor.RelativeBrightness(0.15f)),
                ("dropdown-item-hover-background", baseColor.RelativeBrightness(0.2f))
            };
        }
    }
}
