using Scf.Servico.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace Scf.Servico.Domain.Entities
{

    /// <summary>
    /// Classe TConsolidados
    /// </summary>
    public class TConsolidados : Entity
    {
        [Key]
        public int Codigo { get; set; }
        public DateTime DataConsolidado { get; set; }
        public decimal TotalEntrada { get; set; }
        public decimal TotalSaida { get; set; }
        public decimal ValorConsolidado { get; set; }
    }

    public class TConsolidadosString : Entity
    {       
        public int Codigo { get; set; }
        public string DataConsolidado { get; set; }
        public decimal TotalEntrada { get; set; }
        public decimal TotalSaida { get; set; }
        public decimal ValorConsolidado { get; set; }
    }
}
