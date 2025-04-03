using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Infrastructure.Common.Configuration
{
    [ExcludeFromCodeCoverageAttribute]
    public class CacheConfiguration
    {
        public const string Cache = "CacheConfiguration";

        public int AbsoluteExpiration { get; set; } // valore em hora
        public int SlidingExpiration { get; set; } // valor em minuto
    }
}
