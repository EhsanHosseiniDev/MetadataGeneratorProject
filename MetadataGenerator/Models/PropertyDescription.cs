using System.Collections.Generic;

namespace MetadataGenerator.Models;

public class PropertyDescription
{
    public string Name { get; set; } = string.Empty;
    public string Display { get; set; } = string.Empty;
    public bool Hide { get; set; }
    public bool AllowQuery { get; set; }
    public bool ColumnHide { get; set; }
    public bool Filterable { get; set; }
    public bool IsId { get; set; }
    public bool PinAble { get; set; }
    public bool Searchable { get; set; }
    public bool Sortable { get; set; }
    public int Order { get; set; }
    public PropertyType PropertyType { get; set; }
    public string[] DefaultValues { get; set; } = [];
    public List<PropertyDescription> PropertyDescriptions { get; set; } = new();
}