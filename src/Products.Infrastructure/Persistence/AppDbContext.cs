using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;

namespace Products.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).HasMaxLength(120).IsRequired();
            entity.Property(p => p.Price).HasPrecision(18, 2).IsRequired();
        });
    }
}
