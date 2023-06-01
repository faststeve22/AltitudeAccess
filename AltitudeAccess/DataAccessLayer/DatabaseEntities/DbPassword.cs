namespace AltitudeAccess.DataAccessLayer.DatabaseEntities
{
    public class DbPassword
    {
        public int PasswordId { get; private set; }
        public int UserId { get; private set; }
        public string PasswordHash { get; private set; }

        public DbPassword(int passwordId, int userId, string passwordHash) 
        { 
            PasswordId = passwordId;
            UserId = userId;
            PasswordHash = passwordHash;
        }
    }
}
