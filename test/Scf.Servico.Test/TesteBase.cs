using Scf.Servico.Domain.DTO.Request;
using Scf.Servico.Domain.DTO.Response;
using Scf.Servico.Domain.Interfaces.Services;
using Moq;

namespace Scf.Servico.Test
{
    public abstract class TesteBase
    {
        #region Propriedades
        protected readonly Mock<ILancamentoService> LancamentoServiceMock;
        #endregion

        #region Construtor
        protected TesteBase()
        {
            this.LancamentoServiceMock = new Mock<ILancamentoService>();            
        }

        #endregion

        #region Mock Response

        #region SetupLancamentoResponse
        public static IEnumerable<LancamentoResponse> SetupLancamentoResponse()
        {
            return new List<LancamentoResponse>
            {
                new LancamentoResponse {
                    DataDoLancamento = DateTime.Now,
                    TipoLancamento = 0
                },
            };
        }
        #endregion
        
        #endregion

        #region Mock Request

        #region SetupLancamentoRequest
        public static IEnumerable<LancamentoRequest> SetupLancamentoRequest()
        {
            return new List<LancamentoRequest>
            {
                new LancamentoRequest {
                    DataDoLancamento = DateTime.Now,
                    TipoLancamento = 1
                },
            };
        }
        #endregion

        
        #endregion
    }
}