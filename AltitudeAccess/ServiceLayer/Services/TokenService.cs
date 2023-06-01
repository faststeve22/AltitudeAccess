using AltitudeAccess.ServiceLayer.ServiceInterfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AltitudeAccess.DataAccessLayer.DAOInterfaces;
using AltitudeAccess.ServiceLayer.Models;

namespace AltitudeAccess.ServiceLayer.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _jwtSecret;
        private readonly int _jwtExpiration;
        private readonly IUserApplicationRoleDAO _userApplicationRoleDAO;

        public TokenService(string jwtSecret, int jwtExpiration, IUserApplicationRoleDAO userApplicationRoleDAO)
        {
            _jwtSecret = jwtSecret;
            _jwtExpiration = jwtExpiration;
            _userApplicationRoleDAO = userApplicationRoleDAO;

        }

        public string GenerateToken(User user, int applicationId)
        {
            UserApplicationRole ApplicationRole = GetUserApplicationRole(user, applicationId);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, ApplicationRole.RoleId.ToString()),
                    new Claim(ClaimTypes.Email, user.EmailAddress),
                    new Claim("aud", "https://api.blueskies.logbook.com")
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "https://api.altitudeaccess.com",
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public UserApplicationRole GetUserApplicationRole(User user, int applicationId)
        {
            try
            {
                return _userApplicationRoleDAO.GetUserApplicationRole(user, applicationId);
            }
            catch (Exception)
            {
                throw new Exception("Error accessing user application role.");
            }

        }
    }
}
