namespace STINProject_API.Services.PersistenceService.Model
{
    public class User
    {
        public Guid UserId { get; private set; }

        public string Username { get; private set; }

        // TODO hash password
        public string Password { get; private set; }

        // TODO verify if email address is valid
        public string Email { get; private set; }

        public User(string username, string password, string email)
        {
            UserId = new Guid();
            Username = username;
            Password = password;
            Email = email;
        }
    }
}
