using System.Collections.Generic;

namespace MetadataGenerator.Models;

public class ActionModel
{
    public bool IsAuth { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ActionUri { get; set; } = string.Empty;
    public string RequestType { get; set; } = string.Empty;
    public string? RequestParameterStatus { get; set; } = string.Empty;

    public IEnumerable<PropertyModel> RequestProperties { get; set; } = new List<PropertyModel>();
    public IEnumerable<PropertyModel> ResponseProperties { get; set; } = new List<PropertyModel>();
}