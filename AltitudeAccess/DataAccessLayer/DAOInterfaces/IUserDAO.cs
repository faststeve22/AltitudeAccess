using AltitudeAccess.DataAccessLayer.DatabaseEntities;
using AltitudeAccess.ServiceLayer.Models;
using System.Data;

namespace AltitudeAccess.DataAccessLayer.DAOInterfaces
{
    public interface IUserDAO
    {
        public User GetUserByUsername(string username);
        public User GetUserById(int userId);
        public User AddUser(User newUser);
        public bool IsUserNameAvailable(string userName);
        public string UpdatePasswordHash(User user, string passwordHash);
    }
}
