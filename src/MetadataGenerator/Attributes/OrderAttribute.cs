using System;

namespace MetadataGenerator.Attributes;

public class OrderAttribute(int order) : Attribute
{
    public int? Value { get; set; } = order;
}