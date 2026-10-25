using System.ComponentModel.DataAnnotations;

namespace Products.Application.Products.DTOs;

public sealed class CreateProductRequest
{
    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string Name { get; init; } = string.Empty;

    [Range(typeof(decimal), "0.01", "999999999")]
    public decimal Price { get; init; }
}
