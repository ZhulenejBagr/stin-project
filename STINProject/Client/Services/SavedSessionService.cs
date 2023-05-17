using STINProject.Shared;
using System.Net.Http.Json;

namespace STINProject.Client.Services
{
    public class SavedSessionService
    {
        private readonly HttpClient _httpClient;
        public Session Session { get; set; }
        public SavedSessionService(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task<bool> VerifySession()
        {
            var result = await _httpClient.GetFromJsonAsync<bool>($"Auth/Session/{Session.SessionId}");
            return result;
        }
    }
}
