using System.ComponentModel.DataAnnotations;

namespace CartaOnline.API.DTOs;

public class CompanyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
}

public class CreateCompanyDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 200 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "La dirección es obligatoria")]
    [StringLength(500, ErrorMessage = "La dirección no puede exceder los 500 caracteres")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [StringLength(50, ErrorMessage = "El teléfono no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[\d\-\+\(\)\s]+$", ErrorMessage = "El formato del teléfono no es válido")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    [StringLength(200, ErrorMessage = "El email no puede exceder los 200 caracteres")]
    public string Email { get; set; } = string.Empty;

    [Url(ErrorMessage = "La URL del logo no es válida")]
    public string? LogoUrl { get; set; }
}

public class UpdateCompanyDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 200 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "La dirección es obligatoria")]
    [StringLength(500, ErrorMessage = "La dirección no puede exceder los 500 caracteres")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [StringLength(50, ErrorMessage = "El teléfono no puede exceder los 50 caracteres")]
    [RegularExpression(@"^[\d\-\+\(\)\s]+$", ErrorMessage = "El formato del teléfono no es válido")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    [StringLength(200, ErrorMessage = "El email no puede exceder los 200 caracteres")]
    public string Email { get; set; } = string.Empty;

    [Url(ErrorMessage = "La URL del logo no es válida")]
    public string? LogoUrl { get; set; }
}

