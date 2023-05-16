using STINProject.Server.Services.PersistenceService.Models;

namespace STINProject.Server.Services.LoginService
{
    public class Session
    {
        public Guid SessionId { get; set; }
        public User User { get; set; }

        public DateTime ExpiresAt { get; set; }
        public bool Valid => DateTime.Now < ExpiresAt;

        public Session(User user) 
        {
            SessionId = Guid.NewGuid();
            ExpiresAt = DateTime.Now + TimeSpan.FromMinutes(10);
            User = user;
        }
    }
}
