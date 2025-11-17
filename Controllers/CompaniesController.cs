using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CartaOnline.API.Data;
using CartaOnline.API.DTOs;
using CartaOnline.API.Models;

namespace CartaOnline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CompaniesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Companies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
    {
        var companies = await _context.Companies
            .Select(c => new CompanyDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Phone = c.Phone,
                Email = c.Email,
                LogoUrl = c.LogoUrl
            })
            .ToListAsync();

        return Ok(companies);
    }

    // GET: api/Companies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> GetCompany(int id)
    {
        var company = await _context.Companies.FindAsync(id);

        if (company == null)
        {
            return NotFound();
        }

        var companyDto = new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Address = company.Address,
            Phone = company.Phone,
            Email = company.Email,
            LogoUrl = company.LogoUrl
        };

        return Ok(companyDto);
    }

    // POST: api/Companies
    [HttpPost]
    public async Task<ActionResult<CompanyDto>> CreateCompany(CreateCompanyDto createDto)
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

        // Verificar si ya existe una empresa con el mismo email
        var existingCompany = await _context.Companies
            .FirstOrDefaultAsync(c => c.Email.ToLower() == createDto.Email.ToLower());

        if (existingCompany != null)
        {
            return BadRequest(new { message = "Ya existe una empresa con este email" });
        }

        var company = new Company
        {
            Name = createDto.Name.Trim(),
            Address = createDto.Address.Trim(),
            Phone = createDto.Phone.Trim(),
            Email = createDto.Email.ToLower().Trim(),
            LogoUrl = createDto.LogoUrl?.Trim()
        };

        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        var companyDto = new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Address = company.Address,
            Phone = company.Phone,
            Email = company.Email,
            LogoUrl = company.LogoUrl
        };

        return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, new
        {
            message = "Empresa creada exitosamente",
            data = companyDto
        });
    }

    // PUT: api/Companies/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, UpdateCompanyDto updateDto)
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

        var company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            return NotFound(new { message = "Empresa no encontrada" });
        }

        // Verificar si el email ya existe en otra empresa
        var existingCompany = await _context.Companies
            .FirstOrDefaultAsync(c => c.Email.ToLower() == updateDto.Email.ToLower() && c.Id != id);

        if (existingCompany != null)
        {
            return BadRequest(new { message = "Ya existe otra empresa con este email" });
        }

        company.Name = updateDto.Name.Trim();
        company.Address = updateDto.Address.Trim();
        company.Phone = updateDto.Phone.Trim();
        company.Email = updateDto.Email.ToLower().Trim();
        company.LogoUrl = updateDto.LogoUrl?.Trim();

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CompanyExists(id))
            {
                return NotFound(new { message = "Empresa no encontrada" });
            }
            throw;
        }

        return Ok(new { message = "Empresa actualizada exitosamente" });
    }

    // DELETE: api/Companies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        var company = await _context.Companies
            .Include(c => c.Categories)
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (company == null)
        {
            return NotFound(new { message = "Empresa no encontrada" });
        }

        // Verificar si la empresa tiene categorías o productos asociados
        if (company.Categories.Any() || company.Products.Any())
        {
            return BadRequest(new
            {
                message = "No se puede eliminar la empresa porque tiene categorías y/o productos asociados. Elimine primero las categorías y productos."
            });
        }

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Empresa eliminada exitosamente" });
    }

    private bool CompanyExists(int id)
    {
        return _context.Companies.Any(e => e.Id == id);
    }
}

