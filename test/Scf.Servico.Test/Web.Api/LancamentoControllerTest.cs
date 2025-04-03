using FluentAssertions;
using Moq;
using Scf.Servico.Web.Api.Controllers;
using Xunit;

namespace Scf.Servico.Test.Web.Api
{
    /// <summary>
    /// LancamentoControllerTest
    /// </summary>
    public class LancamentoControllerTest : TesteBase
    {
        #region Listar
        [Fact]
        public async Task Listar()
        {
            this.LancamentoServiceMock.Setup(x => x.ListarAsync()).ReturnsAsync(SetupLancamentoResponse());

            var stubLancamento = new LancamentoController(LancamentoServiceMock.Object);
            var resultado = await stubLancamento.Get();

            this.LancamentoServiceMock.Verify(c => c.ListarAsync(), Times.Once);
            resultado?.FirstOrDefault().Should().NotBeNull();

        }
        #endregion
    }
}
