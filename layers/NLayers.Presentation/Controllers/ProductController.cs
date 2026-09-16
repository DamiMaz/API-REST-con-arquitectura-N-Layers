using Microsoft.AspNetCore.Mvc;
using NLayers.BusinessLogic.Interfaces;
using NLayers.Entities.Entities;
using NLayers.Presentation.Base;
using NLayers.Presentation.Inputs;
using NLayers.Presentation.Outputs;

namespace NLayers.Presentation.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : BaseController
{
    private readonly IProductManager _productManager;

    public ProductController(IProductManager productManager)
    {
        _productManager = productManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productManager.GetProductsAsync();
        var outputs = products.Select(p => new ProductOutput
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Status = p.Status
        }).ToList();

        return Success(outputs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var product = await _productManager.GetProductAsync(id);
            if (product == null)
            {
                return NotFoundResult($"Producto con Id {id} no encontrado.");
            }

            var output = new ProductOutput
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Status = product.Status
            };

            return Success(output);
        }
        catch (ArgumentException ex)
        {
            return BadRequestResult(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateInput input)
    {
        try
        {
            var entity = new Product
            {
                Name = input.Name,
                Price = input.Price
            };

            var created = await _productManager.CreateProductAsync(entity);
            var output = new ProductOutput
            {
                Id = created.Id,
                Name = created.Name,
                Price = created.Price,
                Status = created.Status
            };

            return CreatedResult(output, "El producto se creó exitosamente");
        }
        catch (ArgumentException ex)
        {
            return BadRequestResult(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateInput input)
    {
        try
        {
            var entity = new Product
            {
                Id = id,
                Name = input.Name,
                Price = input.Price,
                Status = input.Status
            };

            await _productManager.UpdateProductAsync(entity);
            return Success(null, "El producto se actualizó exitosamente");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFoundResult(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequestResult(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _productManager.DeleteProductAsync(id);
            return Success(null, "El producto se eliminó exitosamente");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFoundResult(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequestResult(ex.Message);
        }
    }
}
