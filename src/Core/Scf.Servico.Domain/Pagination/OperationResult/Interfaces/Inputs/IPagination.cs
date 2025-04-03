namespace Scf.Servico.Domain.Pagination.OperationResult.Interfaces.Inputs
{
    public interface IPagination : IOrderable
    {
        int ItemsPerPage { get; set; }
        int Page { get; set; }
        bool CountTotal { get; set; }
    }
}