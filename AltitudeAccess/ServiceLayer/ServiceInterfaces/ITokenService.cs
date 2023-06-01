using AltitudeAccess.ServiceLayer.Models;

namespace AltitudeAccess.ServiceLayer.ServiceInterfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user, int applicationId);
    }
}
