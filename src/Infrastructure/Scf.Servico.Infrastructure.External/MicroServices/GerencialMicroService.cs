using Microsoft.Extensions.Options;
using Scf.Servico.Domain.Interfaces.MicroServices;
using Scf.Servico.Infrastructure.External.Configurations;

namespace Scf.Servico.Infrastructure.External.MicroServices
{
    public class GerencialMicroService : ServiceBase, IGerencialMicroService
    {
        private bool disposedValue;

        public GerencialMicroService(IOptions<GerencialMicroServiceConfiguration> msConfig) : base(msConfig.Value) { }

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        ~GerencialMicroService()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
