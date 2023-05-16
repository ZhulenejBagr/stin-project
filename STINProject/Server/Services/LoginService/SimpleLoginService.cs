using STINProject.Server.Services.PersistenceService;

namespace STINProject.Server.Services.LoginService
{
    public class SimpleLoginService : ILoginService
    {
        private readonly SessionStorage _storage;
        private readonly IPersistenceService _persistenceService;

        public SimpleLoginService(IPersistenceService persistenceService, SessionStorage storage)
        {
            _storage = storage;
            _persistenceService = persistenceService;
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

        public Guid CreateSession(string username)
        {
            var user = _persistenceService.GetUser(username);
            if (user is not null)
            {
                var session = new Session(user);
                _storage.Sessions.Add(session);
                return session.SessionId;
            }
            return Guid.Empty;
        }

        public bool CheckAndUpdateSession(Guid sessionId)
        {
            var session = _storage.Sessions.FirstOrDefault(s => s.SessionId == sessionId);
            if (session is not null)
            {
                if (session.Valid)
                {
                    session.ExpiresAt = DateTime.Now + TimeSpan.FromMinutes(5);
                    return true;
                }
            }
            return false;
        }
    }
}
