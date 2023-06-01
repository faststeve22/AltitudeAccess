using AltitudeAccess.PresentationLayer.DTOs;

namespace AltitudeAccess.ServiceLayer.Models
{
    public class Application
    {
        public int ApplicationId { get; private set; }
        public string ApplicationName { get; private set; }


        public Application(int applicationId, string applicationName)
        {
            ApplicationId = applicationId;
            ApplicationName = applicationName;
        }

        public Application(RegistrationRequestDTO request)
        {
            ApplicationId = request.ApplicationId;
        }
    }
}
