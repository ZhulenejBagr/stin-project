using Microsoft.AspNetCore.Mvc;
using STINProject.Server.Services.ExchangeRateService;
using STINProject.Server.Services.LoginService;
using STINProject.Server.Services.PersistenceService;
using STINProject.Server.Services.TransactionService;
using STINProject.Shared;

namespace STINProject.Server.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class BankController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public BankController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet("Login/{username}/{password}")]
        public Guid Login([FromRoute] string username, [FromRoute] string password)
        {
            if (_loginService.CheckCredentials(username, password))
            {
                var session = _loginService.CreateSession(username);
                return session;
            }
            return Guid.Empty;
        }
    }
}