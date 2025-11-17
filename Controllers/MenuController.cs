using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CartaOnline.API.Data;
using CartaOnline.API.DTOs;

namespace CartaOnline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MenuController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Menu/company/{companyId}
    [HttpGet("company/{companyId}")]
    public async Task<ActionResult> GetMenuByCompany(int companyId)
    {
        var company = await _context.Companies.FindAsync(companyId);
        if (company == null)
        {
            return NotFound("Empresa no encontrada");
        }

        var categories = await _context.Categories
            .Where(c => c.CompanyId == companyId)
            .OrderBy(c => c.Name)
            .Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products
                    .OrderBy(p => p.Name)
                    .Select(p => new
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl
                    })
                    .ToList()
            })
            .ToListAsync();

        var menu = new
        {
            Company = new
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                Phone = company.Phone,
                Email = company.Email,
                LogoUrl = company.LogoUrl
            },
            Categories = categories
        };

        return Ok(menu);
    }

    // GET: api/Menu/company-name/{companyName}
    [HttpGet("company-name/{companyName}")]
    public async Task<ActionResult> GetMenuByCompanyName(string companyName)
    {
        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Name.ToLower() == companyName.ToLower());

        if (company == null)
        {
            return NotFound("Empresa no encontrada");
        }

        var categories = await _context.Categories
            .Where(c => c.CompanyId == company.Id)
            .OrderBy(c => c.Name)
            .Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products
                    .OrderBy(p => p.Name)
                    .Select(p => new
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl
                    })
                    .ToList()
            })
            .ToListAsync();

        var menu = new
        {
            Company = new
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                Phone = company.Phone,
                Email = company.Email,
                LogoUrl = company.LogoUrl
            },
            Categories = categories
        };

        return Ok(menu);
    }
}

