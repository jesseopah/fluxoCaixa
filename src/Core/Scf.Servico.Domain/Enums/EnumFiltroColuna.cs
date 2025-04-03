using System.ComponentModel;

namespace Scf.Servico.Domain.Enums
{
    public enum EnumFiltroPorColuna
    {
        [Description("SICAD")]
        Sicad = 1,

        [Description("Nome")]
        Nome = 2,

        [Description("Valor")]
        Valor = 3,

        [Description("Participação")]
        Participacao = 4,

        [Description("Saldo")]
        Saldo = 5,

        [Description("MargemMes")]
        MargemMes = 6,

        [Description("MargemPeriodo")]
        MargemPeriodo = 7,

        [Description("MargemLiquida")]
        MargemLiquida = 8,

        [Description("SaldoDevedor")]
        SaldoDevedor = 9,

        [Description("ValorEmAtraso")]
        ValorEmAtraso = 10,

        [Description("DataVigenciaCadastro")]
        DataVigenciaCadastro = 11,

        [Description("CadastroVigente")]
        CadastroVigente = 12,

        [Description("ValorLimiteAprovado")]
        ValorLimiteAprovado = 13,

        [Description("DataVigenciaLimite")]
        DataVigenciaLimite = 14,

        [Description("CodigoDoBem")]
        CodigoDoBem = 15,

        [Description("DataVigenciaSeguroObrigatorio")]
        DataVigenciaSeguroObrigatorio = 16,

        [Description("AvaliacaoVigente")]
        AvaliacaoVigente = 17,

        [Description("ProximaAvaliacao")]
        ProximaAvaliacao = 18,

        [Description("DescricaoSituacao")]
        DescricaoSituacao = 19,

        [Description("DescricaoConcluida")]
        DescricaoConcluida = 20,

        [Description("DataPlanejada")]
        DataPlanejada = 21,

        [Description("DataFinalizacaoAutomatica")]
        DataFinalizacaoAutomatica = 22
    }
}
