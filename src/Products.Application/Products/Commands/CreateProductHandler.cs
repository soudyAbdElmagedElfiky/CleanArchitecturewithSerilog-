using MediatR;
using Products.Application.Products.Abstractions;
using Products.Application.Products.DTOs;
using Products.Domain.Entities;

namespace Products.Application.Products.Commands;

public sealed class CreateProductHandler(IProductRepository repository)
    : IRequestHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Price);
        await repository.AddAsync(product, cancellationToken);

        return new ProductDto(product.Id, product.Name, product.Price);
    }
}
