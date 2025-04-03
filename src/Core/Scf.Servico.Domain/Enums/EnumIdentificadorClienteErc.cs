using System.ComponentModel;

namespace Scf.Servico.Domain.Enums
{
    public enum EnumIdentificadorClienteErc
    {
        [Description("Nivel Cliente Margem Bruta Acumulada")]
        MargemBrutaAcumulada = 1,

        [Description("Nivel Cliente Margem Bruta Fne")]
        MargemBrutaFne = 2,

        [Description("Nivel Cliente Margem Bruta Nao Fne")]
        MargemBrutaNaoFne = 3,
    }
}
