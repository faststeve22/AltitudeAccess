
namespace AltitudeAccess.PresentationLayer.DTOs
{
    public class RegistrationRequestDTO 
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public int ApplicationId { get; private set; }


        public RegistrationRequestDTO(string userName, string password, string firstName, string lastName, string emailAddress, int applicationId)
        {
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            ApplicationId = applicationId;  
        }
    }
}
