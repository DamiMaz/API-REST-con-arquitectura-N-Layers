using NLayers.BusinessLogic.Interfaces;
using NLayers.Core.Utils;
using NLayers.DataAccess.Interfaces;
using NLayers.Entities.Entities;

namespace NLayers.BusinessLogic.Managers;

public class ProductManager : IProductManager
{
    private readonly IProductStore _productStore;

    public ProductManager(IProductStore productStore)
    {
        _productStore = productStore;
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("El identificador debe ser mayor a 0.", nameof(id));
        }

        return await _productStore.GetByIdAsync(id);
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _productStore.GetAllAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        if (StringUtils.IsNullOrEmpty(product.Name))
        {
            throw new ArgumentException("El nombre del producto es obligatorio.", nameof(product.Name));
        }

        if (!ValidationUtils.IsValidPrice(product.Price))
        {
            throw new ArgumentException("El precio del producto debe ser mayor a 0.", nameof(product.Price));
        }

        return await _productStore.CreateAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        if (product.Id <= 0)
        {
            throw new ArgumentException("El identificador debe ser mayor a 0.", nameof(product.Id));
        }

        if (StringUtils.IsNullOrEmpty(product.Name))
        {
            throw new ArgumentException("El nombre del producto es obligatorio.", nameof(product.Name));
        }

        if (!ValidationUtils.IsValidPrice(product.Price))
        {
            throw new ArgumentException("El precio del producto debe ser mayor a 0.", nameof(product.Price));
        }

        var existing = await _productStore.GetByIdAsync(product.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"No se encontró el producto con Id {product.Id}.");
        }

        existing.Name = product.Name;
        existing.Price = product.Price;
        existing.Status = product.Status;

        await _productStore.UpdateAsync(existing);
    }

    public async Task DeleteProductAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("El identificador debe ser mayor a 0.", nameof(id));
        }

        var existing = await _productStore.GetByIdAsync(id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"No se encontró el producto con Id {id}.");
        }

        await _productStore.DeleteAsync(id);
    }
}
