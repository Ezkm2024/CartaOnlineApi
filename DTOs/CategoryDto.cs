using System.ComponentModel.DataAnnotations;

namespace CartaOnline.API.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; }
}

public class CreateCategoryDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 200 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "La empresa es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID de la empresa debe ser mayor a 0")]
    public int CompanyId { get; set; }
}

public class UpdateCategoryDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 200 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "La empresa es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID de la empresa debe ser mayor a 0")]
    public int CompanyId { get; set; }
}

