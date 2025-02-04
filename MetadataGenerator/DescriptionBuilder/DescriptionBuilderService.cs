using System.Collections.Generic;
using System.Reflection;
using MetadataGenerator.Attributes;
using MetadataGenerator.Models;
using Microsoft.Extensions.Localization;

namespace MetadataGenerator.DescriptionBuilder;

public class DescriptionBuilderService(IStringLocalizer _localizer) : IDescriptionBuilderService
{
    public ICollection<PageControllerModel> Generate(Assembly[] assembly, string? key = null)
    {
        var pageControllers = assembly.GetPageControllers();

        var result = new List<PageControllerModel>();

        foreach (var pageController in pageControllers)
        {
            var page = new PageControllerModel
            {
                Name = pageController.GetControllerName()
            };

            if (key != null && page.Name != key)
                continue;

            page.Display = _localizer[pageController.GetControllerDisplayName() ?? page.Name];
            page.ShowInMenu = pageController.ControllerShowInMenu();
            page.MenuOrder = pageController.ControllerMenuOrder();
            page.Icon = pageController.ControllerIcon();
            page.CanUploadExcel = true;

            var masterModel = pageController.GetMasterModelType();
            var propertyDescriptions = GenerateFromModel(masterModel!.GetProperties());
            page.PropertyDescriptions = propertyDescriptions;
            result.Add(page);
        }
        return result;
    }

    private List<PropertyDescription> GenerateFromModel(PropertyInfo[] masterModelPropertyInfos)
    {
        var propertyDescription = new List<PropertyDescription>();

        foreach (var masterModelProperty in masterModelPropertyInfos)
        {

            var isobject = masterModelProperty.IsObject();
            var isCollection = masterModelProperty!.PropertyType.IsGenericType;
            var subPropertyDescriptions = (isobject, isCollection) switch
            {
                (true, true) => GenerateFromModel(masterModelProperty.PropertyType.GetGenericArguments()[0].GetProperties()),
                (true, false) => GenerateFromModel(masterModelProperty.PropertyType.GetProperties()),
                _ => null
            };

            propertyDescription.Add(subPropertyDescriptions != null
                ? GeneratePropertyDescription(masterModelProperty, isobject, subPropertyDescriptions)
                : GeneratePropertyDescription(masterModelProperty, isobject, new()));
        }
        return propertyDescription;
    }

    private PropertyDescription GeneratePropertyDescription(PropertyInfo masterModelProperty, bool isobject, List<PropertyDescription> subDescription)
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
            Display = _localizer[masterModelProperty.GetDisplayName() ?? masterModelProperty.Name],
            PropertyType = masterModelProperty.GetType(isobject),
            PropertyDescriptions = subDescription,
            DefaultValues = masterModelProperty.PropertyType.GenerateDefaultValue()
        };
    }
}