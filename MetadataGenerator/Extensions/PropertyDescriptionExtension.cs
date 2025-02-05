using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Castle.Core.Internal;
using MetadataGenerator.Attributes;
using MetadataGenerator.Definitions;
using MetadataGenerator.Models;

namespace MetadataGenerator.Extensions;

public static class PropertyDescriptionExtension
{
    public static bool IsObject(this PropertyInfo masterModelProperty)
    {
        var readModelGenericArgs = masterModelProperty.PropertyType.GetGenericArguments();
        var isMasterModel = readModelGenericArgs.FirstOrDefault(t => t.IsSubclassOf(typeof(ICommand)) && !t.IsAbstract) != null;
        return masterModelProperty.PropertyType.IsSubclassOf(typeof(ICommand)) || isMasterModel;
    }

    public static bool HasAttribute<T>(this PropertyInfo masterModelProperty) where T : Attribute
        => masterModelProperty.GetAttribute<T>() != null;

    public static bool IsHide(this PropertyInfo masterModelProperty)
        => masterModelProperty.HasAttribute<HideAttribute>();

    public static PropertyType GetType(this PropertyInfo masterModelProperty, bool isobject)
        => masterModelProperty.PropertyType.GetConvertedType(isobject);

    public static string? GetDisplayName(this PropertyInfo masterModelProperty)
        => masterModelProperty.GetAttribute<DisplayAttribute>()?.Name;

    public static int GetOrder(this PropertyInfo masterModelProperty)
        => masterModelProperty.GetAttribute<OrderAttribute>()?.Value ?? -1;

    public static string[] GenerateDefaultValue(this Type propertyType)
        => propertyType.IsEnum ? propertyType.ToEnumToDisplayNames() : [];

    public static PropertyType GetConvertedType(this Type propertyType, bool isObject = false)
    {
        if (isObject || 
            propertyType == typeof(ICollection<>) || 
            propertyType == typeof(IEnumerable<>) || 
            propertyType == typeof(List<>) || propertyType == typeof(object))
            return PropertyType.Object;
        if (propertyType == typeof(string))
            return PropertyType.String;
        if (propertyType == typeof(decimal))
            return PropertyType.Decimal;
        if (propertyType == typeof(int) || propertyType == typeof(long))
            return PropertyType.Number;
        if (propertyType.IsEnum)
            return PropertyType.Enum;
        if (propertyType == typeof(DateTime))
            return PropertyType.DateTime;
        if (propertyType == typeof(bool))
            return PropertyType.Boolean;
        if (propertyType == typeof(Guid))
            return PropertyType.Guid;

        return PropertyType.String;
    }
}