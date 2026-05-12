using CFS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CFS.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Metadata> Metadatas { get; set; } = null!;
}
