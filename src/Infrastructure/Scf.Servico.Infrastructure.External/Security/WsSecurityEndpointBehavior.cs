using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Scf.Servico.Infrastructure.External.Security
{
	public class WsSecurityEndpointBehavior : IEndpointBehavior
	{
		private readonly string password;
		private readonly string username;

		public WsSecurityEndpointBehavior(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime) =>
			clientRuntime.ClientMessageInspectors.Add(new WsSecurityMessageInspector(username, password));

		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) 
		{
            // Method intentionally left empty.
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) 
		{
            // Method intentionally left empty.
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint) 
		{
            // Method intentionally left empty.
        }
    }
}
