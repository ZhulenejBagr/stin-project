namespace STINProject.Server.Services.LoginService
{
    public interface ILoginService
    {
        public bool CheckCredentials(string username, string password);
        public Guid CreateSession(string username);
        public bool CheckAndUpdateSession(Guid sessionId);
    }
}
