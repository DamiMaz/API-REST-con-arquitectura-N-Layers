using NLayers.Entities.Enums;

namespace NLayers.Presentation.Inputs;

public class ProductUpdateInput
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; } = ProductStatus.Active;
}
