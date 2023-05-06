namespace STINProject_API.Services.PersistenceService.Models
{
    public class Transaction
    {
        public Guid TransactionID { get; private set; }
        public Guid AccountID { get; private set; }
        public double Value { get; private set; }
        public DateTime Date { get; private set; }

        public Transaction(Guid accountID, double value, DateTime date)
        {
            TransactionID = new Guid();
            AccountID = accountID;
            Value = value;
            Date = date;
        }
    }
}
