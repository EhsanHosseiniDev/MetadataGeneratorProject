using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MetadataGenerator.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetadataGenerator.Extensions;

public static class ControllerExtension
{
    public static List<Type> GetControllers(this Assembly[] assemblies)
    {
        var controllers = new List<Type>();

        foreach (var assembly in assemblies)
            controllers.AddRange(assembly
                .GetTypes()
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && !t.IsAbstract));

        return controllers;
    }

    public static IEnumerable<ControllerModel> GetControllerModels(this IEnumerable<Type> types)
        => types.Select(type => type.GetControllerModel());

    public static ControllerModel GetControllerModel(this Type type)
    {
        return new()
        {
            ControllerUri = type.GetControllerUri(),
            Name = type.GetTypeName(),
            Actions = type.GetActionModels()
        };
    }

    public static List<ActionModel> GetActionModels(this Type controllerType)
    {
        var actionModels = new List<ActionModel>();

        var actionMethods = controllerType.GetActionMethods();

        foreach (var method in actionMethods)
        {
            actionModels.Add(new()
            {
                ActionUri = method.GetActionUri(),
                RequestType = method.GetRequestType().ToString(),
                Name = method.GetActionMethodName(),
                RequestParameterStatus = method.GetRequestParameterStatus().ToString(),
                RequestProperties = method.ConvertFirstParameter(),
                ResponseProperties = method.ConvertReturnParameter()
            });
        }

        return actionModels;
    }

    public static string GetControllerUri(this Type controllerType)
        => controllerType.Name.Replace("Controller", "");
}