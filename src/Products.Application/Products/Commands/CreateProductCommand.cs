using MediatR;
using Products.Application.Products.DTOs;

namespace Products.Application.Products.Commands;

public sealed record CreateProductCommand(string Name, decimal Price) : IRequest<ProductDto>;
