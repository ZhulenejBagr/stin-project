namespace STINProject_API.Services.PersistenceService.Model
{
    public class Account
    {
        public Guid AccountId { get; private set; }
        public Guid OwnerId { get; private set; }

        // TODO create list of currencies
        // TODO create currency object
        public string Currency { get; private set; }
        // TODO exclude incorrect values
        public double Balance { get; set; }

        public Account(Guid ownerId, string currency)
        {
            AccountId = new Guid();
            OwnerId = ownerId;
            Currency = currency;
            Balance = 0;
        }
    }
}
