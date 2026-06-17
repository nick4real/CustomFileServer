using Microsoft.Extensions.Configuration;

namespace CFS.Infrastructure.Options;

public class MongoDbOptions
{
    [ConfigurationKeyName("CFS_MONGODB_URI")]
    public string Uri { get; set; } = null!;

    [ConfigurationKeyName("CFS_MONGODB_DATABASENAME")]
    public string DatabaseName { get; set; } = null!;

    [ConfigurationKeyName("CFS_MONGODB_FILESCOLLECTIONNAME")]
    public string FilesCollectionName { get; set; } = "fileStorage";

    [ConfigurationKeyName("CFS_MONGODB_PRODUCTSCOLLECTIONNAME")]
    public string ProductsCollectionName { get; set; } = "productStorage";
}
