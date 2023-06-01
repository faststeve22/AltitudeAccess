using AltitudeAccess.PresentationLayer.DTOs;

namespace AltitudeAccess.ServiceLayer.ServiceInterfaces
{
    public interface ILoginService
    {
        public string UserLogin(LoginRequestDTO login);
    }
}
