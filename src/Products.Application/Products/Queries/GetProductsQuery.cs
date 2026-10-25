using MediatR;
using Products.Application.Products.DTOs;

namespace Products.Application.Products.Queries;

public sealed record GetProductsQuery : IRequest<IReadOnlyList<ProductDto>>;
