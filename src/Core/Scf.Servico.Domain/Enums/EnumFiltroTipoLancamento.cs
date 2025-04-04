using System.ComponentModel;

namespace Scf.Servico.Domain.Enums
{
    public enum EnumFiltroTipoLancamento
    {
        [Description("Crédito")]
        Contem = 1,

        [Description("Débito")]
        Igual = 2
    }
}
