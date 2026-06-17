using CFS.Application.Common.Result;
using CFS.Application.Interfaces.Repositories;
using CFS.Application.Interfaces.Services;
using CFS.Domain.Entities;

namespace CFS.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<Result> AddProductAsync(CancellationToken ct, Product product)
    {
        if (ct.IsCancellationRequested)
            return Result.Failure(new Error(ErrorCode.BadRequest, "Request was cancelled."));

        // TODO: Product check

        try
        {
            await productRepository.AddProductAsync(ct, product);
        }
        catch
        {
            return Result.Failure(new Error(ErrorCode.InternalServerError, "Error while proccessing product."));
        }

        return Result.Success();
    }

    public async Task<Result<Product>> GetProductByIdAsync(CancellationToken ct, Guid id)
    {
        if (ct.IsCancellationRequested)
            return Result<Product>.Failure(new Error(ErrorCode.BadRequest, "Request was cancelled."));

        var result = await productRepository.GetProductByIdAsync(id, ct);

        if (result is null)
            return Result<Product>.Failure(new Error(ErrorCode.NotFound, "Not found."));

        return Result<Product>.Success(result);
    }

    public async Task<Result<IEnumerable<Product>>> GetProductsAsync(CancellationToken ct)
    {
        if (ct.IsCancellationRequested)
            return Result<IEnumerable<Product>>.Failure(new Error(ErrorCode.BadRequest, "Request was cancelled."));

        var result = await productRepository.GetAllProductsAsync(ct);

        if (result is null || result.Count <= 0)
            return Result<IEnumerable<Product>>.Failure(new Error(ErrorCode.NotFound, "Not found."));

        return Result<IEnumerable<Product>>.Success(result!);
    }
}
