using System.Collections.Generic;

namespace MetadataGenerator.Models;

public class PageControllerModel
{
    public string Name { get; set; } = string.Empty;
    public string Display { get; set; } = string.Empty;
    public bool ShowInMenu { get; set; }
    public int MenuOrder { get; set; }
    public string Icon { get; set; } = "bank";
    public bool CanUploadExcel { get; set; }
    public List<PropertyDescription> PropertyDescriptions { get; set; } = new();
}