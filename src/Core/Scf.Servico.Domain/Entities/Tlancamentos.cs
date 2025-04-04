using Scf.Servico.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace Scf.Servico.Domain.Entities
{

    /// <summary>
    /// Classe TLancamentos
    /// </summary>
    public class Tlancamentos : Entity
    {
        [Key]
        public int Codigo { get; set; }

        /// <summary>
        /// TipoDeLancamento pode ser 1 = Crédito e 2 = Debito 
        /// </summary>
        /// <value>Data de Referência</value>
        public int TipoLancamento { get; set; }
        public string? DescricaoLancamento { get; set; }
        public Decimal ValorLancamento { get; set; }
        public DateTime DataDoLancamento { get; set; }
    }
}
