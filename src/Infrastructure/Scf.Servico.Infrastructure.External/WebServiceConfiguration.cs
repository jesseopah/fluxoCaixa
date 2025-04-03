namespace Scf.Servico.Infrastructure.External
{
    public abstract class WebServiceConfiguration
    {
        public string UrlService { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int? MaxBufferSize { get; set; } // Valor opcional
        public int? MaxReceivedMessageSize { get; set; } // Valor opcional
        public int? Timeout { get; set; } // Valor opcional
    }
}
