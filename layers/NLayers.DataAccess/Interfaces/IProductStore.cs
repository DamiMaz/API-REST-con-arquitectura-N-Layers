using NLayers.Entities.Entities;

namespace NLayers.DataAccess.Interfaces;

public interface IProductStore
{
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> GetAllAsync();
    Task<Product> CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}
