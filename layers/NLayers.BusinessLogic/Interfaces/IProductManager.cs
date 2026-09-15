using NLayers.Entities.Entities;

namespace NLayers.BusinessLogic.Interfaces;

public interface IProductManager
{
    Task<Product?> GetProductAsync(int id);
    Task<List<Product>> GetProductsAsync();
    Task<Product> CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
}
