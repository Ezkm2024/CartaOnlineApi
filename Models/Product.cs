namespace CartaOnline.API.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int CompanyId { get; set; }
    public string? ImageUrl { get; set; }
    
    // Navigation properties
    public virtual Category Category { get; set; } = null!;
    public virtual Company Company { get; set; } = null!;
}

