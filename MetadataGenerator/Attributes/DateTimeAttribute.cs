using System;

namespace MetadataGenerator.Attributes;

public class DateTimeAttribute : Attribute
{
    public bool IsDate { get; set; }
}