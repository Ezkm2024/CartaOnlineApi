namespace CartaOnline.API.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    
    // Navigation properties
    public virtual Company Company { get; set; } = null!;
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

