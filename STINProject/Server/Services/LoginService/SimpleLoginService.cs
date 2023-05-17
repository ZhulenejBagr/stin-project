using Google.Authenticator;
using STINProject.Server.Services.PersistenceService;
using STINProject.Shared;
using System.Text;

namespace STINProject.Server.Services.LoginService
{
    public class SimpleLoginService : ILoginService
    {
        private readonly SessionStorage _storage;
        private readonly IPersistenceService _persistenceService;
        private readonly TwoFactorService _twoFactorService;

        public SimpleLoginService(IPersistenceService persistenceService, SessionStorage storage, TwoFactorService twoFactorService)
        {
            _storage = storage;
            _persistenceService = persistenceService;
            _twoFactorService = twoFactorService;
        }

        public bool CheckCredentials(string username, string password)
        {
            var user = _persistenceService.GetUser(username);
            if (user is null)
            {
                return false;
            }
            return user.Password == password;
        }

        public Session CreateSession(string username)
        {
            var user = _persistenceService.GetUser(username);
            if (user is not null)
            {
                var session = new Session(user.UserId);
                _storage.Sessions.Add(session);
                return session;
            }
            return new Session();
        }

        public bool CheckAndUpdateSession(Guid sessionId)
        {
            var session = _storage.Sessions.FirstOrDefault(s => s.SessionId == sessionId);
            if (session is not null)
            {
                if (!session.Expired)
                {
                    session.ExpiresAt = DateTime.Now + TimeSpan.FromMinutes(5);
                    return true;
                }
            }
            return false;
        }

        public bool VerifyTwoFactor(Guid sessionId, string code)
        {
            var session = _storage.Sessions.FirstOrDefault(s => s.SessionId == sessionId);
            if (session is not null)
            {
                var result = _twoFactorService.VerifyCode(code);
                session.TwoFactorCompleted = result;
                return result;
            }
            return false;
        }
    }
}
