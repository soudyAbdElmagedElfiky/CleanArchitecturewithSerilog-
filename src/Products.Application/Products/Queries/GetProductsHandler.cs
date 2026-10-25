using MediatR;
using Products.Application.Products.Abstractions;
using Products.Application.Products.DTOs;

namespace Products.Application.Products.Queries;

public sealed class GetProductsHandler(IProductRepository repository)
    : IRequestHandler<GetProductsQuery, IReadOnlyList<ProductDto>>
{
    public async Task<IReadOnlyList<ProductDto>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await repository.GetAllAsync(cancellationToken);
        return products.Select(p => new ProductDto(p.Id, p.Name, p.Price)).ToList();
    }
}
