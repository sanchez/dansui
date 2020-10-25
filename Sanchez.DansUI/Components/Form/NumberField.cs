using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Sanchez.DansUI.Components.Form
{
    public class NumberField<TValue> : BaseField<TValue>
    {
        protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string validationErrorMessage)
        {
            if (BindConverter.TryConvertTo<TValue>(value, CultureInfo.InvariantCulture, out result))
            {
                validationErrorMessage = null;
                return true;
            }
            else
            {
                validationErrorMessage = $"Failed to parse field: {FieldIdentifier.FieldName}";
                return false;
            }
        }

        protected override string FormatValueAsString(TValue? value)
        {
            return value switch
            {
                null => null,

                int x => BindConverter.FormatValue(x, CultureInfo.InvariantCulture),
                long x => BindConverter.FormatValue(x, CultureInfo.InvariantCulture),
                short x => BindConverter.FormatValue(x, CultureInfo.InvariantCulture),
                float x => BindConverter.FormatValue(x, CultureInfo.InvariantCulture),
                double x => BindConverter.FormatValue(x, CultureInfo.InvariantCulture),
                decimal x => BindConverter.FormatValue(x, CultureInfo.InvariantCulture),

                _ => throw new InvalidOperationException($"Unsupported type {value.GetType()}")
            };
        }
    }
}
