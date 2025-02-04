﻿using System;

namespace MetadataGenerator.DescriptionBuilder.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ConfigControllerAttribute : Attribute
{
    public bool IsShowInMenu { get; set; }
    public int MenuOrder { get; set; }
    public string Icon { get; set; } = string.Empty;
}