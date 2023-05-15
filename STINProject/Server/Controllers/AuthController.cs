using Microsoft.AspNetCore.Mvc;
using STINProject.Server.Services.ExchangeRateService;
using STINProject.Server.Services.PersistenceService;
using STINProject.Server.Services.TransactionService;

namespace STINProject.Server.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class BankController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IPersistenceService _persistenceService;
        private readonly ITransactionService _transactionService;

        public BankController(IExchangeRateService exchangeRateService, IPersistenceService persistenceService, ITransactionService transactionService)
        {
            _exchangeRateService = exchangeRateService;
            _persistenceService = persistenceService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public string Get()
        {
            return "test";
        }
    }
}