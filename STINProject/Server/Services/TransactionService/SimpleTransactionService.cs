using STINProject.Server.Services.ExchangeRateService;
using STINProject.Server.Services.PersistenceService;
using STINProject.Server.Services.PersistenceService.Models;

namespace STINProject.Server.Services.TransactionService
{
    public class SimpleTransactionService : ITransactionService
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IPersistenceService _persistenceService;

        public SimpleTransactionService(IExchangeRateService exchangeRateService, IPersistenceService persistenceService)
        {
            _exchangeRateService = exchangeRateService;
            _persistenceService = persistenceService;
        }

        public bool TryExecuteTransaction(Guid userId, string currencyCode, double quantity)
        {
            if (!_exchangeRateService.IsCurrencySupported(currencyCode))
            {
                return false;
            }

            var userAccounts = _persistenceService.GetAccounts(userId);
            if (!userAccounts.Any()) 
            {
                return false;
            }
            var currencyMatch = userAccounts.Where(x => x.Currency == currencyCode).ToList();
            var currenyNonMatch = userAccounts.Where(x => x.Currency != currencyCode).ToList();

            if (quantity >= 0 && currencyMatch.Any())
            {
                var account = currencyMatch.First();
                var transaction = new Transaction { AccountID = account.AccountId, Date = DateTime.Now, Value = quantity };
                _persistenceService.AddTransaction(transaction);
                return true;
            }

            if (currencyMatch.Any())
            {
                foreach (var account in currencyMatch)
                {
                    if (account.Balance + quantity >= 0)
                    {
                        var transaction = new Transaction { AccountID = account.AccountId, Date = DateTime.Now, Value = quantity };
                        _persistenceService.AddTransaction(transaction);
                        return true;
                    }
                }
            }

            if (currenyNonMatch.Any())
            {
                foreach (var account in currenyNonMatch)
                {
                    var converted = _exchangeRateService.ExchangeCurrency(quantity, currencyCode, account.Currency);

                    if (account.Balance + converted >= 0)
                    {
                        var transaction = new Transaction { AccountID = account.AccountId, Date = DateTime.Now, Value = converted };
                        _persistenceService.AddTransaction(transaction);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
