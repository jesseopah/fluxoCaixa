using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Domain.DTO.Response
{
    [ExcludeFromCodeCoverageAttribute]
    public class ConsolidadoResponse
    {
        public DateTime DataConsolidado { get; set; }
        public decimal TotalEntrada { get; set; }
        public decimal TotalSaida { get; set; }
        public decimal ValorConsolidado { get; set; }

    }    
}
