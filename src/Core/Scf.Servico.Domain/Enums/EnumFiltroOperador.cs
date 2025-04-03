using System.ComponentModel;

namespace Scf.Servico.Domain.Enums
{
    public enum EnumFiltroOperador
    {
        [Description("Contém")]
        Contem = 1,

        [Description("Igual")]
        Igual = 2,

        [Description("Maior que")]
        MaiorQue = 3,

        [Description("Menor que")]
        MenorQue = 4,

        [Description("Entre")]
        Entre = 5
    }
}
