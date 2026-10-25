using MediatR;
using Products.Application.Products.Abstractions;
using Products.Application.Products.DTOs;

namespace Products.Application.Products.Queries;

public sealed class GetProductByIdHandler(IProductRepository repository)
    : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);
        return product is null ? null : new ProductDto(product.Id, product.Name, product.Price);
    }
}
