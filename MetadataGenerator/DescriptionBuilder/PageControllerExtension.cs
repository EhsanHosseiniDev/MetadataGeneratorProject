using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MetadataGenerator.Definitions;
using MetadataGenerator.DescriptionBuilder.Attributes;

namespace MetadataGenerator.DescriptionBuilder;

public static class PageControllerExtension
{
    public static List<Type> GetPageControllers(this Assembly[] assemblies)
    {
        var pageControllerType = typeof(IPageController);
        var pageControllers = new List<Type>();
        
        foreach (var assembly in assemblies)
        {
            try
            {
                var types = assembly.GetTypes();
                pageControllers.AddRange(types
                    .Where(type => type is { IsClass: true, IsAbstract: false } && IsSubclassOfRawGeneric(pageControllerType, type)));
            }
            catch (ReflectionTypeLoadException)
            {
            }
        }

        return pageControllers;
    }

    private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
    {
        while (toCheck != null && toCheck != typeof(object))
        {
            var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
            if (generic == cur)
            {
                return true;
            }
            toCheck = toCheck.BaseType;
        }
        return false;
    }

    public static string GetControllerName(this Type controllerType)
        => controllerType.Name.Replace("Controller", "");

    public static ConfigControllerAttribute? GetControllerConfig(this Type controllerType) =>
        controllerType.GetAttribute<ConfigControllerAttribute>();

    public static bool ControllerShowInMenu(this Type controllerType) =>
        controllerType.GetControllerConfig()?.IsShowInMenu ?? false;

    public static int ControllerMenuOrder(this Type controllerType) =>
        controllerType.GetControllerConfig()?.MenuOrder ?? 0;

    public static string ControllerIcon(this Type controllerType) =>
        controllerType.GetControllerConfig()?.Icon ?? "home";

    public static string? GetControllerDisplayName(this Type controllerType) =>
        controllerType.GetAttribute<DisplayAttribute>()?.Name ?? null;

    public static Type? GetMasterModelType(this Type controllerType)
    {
        var result = controllerType.BaseType!.GetGenericArguments()
            .FirstOrDefault(t => t.IsSubclassOf(typeof(MasterModel)) && !t.IsAbstract);
        if (result != null) return result;

        var interfaceType = controllerType.GetInterfaces()
            .FirstOrDefault(i => i.IsAssignableFrom(typeof(PageControllerBase<,,,>)));

        var genericArguments = interfaceType?.GetGenericArguments();
        return genericArguments?[1];
    }
}