using Scf.Servico.Domain.SeedWork;
using Scf.Servico.Infrastructure.Common.Cache;
using Scf.Servico.Infrastructure.Common.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Infrastructure.Common
{
    [ExcludeFromCodeCoverageAttribute]
    public static class DependencyRegister
    {
        public static IServiceCollection RegisterCommonDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheConfiguration>(configuration.GetSection(CacheConfiguration.Cache));
            services.AddMemoryCache();
            services.AddTransient<ICache, CacheManager>();

            return services;
        }
    }
}
