using AltitudeAccess.PresentationLayer.DTOs;
using AltitudeAccess.ServiceLayer.Models;

namespace AltitudeAccess.ServiceLayer.ServiceInterfaces
{
    public interface IRegistrationService
    {
        public User RegisterNewUser(RegistrationRequestDTO request);
    }
}
