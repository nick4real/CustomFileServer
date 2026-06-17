using CFS.Domain.Entities;

namespace CFS.Application.Interfaces.Repositories;

public interface IProductRepository
{
    public Task<List<Product>> GetAllProductsAsync(CancellationToken ct);
    public Task<Product> GetProductByIdAsync(Guid id, CancellationToken ct);
    public Task AddProductAsync(CancellationToken ct, Product product);
}
