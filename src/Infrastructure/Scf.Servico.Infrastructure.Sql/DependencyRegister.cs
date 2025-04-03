using Scf.Servico.Domain.Interfaces.Repositories;
using Scf.Servico.Infrastructure.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Infrastructure.Sql
{

    [ExcludeFromCodeCoverageAttribute]
    public static class DependencyRegister
    {
        public static IServiceCollection RegisterSqlDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var strConn = configuration.GetConnectionString("Context");
            
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(strConn));
            services.AddTransient<DbContext, AppDbContext>();

            services.AddTransient<ILancamentoRepository, LancamentoRepository>();
          
            return services;
        }
    }
}
