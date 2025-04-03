using Scf.Servico.Domain.Enums;
using Scf.Servico.Domain.Pagination.OperationResult;

namespace Scf.Servico.Domain.Pagination.Filters
{
   
    public class DynamicFilter : BaseFilter
    {
        public EnumFiltroPorColuna FiltroColuna { get; set; }

        public EnumFiltroOperador FiltroOperador { get; set; }

        public string? FiltroValor { get; set; } = string.Empty;

        public string? FiltroValorOpcional { get; set; } = string.Empty;
    }
}