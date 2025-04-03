using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Scf.Servico.Domain.Pagination.OperationResult
{

    [ExcludeFromCodeCoverageAttribute]
    public class OrdenationAttribute
    {
        public string? PropertyName { get; set; } 

        public ListSortDirection Direction { get; set; }
    }
}