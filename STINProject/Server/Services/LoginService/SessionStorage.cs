using STINProject.Shared;
using System.Diagnostics.CodeAnalysis;

namespace STINProject.Server.Services.LoginService
{
    [ExcludeFromCodeCoverage]
    public class SessionStorage
    {
        public ICollection<Session> Sessions { get; set; }
        public SessionStorage() 
        { 
            Sessions = new List<Session>();
        }
    }
}
