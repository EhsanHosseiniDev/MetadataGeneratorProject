using System.Collections.Generic;

namespace MetadataGenerator.Models;

public class ControllerModel
{
    public string ControllerUri { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<ActionModel> Actions { get; set; } = new();
}