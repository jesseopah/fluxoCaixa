using Scf.Servico.Domain.DTO.Response;
using Scf.Servico.Domain.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Scf.Servico.ApplicationCore.Converters
{
    [ExcludeFromCodeCoverageAttribute]
    public static class ServiceConverter
    {
        public static void RegisterMaps(this IServiceCollection services)
        {

            TypeAdapterConfig<LancamentoResponse, Tlancamentos>
                .NewConfig()
                .Map(member => member.CodigoLancamento, source => source.CodigoLancamento)
                .Map(member => member.TipoLancamento, source => source.TipoLancamento)
                .Map(member => member.DataDoLancamento, source => source.DataDoLancamento)
                .Map(member => member.DescricaoLancamento, source => source.DescricaoLancamento)
                .Map(member => member.ValorLancamento, source => source.ValorLancamento);

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
