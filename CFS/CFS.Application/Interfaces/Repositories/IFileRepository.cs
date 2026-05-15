using Microsoft.AspNetCore.Http;

namespace CFS.Application.Interfaces.Repositories;

public interface IFileRepository
{
    Task<string> SaveFileAsync(IFormFile file, CancellationToken ct);
    Task<Stream?> LoadFileAsync(string gridFsId, CancellationToken ct);
}
