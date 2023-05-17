using STINProject.Shared;

namespace STINProject.Server.Services.LoginService
{
    public interface ILoginService
    {
        public bool CheckCredentials(string username, string password);
        public Session CreateSession(string username);
        public bool CheckAndUpdateSession(Guid sessionId);
        public bool VerifyTwoFactor(Guid sessionId, string code);
    }
}
