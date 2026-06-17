using CFS.Application.Common.Result;
using CFS.Domain.Entities;

namespace CFS.Application.Interfaces.Services;

public interface IProductService
{
    public Task<Result<IEnumerable<Product>>> GetProductsAsync(CancellationToken ct);
    public Task<Result<Product>> GetProductByIdAsync(string id, CancellationToken ct);
    public Task<Result> AddProductAsync(CancellationToken ct, Product product);
}
