using Google.Authenticator;
using System.Text;

namespace STINProject.Server.Services.LoginService
{
    public class TwoFactorService
    {
        private static readonly byte[] _sskey = Encoding.UTF8.GetBytes("la biblioteka");
        private readonly string _appName = "Banking app";
        private readonly TwoFactorAuthenticator _authenticator;
        public TwoFactorService() 
        { 
            _authenticator = new TwoFactorAuthenticator();
        }

        public SetupCode GetSetupCode()
        {
            return _authenticator.GenerateSetupCode(_appName, "description", _sskey);
        }

        public bool VerifyCode(string code)
        {
            return _authenticator.ValidateTwoFactorPIN(_sskey, code);
        }
    }
}
