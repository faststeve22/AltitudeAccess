namespace AltitudeAccess.DataAccessLayer.DatabaseEntities
{
    public class DbUser
    {
        public int UserId { get; private set; }
        public string Username { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }

        public DbUser(int userId, string username, string firstName, string lastName, string emailAddress)
        {
            UserId = userId;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }
    }
}
