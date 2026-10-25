using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Products.Commands;
using Products.Application.Products.DTOs;
using Products.Application.Products.Queries;

namespace Products.API.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(
    ISender sender,
    ILogger<ProductsController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("Request to get all products");

        var products = await sender.Send(new GetProductsQuery(), cancellationToken);

        logger.LogInformation("Retrieved {ProductCount} products", products.Count);
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetById(int id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Request to get product {ProductId}", id);

        if (id <= 0)
        {
            logger.LogWarning("Rejected request for product with invalid Id {ProductId}", id);
            return BadRequest(new { Message = "Id must be greater than zero." });
        }

        var product = await sender.Send(new GetProductByIdQuery(id), cancellationToken);

        if (product is null)
        {
            logger.LogWarning("Product {ProductId} was not found", id);
            return NotFound();
        }

        logger.LogInformation("Product {ProductId} retrieved successfully", id);
        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> Create(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Request to create product {ProductName}", request.Name);

        var product = await sender.Send(
            new CreateProductCommand(request.Name, request.Price),
            cancellationToken);

        logger.LogInformation("Product {ProductId} created successfully", product.Id);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
}
