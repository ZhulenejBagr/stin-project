namespace STINProject.Server.Services.TransactionService
{
    public interface ITransactionService
    {
        public bool TryExecuteTransaction(Guid userId, string currencyCode, double quantity);
    }
}
