using CFS.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace CFS.Infrastructure.Persistence.Repositories;

public class FileRepository(MongoDbContext dbContext) : IFileRepository
{
    public async Task<string> SaveFileAsync(IFormFile file, CancellationToken ct)
    {
        ObjectId gridFsId;
        await using (var stream = file.OpenReadStream())
        {
            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument
                {
                    { "ContentType", file.ContentType },
                    { "FileName", file.FileName },
                    { "SizeBytes", file.Length },
                    { "UploadedAtUtc", DateTime.UtcNow }
                }
            };

            gridFsId = await dbContext.GridFSBucket.UploadFromStreamAsync(file.FileName, stream, options, ct);
        }

        return gridFsId.ToString();
    }

    public async Task<Stream?> LoadFileAsync(string gridFsId, CancellationToken ct)
    {
        if (!ObjectId.TryParse(gridFsId, out var objectId))
            return null;

        var stream = new MemoryStream();
        await dbContext.GridFSBucket.DownloadToStreamAsync(objectId, stream, cancellationToken: ct);
        stream.Position = 0;
        return stream;
    }
}
