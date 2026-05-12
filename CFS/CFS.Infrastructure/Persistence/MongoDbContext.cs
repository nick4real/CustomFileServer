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
    public GridFSBucket GridFSBucket => _gridFSBucket;

    public MongoDbContext(IOptions<MongoDbOptions> options)
    {
        var mongoOptions = options.Value;

        _mongoClient = new MongoClient(mongoOptions.Uri);
        _mongoDatabase = _mongoClient.GetDatabase(mongoOptions.DatabaseName);
        _gridFSBucket = new GridFSBucket(_mongoDatabase, new GridFSBucketOptions
        {
            BucketName = mongoOptions.CollectionName
        });
    }
}
