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
            if (ct.IsCancellationRequested)
                return null;

            return await appDbContext.Set<Metadata>().FirstOrDefaultAsync(m => m.Id == id, ct);
        }

        public async Task AddMetadataAsync(Metadata metadata, CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
                return;

            await appDbContext.Set<Metadata>().AddAsync(metadata, ct);
            await appDbContext.SaveChangesAsync(ct);
        }
    }
}
