using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CFS.Infrastructure.Models;

public class ProductBson
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public IDictionary<string, string> Parameters { get; set; }
}
