namespace Scf.Servico.Infrastructure.External.Configurations
{
    public class SecurityServiceWebServiceConfiguration : WebServiceConfiguration
    {
        public int ExpirationInSeconds { get; set; }  = 86400;
    }
}
