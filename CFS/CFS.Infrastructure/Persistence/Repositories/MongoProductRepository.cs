using CFS.Application.Interfaces.Repositories;
using CFS.Domain.Entities;
using CFS.Infrastructure.Maps;
using MongoDB.Driver;

namespace CFS.Infrastructure.Persistence.Repositories;

public class MongoProductRepository(MongoDbContext dbContext) : IProductRepository
{
    public async Task AddProductAsync(CancellationToken ct, Product product)
        => await dbContext.Products.InsertOneAsync(product, cancellationToken: ct);

    public async Task<List<Product>> GetAllProductsAsync(CancellationToken ct)
        => await dbContext.Products.Find(_ => true).ToListAsync(ct);
    
    public async Task<Product> GetProductByIdAsync(Guid id, CancellationToken ct)
        => await dbContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync(ct);
}
