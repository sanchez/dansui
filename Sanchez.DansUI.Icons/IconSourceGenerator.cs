using Microsoft.CodeAnalysis;

using Sanchez.DansUI.Icons.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Sanchez.DansUI.Icons
{
    [Generator]
    public class IconSourceGenerator : ISourceGenerator
    {
        protected IEnumerable<IconEntry> LoadIcons(string path)
        {
            var icons = new List<IconEntry>();

            try
            {
                var files = Directory.EnumerateFiles(path);


                foreach (var x in Directory.EnumerateFiles(path))
                {
                    if (Path.GetExtension(x) != ".svg") continue;

                    icons.Add(new IconEntry()
                    {
                        IconName = Path.GetFileNameWithoutExtension(x),
                        IconSource =
                            File.ReadAllText(x)
                                .Replace("\n", string.Empty)
                                .Replace("\r", string.Empty)
                                .Replace("\t", string.Empty)
                    });
                }
            }
            catch (Exception ex)
            {
            }

            return icons;
        }

        protected string GetName(IconEntry icon) => icon.IconName.ToUpper().Replace(' ', '_').Replace('-', '_');
        protected string GetCode(IconEntry icon) => icon.IconSource.Replace("\\", "\\\\").Replace("\"", "\\\"");

        protected void AppendEnum(StringBuilder sourceBuilder, Dictionary<string, IconEntry> icons)
        {
            sourceBuilder.AppendLine("public enum IconType {");

            var iconSet = icons.Select(x => GetName(x.Value)).Distinct().Prepend("UNKNOWN").ToList();
            sourceBuilder.AppendLine(string.Join(", ", iconSet));

            sourceBuilder.AppendLine("}");
        }

        protected void AppendConverter(StringBuilder sourceBuilder, Dictionary<string, IconEntry> icons)
        {
            sourceBuilder.AppendLine("public static class IconTypeHelper {");
            sourceBuilder.AppendLine("public static RenderFragment ToRenderFragment(this IconType type) {");
            sourceBuilder.AppendLine("return type switch {");

            foreach (var icon in icons)
            {
                sourceBuilder.AppendLine($"IconType.{GetName(icon.Value)} => (RenderTreeBuilder builder) => builder.AddMarkupContent(0, \"{GetCode(icon.Value)}\"),");
            }

            sourceBuilder.AppendLine("_ => throw new System.ArgumentException(\"Unsupported icon type\", nameof(type))");
            sourceBuilder.AppendLine("};");
            sourceBuilder.AppendLine("}");
            sourceBuilder.AppendLine("}");
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var iconItems =
                LoadIcons(@"D:\Documents\git\dansui\lucide\icons")
                .ToDictionary(x => x.IconName);

            var sourceBuilder = new StringBuilder();

            sourceBuilder.AppendLine("using Microsoft.AspNetCore.Components;");
            sourceBuilder.AppendLine("using Microsoft.AspNetCore.Components.Rendering;");

            sourceBuilder.AppendLine("namespace Sanchez.DansUI.Icons {");
            AppendEnum(sourceBuilder, iconItems);
            AppendConverter(sourceBuilder, iconItems);
            sourceBuilder.AppendLine("}");

            var test = sourceBuilder.ToString();

            context.AddSource("Sanchez.DansUI.Icons.IconPack.cs", sourceBuilder.ToString());
        }

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached)
            {
                //Debugger.Launch();
            }
#endif
        }
    }
}
