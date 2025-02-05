using System;
using System.Collections.Generic;

namespace MetadataGenerator.Models;

public class ApplicationModel
{
    public Guid Version { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<ControllerModel> Controllers { get; set; } = new List<ControllerModel>();
}