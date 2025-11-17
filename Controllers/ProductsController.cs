using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CartaOnline.API.Data;
using CartaOnline.API.DTOs;
using CartaOnline.API.Models;

namespace CartaOnline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] int? companyId = null, [FromQuery] int? categoryId = null)
    {
        var query = _context.Products.AsQueryable();

        if (companyId.HasValue)
        {
            query = query.Where(p => p.CompanyId == companyId.Value);
        }

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        var products = await query
            .Include(p => p.Category)
            .ToListAsync();

        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CategoryId = p.CategoryId,
            CompanyId = p.CompanyId,
            ImageUrl = p.ImageUrl,
            CategoryName = p.Category?.Name ?? string.Empty
        }).ToList();

        return Ok(productDtos);
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CompanyId = product.CompanyId,
            ImageUrl = product.ImageUrl,
            CategoryName = product.Category?.Name ?? string.Empty
        };

        return Ok(productDto);
    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Los datos proporcionados no son válidos",
                errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList()
            });
        }

        // Verificar que la categoría existe
        var category = await _context.Categories.FindAsync(createDto.CategoryId);
        if (category == null)
        {
            return BadRequest(new { message = "La categoría especificada no existe" });
        }

        // Verificar que la empresa existe
        var company = await _context.Companies.FindAsync(createDto.CompanyId);
        if (company == null)
        {
            return BadRequest(new { message = "La empresa especificada no existe" });
        }

        // Verificar que la categoría pertenece a la empresa
        if (category.CompanyId != createDto.CompanyId)
        {
            return BadRequest(new { message = "La categoría no pertenece a la empresa especificada" });
        }

        // Verificar que no exista un producto con el mismo nombre en la misma categoría
        var existingProduct = await _context.Products
            .FirstOrDefaultAsync(p => p.Name.ToLower() == createDto.Name.ToLower() && p.CategoryId == createDto.CategoryId);

        if (existingProduct != null)
        {
            return BadRequest(new { message = "Ya existe un producto con este nombre en la categoría especificada" });
        }

        var product = new Product
        {
            Name = createDto.Name.Trim(),
            Description = createDto.Description?.Trim(),
            Price = createDto.Price,
            CategoryId = createDto.CategoryId,
            CompanyId = createDto.CompanyId,
            ImageUrl = createDto.ImageUrl?.Trim()
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CompanyId = product.CompanyId,
            ImageUrl = product.ImageUrl,
            CategoryName = category.Name
        };

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, new
        {
            message = "Producto creado exitosamente",
            data = productDto
        });
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Los datos proporcionados no son válidos",
                errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList()
            });
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new { message = "Producto no encontrado" });
        }

        // Verificar que la categoría existe
        var category = await _context.Categories.FindAsync(updateDto.CategoryId);
        if (category == null)
        {
            return BadRequest(new { message = "La categoría especificada no existe" });
        }

        // Verificar que la empresa existe
        var company = await _context.Companies.FindAsync(updateDto.CompanyId);
        if (company == null)
        {
            return BadRequest(new { message = "La empresa especificada no existe" });
        }

        // Verificar que la categoría pertenece a la empresa
        if (category.CompanyId != updateDto.CompanyId)
        {
            return BadRequest(new { message = "La categoría no pertenece a la empresa especificada" });
        }

        // Verificar que no exista otro producto con el mismo nombre en la misma categoría
        var existingProduct = await _context.Products
            .FirstOrDefaultAsync(p => p.Name.ToLower() == updateDto.Name.ToLower() &&
                                    p.CategoryId == updateDto.CategoryId && p.Id != id);

        if (existingProduct != null)
        {
            return BadRequest(new { message = "Ya existe otro producto con este nombre en la categoría especificada" });
        }

        product.Name = updateDto.Name.Trim();
        product.Description = updateDto.Description?.Trim();
        product.Price = updateDto.Price;
        product.CategoryId = updateDto.CategoryId;
        product.CompanyId = updateDto.CompanyId;
        product.ImageUrl = updateDto.ImageUrl?.Trim();

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound(new { message = "Producto no encontrado" });
            }
            throw;
        }

        return Ok(new { message = "Producto actualizado exitosamente" });
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new { message = "Producto no encontrado" });
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Producto eliminado exitosamente" });
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}

