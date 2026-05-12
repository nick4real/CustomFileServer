using CFS.Domain.Entities;

namespace CFS.Application.Interfaces.Repositories;

public interface IMetadataRepository
{
    Task<IReadOnlyList<Metadata>?> GetAllMetadataAsync(CancellationToken ct);
    Task<Metadata?> GetMetadataByIdAsync(Guid id, CancellationToken ct);
    Task AddMetadataAsync(Metadata metadata, CancellationToken ct);
}
