using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Domain.DTO.Request
{
    [ExcludeFromCodeCoverageAttribute]
    public class LancamentoRequest
    {
        public int CodigoLancamento{ get; set; }
        public int TipoLancamento { get; set; }
        public string? DescricaoLancamento { get; set; }
        public decimal ValorLancamento { get; set; }
        public DateTime DataDoLancamento { get; set; }

    }
}
