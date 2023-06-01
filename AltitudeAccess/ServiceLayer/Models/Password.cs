namespace AltitudeAccess.ServiceLayer.Models
{
    public class Password
    {
        public string PasswordHash { get; private set; }

        public Password(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}
