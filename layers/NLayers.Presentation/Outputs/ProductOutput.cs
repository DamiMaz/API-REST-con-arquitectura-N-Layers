using NLayers.Entities.Enums;

namespace NLayers.Presentation.Outputs;

public class ProductOutput
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
}
