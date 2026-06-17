using CFS.Application.Common.Result;
using CFS.Domain.Entities;

namespace CFS.Application.Interfaces.Services;

public interface IProductService
{
    public Task<Result<IEnumerable<Product>>> GetProductsAsync(CancellationToken ct);
    public Task<Result<Product>> GetProductByIdAsync(CancellationToken ct, Guid id);
    public Task<Result> AddProductAsync(CancellationToken ct, Product product);
}
