namespace Scf.Servico.Domain.Pagination.OperationResult.Interfaces.Results
{
    public interface IOperationResult<T> : IOperationResultBase
    {
        T Data { get; set; }

        new IOperationResult<T> AddMessage(string message);

        new IOperationResult<T> AddMessages(IEnumerable<string> messages);
    }
}