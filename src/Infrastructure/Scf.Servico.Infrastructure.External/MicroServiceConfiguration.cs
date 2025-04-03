namespace Scf.Servico.Infrastructure.External
{
    public abstract class MicroServiceConfiguration
    {
        public string UrlService { get; set; } = string.Empty;
        public string TokenSSO { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public DateTime DataToken { get; set; }
        public int TokenExpiresIn { get; set; }
        public string? Token { get; set; } = string.Empty;
    }
}
