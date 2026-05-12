using CFS.Application.Interfaces.Repositories;
using CFS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace CFS.Infrastructure.Persistence.Repositories;

public class FileRepository(MongoDbContext dbContext) : IFileRepository
{
    public async Task SaveFile(IFormFile file, CancellationToken ct)
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
        };

        var metadata = new Metadata
        {
            Id = Guid.NewGuid(),
            FileName = file.FileName,
            ContentType = file.ContentType,
            SizeBytes = file.Length,
            UploadedAtUtc = DateTimeOffset.UtcNow,
            GridFsId = gridFsId.ToString()
        };


    }

    public async Task LoadFile(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
