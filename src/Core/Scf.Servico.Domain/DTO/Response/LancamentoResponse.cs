using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Domain.DTO.Response
{
    [ExcludeFromCodeCoverageAttribute]
    public class LancamentoResponse
    {
        public int Codigo { get; set; }
        public int TipoLancamento { get; set; }
        public string? DescricaoLancamento { get; set; }
        public decimal ValorLancamento { get; set; }
        public DateTime DataDoLancamento { get; set; }

    }    
}
