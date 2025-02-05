using System.Collections.Generic;
using System.Reflection;
using MetadataGenerator.Attributes;
using MetadataGenerator.Models;

namespace MetadataGenerator.Extensions;

public static class PropertyExtension
{
    public static List<PropertyModel> GenerateFromModel(this PropertyInfo[] masterModelPropertyInfos)
    {
        var propertyDescription = new List<PropertyModel>();

        foreach (var masterModelProperty in masterModelPropertyInfos)
        {

            var isobject = masterModelProperty.IsObject();
            var isCollection = masterModelProperty!.PropertyType.IsGenericType;
            var subPropertyDescriptions = (isobject, isCollection) switch
            {
                (true, true) => masterModelProperty.PropertyType.GetGenericArguments()[0].GetProperties().GenerateFromModel(),
                (true, false) => masterModelProperty.PropertyType.GetProperties().GenerateFromModel(),
                _ => null
            };

            propertyDescription.Add(subPropertyDescriptions != null
                ? GeneratePropertyDescription(masterModelProperty, isobject, subPropertyDescriptions)
                : GeneratePropertyDescription(masterModelProperty, isobject, new()));
        }
        return propertyDescription;
    }

    public static PropertyModel GeneratePropertyDescription(PropertyInfo masterModelProperty, bool isobject, List<PropertyModel> subDescription)
    {
        return new()
        {
            Hide = masterModelProperty.IsHide(),
            AllowQuery = masterModelProperty.HasAttribute<SearchableAttribute>(),
            Searchable = masterModelProperty.HasAttribute<SearchableAttribute>(),
            ColumnHide = masterModelProperty.HasAttribute<ColumnHideAbleAttribute>(),
            Filterable = masterModelProperty.HasAttribute<FilterableAttribute>(),
            IsId = masterModelProperty.HasAttribute<IsIdAttribute>(),
            PinAble = masterModelProperty.HasAttribute<PinAbleAttribute>(),
            Sortable = masterModelProperty.HasAttribute<SortableAttribute>(),
            Order = masterModelProperty.GetOrder(),
            Name = masterModelProperty.Name,
            Display = masterModelProperty.GetDisplayName() ?? masterModelProperty.Name,
            PropertyType = masterModelProperty.GetType(isobject).ToString(),
            PropertyDescriptions = subDescription,
            DefaultValues = masterModelProperty.PropertyType.GenerateDefaultValue()
        };
    }
}