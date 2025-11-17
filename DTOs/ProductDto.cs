using System.ComponentModel.DataAnnotations;

namespace CartaOnline.API.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int CompanyId { get; set; }
    public string? ImageUrl { get; set; }
    public string? CategoryName { get; set; }
}

public class CreateProductDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 200 caracteres")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, 999999.99, ErrorMessage = "El precio debe estar entre 0.01 y 999999.99")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID de la categoría debe ser mayor a 0")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "La empresa es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID de la empresa debe ser mayor a 0")]
    public int CompanyId { get; set; }

    [Url(ErrorMessage = "La URL de la imagen no es válida")]
    public string? ImageUrl { get; set; }
}

public class UpdateProductDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 200 caracteres")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, 999999.99, ErrorMessage = "El precio debe estar entre 0.01 y 999999.99")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID de la categoría debe ser mayor a 0")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "La empresa es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID de la empresa debe ser mayor a 0")]
    public int CompanyId { get; set; }

    [Url(ErrorMessage = "La URL de la imagen no es válida")]
    public string? ImageUrl { get; set; }
}

