using System;

namespace MetadataGenerator.Attributes;

public class ParamKeyAttribute(string key, string alias) : Attribute
{
    public string Key { get; set; } = key;
    public string Alias { get; set; } = alias;
}