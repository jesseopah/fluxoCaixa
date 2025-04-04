using Scf.Servico.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Scf.Servico.Test.Web.Api.Entities
{
    /// <summary>
    /// LancamentoTest
    /// </summary>
    public class LancamentoTest : TesteBase
    {
        #region Lancamento
        [Fact]
        public void Lancamento()
        {
            var retorno = new Tlancamentos()
            {
                Codigo = 1,
                DataDoLancamento = DateTime.Now,
                TipoLancamento = 1              
            };

            retorno.Should().NotBeNull();
        }
        #endregion
    }
}
