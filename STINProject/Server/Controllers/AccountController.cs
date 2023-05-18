using Microsoft.AspNetCore.Mvc;
using STINProject.Server.Services.PersistenceService;
using System.Diagnostics.CodeAnalysis;
using STINProject.Shared;
using STINProject.Server.Services.PersistenceService.Models;
using STINProject.Server.Services.TransactionService;

namespace STINProject.Server.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("Account")]
    public class AccountController
    {
        private readonly IPersistenceService _persistenceService;
        private readonly ITransactionService _transactionService;

        public AccountController(IPersistenceService persistenceService, ITransactionService transactionService) 
        {
            _persistenceService = persistenceService;
            _transactionService = transactionService;
        }

        [HttpGet("{userId}")]
        public IEnumerable<AccountModel> GetAccounts([FromRoute] Guid userId)
        {
            return _persistenceService.GetAccounts(userId)
                .Select(x => new AccountModel
                {
                    AccountId = x.AccountId,
                    Currency = x.Currency,
                    Balance = x.Balance,
                });
        }

        [HttpGet("Transactions/{accountId}")]
        public IEnumerable<TransactionViewModel> GetTransactions([FromRoute] Guid accountId)
        {
            return _persistenceService.GetTransactions(accountId)
                .Select(x => new TransactionViewModel
                {
                    TranscationId = x.TransactionID,
                    Date = x.Date,
                    Value = x.Value,
                });
        }

        [HttpGet("Transactions/AddTransaction/{userId}/{currencyCode}/{value}")]
        public bool AddTransaction([FromRoute] Guid userId, [FromRoute] string currencyCode, [FromRoute] string value)
        {
            var result = double.TryParse(value, out double parsedValue);
            if (!result)
            {
                return false;
            }
           
            return _transactionService.TryExecuteTransaction(userId, currencyCode, parsedValue);
        }
    }
}
