using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CartaOnline.API.Data;
using CartaOnline.API.DTOs;
using CartaOnline.API.Models;

namespace CartaOnline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories([FromQuery] int? companyId = null)
    {
        var query = _context.Categories.AsQueryable();

        if (companyId.HasValue)
        {
            query = query.Where(c => c.CompanyId == companyId.Value);
        }

        var categories = await query
            .Include(c => c.Company)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                CompanyId = c.CompanyId,
                CompanyName = c.Company.Name
            })
            .ToListAsync();

        return Ok(categories);
    }

    // GET: api/Categories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Company)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            CompanyId = category.CompanyId,
            CompanyName = category.Company.Name
        };

        return Ok(categoryDto);
    }

    // POST: api/Categories
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createDto)
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

        // Verificar que la empresa existe
        var company = await _context.Companies.FindAsync(createDto.CompanyId);
        if (company == null)
        {
            return BadRequest(new { message = "La empresa especificada no existe" });
        }

        // Verificar que no exista una categoría con el mismo nombre en la misma empresa
        var existingCategory = await _context.Categories
            .FirstOrDefaultAsync(c => c.Name.ToLower() == createDto.Name.ToLower() && c.CompanyId == createDto.CompanyId);

        if (existingCategory != null)
        {
            return BadRequest(new { message = "Ya existe una categoría con este nombre en la empresa especificada" });
        }

        var category = new Category
        {
            Name = createDto.Name.Trim(),
            CompanyId = createDto.CompanyId
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            CompanyId = category.CompanyId,
            CompanyName = company.Name
        };

        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, new
        {
            message = "Categoría creada exitosamente",
            data = categoryDto
        });
    }

    // PUT: api/Categories/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto updateDto)
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

        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound(new { message = "Categoría no encontrada" });
        }

        // Verificar que la empresa existe
        var company = await _context.Companies.FindAsync(updateDto.CompanyId);
        if (company == null)
        {
            return BadRequest(new { message = "La empresa especificada no existe" });
        }

        // Verificar que no exista otra categoría con el mismo nombre en la misma empresa
        var existingCategory = await _context.Categories
            .FirstOrDefaultAsync(c => c.Name.ToLower() == updateDto.Name.ToLower() &&
                                    c.CompanyId == updateDto.CompanyId && c.Id != id);

        if (existingCategory != null)
        {
            return BadRequest(new { message = "Ya existe otra categoría con este nombre en la empresa especificada" });
        }

        category.Name = updateDto.Name.Trim();
        category.CompanyId = updateDto.CompanyId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(id))
            {
                return NotFound(new { message = "Categoría no encontrada" });
            }
            throw;
        }

        return Ok(new { message = "Categoría actualizada exitosamente" });
    }

    // DELETE: api/Categories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound(new { message = "Categoría no encontrada" });
        }

        // Verificar si la categoría tiene productos asociados
        if (category.Products.Any())
        {
            return BadRequest(new
            {
                message = "No se puede eliminar la categoría porque tiene productos asociados. Elimine primero los productos."
            });
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Categoría eliminada exitosamente" });
    }

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.Id == id);
    }
}

