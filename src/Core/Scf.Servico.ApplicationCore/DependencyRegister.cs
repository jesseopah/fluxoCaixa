using Microsoft.Extensions.DependencyInjection;
using Scf.Servico.ApplicationCore.Converters;
using Scf.Servico.ApplicationCore.Services;
using Scf.Servico.Domain.Interfaces.Services;
using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.ApplicationCore
{
    [ExcludeFromCodeCoverageAttribute]
    public static class DependencyRegister
    {
        public static IServiceCollection RegisterCoreDependencies(this IServiceCollection services)
        {
            services.AddTransient<ILancamentoService, LancamentoService>();
               
            services.RegisterMaps();
            return services;
        }
    }
}
