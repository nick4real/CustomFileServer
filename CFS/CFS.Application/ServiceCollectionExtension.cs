using CFS.Application.Interfaces.Services;
using CFS.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CFS.Application;

public static class ServiceCollectionExtension
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        {
            //Services
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
