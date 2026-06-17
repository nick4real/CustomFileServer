using CFS.Domain.Entities;
using CFS.Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace CFS.Infrastructure.Persistence;

public class MongoDbContext
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly GridFSBucket _gridFSBucket;
    private readonly IMongoCollection<Product> _products;
    public GridFSBucket GridFSBucket => _gridFSBucket;
    public IMongoCollection<Product> Products => _products;

    public MongoDbContext(IOptions<MongoDbOptions> options)
    {
        var mongoOptions = options.Value;

        _mongoClient = new MongoClient(mongoOptions.Uri);
        _mongoDatabase = _mongoClient.GetDatabase(mongoOptions.DatabaseName);
        _gridFSBucket = new GridFSBucket(_mongoDatabase, new GridFSBucketOptions
        {
            BucketName = mongoOptions.FilesCollectionName
        });
        _products = _mongoDatabase.GetCollection<Product>(mongoOptions.ProductsCollectionName);
    }
}
