using Microsoft.EntityFrameworkCore;
using Products.Application.Products.Abstractions;
using Products.Domain.Entities;

namespace Products.Infrastructure.Persistence;

public sealed class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Products
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync(cancellationToken);
    }

    public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
