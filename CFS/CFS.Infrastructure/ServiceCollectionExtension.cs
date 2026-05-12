using CFS.Application.Interfaces.Repositories;
using CFS.Infrastructure.Persistence;
using CFS.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CFS.Infrastructure;

public static class ServiceCollectionExtension
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(IConfiguration configuration)
        {
            // PostgreSQL
            services.AddDbContext<AppDbContext>(o =>
            {
                o.UseNpgsql(configuration.GetConnectionString("CFS-PostgresDB"), builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            });

            // MongoDB
            services.AddSingleton<MongoDbContext>();

            //Repositories
            services.AddScoped<IMetadataRepository, MetadataRepository>();
            services.AddScoped<IFileRepository, FileRepository>();

            return services;
        }
    }
}
