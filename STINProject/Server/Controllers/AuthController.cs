using Microsoft.AspNetCore.Mvc;
using STINProject.Server.Services.LoginService;
using STINProject.Shared;
using System.Diagnostics.CodeAnalysis;

namespace STINProject.Server.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("Auth")]
    public class BankController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly TwoFactorService _twoFactorService;
        public BankController(ILoginService loginService, TwoFactorService twoFactorService)
        {
            _loginService = loginService;
            _twoFactorService = twoFactorService;
        }

        [HttpGet("Login/{username}/{password}")]
        public Session Login([FromRoute] string username, [FromRoute] string password)
        {
            if (_loginService.CheckCredentials(username, password))
            {
                var session = _loginService.CreateSession(username);
                return session;
            }
            return new Session();
        }

        [HttpGet("Login/TwoFactor/Setup")]
        public SetupCodeWrapper GetSetupCode()
        {
            var code = _twoFactorService.GetSetupCode();
            return new SetupCodeWrapper() { QRCode = code.QrCodeSetupImageUrl, ManualCode = code.ManualEntryKey };
        }

        [HttpGet("Session/{sessionId}/{code}")]
        public bool VerifyCode([FromRoute] Guid sessionId, [FromRoute] string code)
        {
            return _loginService.VerifyTwoFactor(sessionId, code);
        }

        [HttpGet("Session/{sessionId}")]
        public bool VerifySession([FromRoute] Guid sessionId)
        {
            return _loginService.CheckAndUpdateSession(sessionId);
        }
    }
}