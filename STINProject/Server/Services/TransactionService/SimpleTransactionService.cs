using STINProject.Server.Services.ExchangeRateService;
using STINProject.Server.Services.PersistenceService;
using STINProject.Server.Services.PersistenceService.Models;

namespace STINProject.Server.Services.TransactionService
{
    public class SimpleTransactionService : ITransactionService
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IPersistenceService _persistenceService;
        private readonly double accountLimitThreshold = 0.9;

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

            // user has matching account and its a deposit
            if (quantity >= 0 && currencyMatch.Any())
            {
                var account = currencyMatch.First();
                var transaction = new Transaction { AccountID = account.AccountId, Date = DateTime.Now, Value = quantity };
                _persistenceService.AddTransaction(transaction);
                return true;
            }

            // user has matching account and its a withdrawal
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

                    else if (quantity < 0 && account.Balance > 0 && account.Balance + quantity * accountLimitThreshold >= 0)
                    {
                        var transaction = new Transaction { AccountID = account.AccountId, Date = DateTime.Now, Value = quantity };
                        var additional = new Transaction { AccountID = account.AccountId, Date = DateTime.Now, Value = (account.Balance + quantity) * 0.1 };
                        _persistenceService.AddTransaction(transaction);
                        _persistenceService.AddTransaction(additional);
                        return true;
                    }
                }
            }

            // user has non matching account
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

                    else if (quantity < 0 && account.Balance > 0 && account.Balance + quantity * accountLimitThreshold >= 0)
                    {
                        var transaction = new Transaction { AccountID = account.AccountId, Date = DateTime.Now, Value = converted };
                        var additional = new Transaction { AccountID = account.AccountId, Date = DateTime.Now, Value = (account.Balance + quantity) * 0.1 };
                        _persistenceService.AddTransaction(transaction);
                        _persistenceService.AddTransaction(additional);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
