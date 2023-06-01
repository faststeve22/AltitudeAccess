using AltitudeAccess.PresentationLayer.DTOs;

namespace AltitudeAccess.ServiceLayer.Models
{
    public class User
    {
        public int UserId { get; private set; }
        public string Username { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }

        public User(int userId, string username, string firstName, string lastName, string emailAddress)
        {
            UserId = userId;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }

        public User(RegistrationRequestDTO request)
        {
            Username = request.UserName;
            FirstName = request.FirstName;
            LastName = request.LastName;
            EmailAddress = request.EmailAddress;
        }
    }
}
