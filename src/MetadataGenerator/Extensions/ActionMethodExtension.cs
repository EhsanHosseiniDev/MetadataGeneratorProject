using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MetadataGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace MetadataGenerator.Extensions;

public static class ActionMethodExtension
{
    public static IEnumerable<MethodInfo> GetActionMethods(this Type controllerType)
    {
        return controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public)
            .Where(m => m.GetCustomAttributes(typeof(HttpMethodAttribute), false).Any());
    }

    public static RequestType GetRequestType(this MethodInfo actionMethod)
    {
        if (actionMethod.GetCustomAttributes(typeof(HttpGetAttribute), false).Any())
            return RequestType.Get;

        if (actionMethod.GetCustomAttributes(typeof(HttpPostAttribute), false).Any())
            return RequestType.Post;

        if (actionMethod.GetCustomAttributes(typeof(HttpPutAttribute), false).Any())
            return RequestType.Update;

        if (actionMethod.GetCustomAttributes(typeof(HttpDeleteAttribute), false).Any())
            return RequestType.Delete;

        return RequestType.Post;
    }

    public static RequestParameterStatus? GetRequestParameterStatus(this MethodInfo actionMethod)
    {
        if (actionMethod.GetCustomAttributes(typeof(FromBodyAttribute), false).Any())
            return RequestParameterStatus.InBody;

        if (actionMethod.GetCustomAttributes(typeof(FromQueryAttribute), false).Any())
            return RequestParameterStatus.InQuery;

        if (actionMethod.GetCustomAttributes(typeof(FromFormAttribute), false).Any())
            return RequestParameterStatus.InForm;

        return null;
    }

    public static string GetActionMethodName(this MethodInfo actionMethod)
    {
        var attribute = actionMethod
            .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

        return attribute?.Name ?? actionMethod.GetParameterName();
    }

    public static string GetParameterName(this MethodInfo actionMethod)
        => actionMethod.GetParameters().FirstOrDefault()?.GetType().GetTypeName() ?? string.Empty;

    public static string GetActionUri(this MethodInfo method)
        => method.Name.Replace("Async", "");

    public static IEnumerable<PropertyModel> ConvertReturnParameter(this MethodInfo actionMethod)
    {
        if (actionMethod.ReturnType.IsGenericType && actionMethod.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
            return actionMethod.ReturnType.GetGenericArguments()[0].GetProperties().GenerateFromModel();

        return actionMethod.ReturnParameter!.ParameterType.GetProperties().GenerateFromModel();
    }
    public static IEnumerable<PropertyModel> ConvertFirstParameter(this MethodInfo actionMethod)
        => actionMethod.GetParameters().FirstOrDefault()?.ParameterType.GetProperties().GenerateFromModel() ?? [];
}