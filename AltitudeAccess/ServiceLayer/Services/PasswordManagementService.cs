using AltitudeAccess.DataAccessLayer.DAOInterfaces;
using AltitudeAccess.ServiceLayer.Models;
using AltitudeAccess.ServiceLayer.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;

namespace AltitudeAccess.ServiceLayer.Services
{
    public class PasswordManagementService : IPasswordManagementService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IPasswordDAO _passwordDAO;
        public PasswordManagementService(IPasswordHasher<User> password, IPasswordDAO passwordDAO) 
        {
            _passwordHasher = password;
            _passwordDAO = passwordDAO;
        }

        public string HashPassword(User user, string password)
        {
            string HashedPassword = _passwordHasher.HashPassword(user, password);
            return HashedPassword;
        }
        public PasswordVerificationResult VerifyUserPassword(User user, string passwordHash, string providedPassword)
        {
            try
            {
                return _passwordHasher.VerifyHashedPassword(user, passwordHash, providedPassword);
            }
            catch (Exception)
            {
                return PasswordVerificationResult.Failed;
            }
            
        }
        // Fix Refresh!!!//
        public void RefreshPasswordHash(User user, string newHash)
        {
            _passwordDAO.UpdatePasswordHash(user.UserId, newHash);
        }
    }
}
