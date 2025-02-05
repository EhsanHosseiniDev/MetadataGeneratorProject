using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MetadataGenerator.Extensions;

public static class TypeExtension
{
    public static string GetTypeName(this Type controllerType)
    {
        var attribute = controllerType?
            .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

        return attribute?.Name ?? controllerType!.Name;
    }
}