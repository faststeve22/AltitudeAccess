using AltitudeAccess.DataAccessLayer.DAOInterfaces;
using AltitudeAccess.PresentationLayer.DTOs;
using AltitudeAccess.ServiceLayer.Exceptions;
using AltitudeAccess.ServiceLayer.Models;
using AltitudeAccess.ServiceLayer.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace AltitudeAccess.ServiceLayer.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserDAO _userDAO;
        private readonly IPasswordManagementService _passwordManagementService;
        private readonly IPasswordDAO _passwordDAO;
        private readonly ITokenService _tokenService;
        public LoginService(IUserDAO userDAO, IPasswordManagementService passwordManagementService, IPasswordDAO passwordDAO, ITokenService tokenService)
        {
            _userDAO = userDAO;
            _passwordManagementService = passwordManagementService;
            _passwordDAO = passwordDAO;
            _tokenService = tokenService;
        }

        public string UserLogin(LoginRequestDTO login)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    User user = GetUser(login.Username);
                    string PasswordHash = GetPasswordHash(user.UserId);
                    VerifyPasswordHash(user, PasswordHash, login.Password);
                    string Token = IssueToken(user, Convert.ToInt32(login.ApplicationId));
                    scope.Complete();
                    return Token;
                }
                catch (Exception ex)
                {
                    throw new UserLoginException("Failed to complete login. Check username and password", ex);
                }
            }
        }

        public User GetUser(string username)
        {
            User user = _userDAO.GetUserByUsername(username);
            if (user == null)
            {
                throw new UserNotFoundException("Authentication Failed.");
            }
            return user;
        }

        public string GetPasswordHash(int userId)
        {
            string PasswordHash = _passwordDAO.GetPasswordHash(userId);
            if (PasswordHash == null)
            {
                throw new AuthenticationException("Authentication Failed");
            }
            return PasswordHash;
        }


        public void VerifyPasswordHash(User user, string passwordHash, string password)
        {
            PasswordVerificationResult result = _passwordManagementService.VerifyUserPassword(user, passwordHash, password);
            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                _passwordManagementService.RefreshPasswordHash(user, password);
            }
            if (result == PasswordVerificationResult.Failed)
            {
                throw new AuthenticationException("Incorrect username or password");
            }
        }

        public string IssueToken(User user, int applicationId)
        {
            string Token = _tokenService.GenerateToken(user, applicationId);
            if (Token == null)
            {
                throw new TokenException("Could not generate user token");
            }
            return Token;
        }
    }
}
