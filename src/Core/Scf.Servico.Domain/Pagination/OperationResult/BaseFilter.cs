using Scf.Servico.Domain.Pagination.OperationResult.Interfaces.Inputs;
using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Domain.Pagination.OperationResult
{

    [ExcludeFromCodeCoverageAttribute]
    public class BaseFilter : IPagination
    {
        public int ItemsPerPage { get; set; } = 5;
        public int Page { get; set; }
        public bool CountTotal { get; set; }

        public OrdenationAttribute Ordenations { get; set; } = new OrdenationAttribute();
    }
}