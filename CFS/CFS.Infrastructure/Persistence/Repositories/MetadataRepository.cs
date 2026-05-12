using CFS.Application.Interfaces.Repositories;
using CFS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CFS.Infrastructure.Persistence.Repositories
{
    public class MetadataRepository(AppDbContext appDbContext) : IMetadataRepository
    {
        public async Task<IReadOnlyList<Metadata>?> GetAllMetadataAsync(CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
                return null;

            return await appDbContext.Set<Metadata>().ToListAsync(ct);
        }

        public async Task<Metadata?> GetMetadataByIdAsync(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task AddMetadataAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
