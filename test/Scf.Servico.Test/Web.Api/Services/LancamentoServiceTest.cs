using Scf.Servico.ApplicationCore.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Scf.Servico.Test.Web.Api.Services
{
    /// <summary>
    /// LancamentoServiceTest
    /// </summary>
    public class LancamentoServiceTest : TesteBase
    {
        #region ListarAsync
        [Fact]
        public async Task ListarAsync()
        {
            this.LancamentoServiceMock.Setup(x => x.Dispose());

            this.LancamentoServiceMock.Setup(x => x.ListarAsync())
                .ReturnsAsync(SetupLancamentoResponse());

            //var stubLancamento = new LancamentoService(LancamentoRepositoryMock.Object);

            //var resultado = await stubLancamento.ListarAsync();

            //resultado.Should().NotBeNull();

            //stubLancamento.Dispose();
        }
        #endregion
    }
}
