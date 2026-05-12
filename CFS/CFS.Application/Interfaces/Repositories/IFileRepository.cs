using Microsoft.AspNetCore.Http;

namespace CFS.Application.Interfaces.Repositories;

public interface IFileRepository
{
    Task SaveFile(IFormFile file, CancellationToken ct);
    Task LoadFile(Guid id, CancellationToken ct);
}
