using System;
using System.Collections.Generic;

namespace MetadataGenerator.Models;

public class AppData
{
    public Guid Version { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public ICollection<PageControllerModel> PageControllers { get; set; } = new List<PageControllerModel>();
}