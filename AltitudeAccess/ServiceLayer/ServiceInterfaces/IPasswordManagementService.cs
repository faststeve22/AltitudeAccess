using AltitudeAccess.ServiceLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace AltitudeAccess.ServiceLayer.ServiceInterfaces
{
    public interface IPasswordManagementService
    {
        public PasswordVerificationResult VerifyUserPassword(User user, string passwordHash, string providedPassword);
        public string HashPassword(User user, string password);
        public void RefreshPasswordHash(User user, string newHash);
    }
}
