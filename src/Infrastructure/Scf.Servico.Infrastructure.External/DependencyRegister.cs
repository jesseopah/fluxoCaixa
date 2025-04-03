using Scf.Servico.Infrastructure.External.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;

namespace Scf.Servico.Infrastructure.External
{
    public static class DependencyRegister
    {
        // MicroService Settings
        //private static string GerencialMicroService = "Services:GerencialMicroService";
        //private static string PropostaMicroService = "Services:PropostaMicroService";
        //private static string ParametrosMicroService = "Services:ParametrosMicroService";

        public static IServiceCollection RegisterExternalDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurarion
            return services;
        }
    }
}
