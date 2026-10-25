using MediatR;
using Products.Application.Products.DTOs;

namespace Products.Application.Products.Queries;

public sealed record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;
