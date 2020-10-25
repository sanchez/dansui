using System.Diagnostics.CodeAnalysis;

namespace Sanchez.DansUI.Components.Form
{
    public class TextField : BaseField<string>
    {
        protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out string result, [NotNullWhen(false)] out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }
    }
}
