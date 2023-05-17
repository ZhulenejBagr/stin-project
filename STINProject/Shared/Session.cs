namespace STINProject.Shared
{
    public class Session
    {
        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
        public bool TwoFactorCompleted { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool Expired => DateTime.Now >= ExpiresAt;
        public bool Valid => TwoFactorCompleted && !Expired;

        public Session(Guid userId)
        {
            SessionId = Guid.NewGuid();
            ExpiresAt = DateTime.Now + TimeSpan.FromMinutes(10);
            UserId = userId;
            TwoFactorCompleted = false;
        }

        public Session()
        {
            SessionId = Guid.Empty;
            ExpiresAt = DateTime.MinValue;
            TwoFactorCompleted = false;
            UserId = Guid.Empty;
        }
    }
}
