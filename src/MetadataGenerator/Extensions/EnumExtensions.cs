using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MetadataGenerator.Extensions;

public static class EnumExtensions
{
    public static string GetStringValue(this Enum flags)
    {
        if (flags.IsFlags())
        {
            var text = GetEnumFlagsText(flags);
            if (!string.IsNullOrWhiteSpace(text))
            {
                return text;
            }
        }
        return GetEnumValueText(flags);
    }

    public static bool IsFlags(this Enum flags)
    {
        return flags.GetType().GetTypeInfo().GetCustomAttributes(true).OfType<FlagsAttribute>().Any();
    }

    private static string GetEnumFlagsText(Enum flags)
    {
        const char leftToRightSeparator = ',';
        const char rightToRightSeparator = '،';

        var sb = new StringBuilder();
        var items = Enum.GetValues(flags.GetType());
        foreach (var value in items)
        {
            if (!flags.HasFlag((Enum)value) || Convert.ToInt64((Enum)value) == 0) continue;

            var text = GetEnumValueText((Enum)value);
            sb.Append(text).Append(leftToRightSeparator).Append(" ");
        }

        return sb.ToString().Trim().TrimEnd(leftToRightSeparator).TrimEnd(rightToRightSeparator);
    }

    private static string GetEnumValueText(Enum value)
    {
        var text = value.ToString();
        var info = value.GetType().GetField(text);

        var display = info?.GetCustomAttributes(true).OfType<DisplayAttribute>().FirstOrDefault();
        if (display != null)
        {
            return display.Name ?? string.Empty;
        }

        var description = info?.GetCustomAttributes(true).OfType<DescriptionAttribute>().FirstOrDefault();
        return description != null ? description.Description : text;
    }

    public static string[] ToEnumToDisplayNames(this Type value)
    {
        if (!value.IsEnum)
            throw new NotSupportedException("type is not enum");
        var result = new List<string>();
        var names = Enum.GetNames(value);
        foreach (var name in names)
        {
            var attribute = value.GetField(name)?
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();
            if (attribute == null)
                result.Add(value.ToString());

            result.Add(attribute?.Name ?? string.Empty);
        }

        return result.ToArray();
    }
}