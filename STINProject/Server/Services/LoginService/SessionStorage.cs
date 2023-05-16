namespace STINProject.Server.Services.LoginService
{
    public class SessionStorage
    {
        public ICollection<Session> Sessions { get; set; }
        public SessionStorage() 
        { 
            Sessions = new List<Session>();
        }
    }
}
