using AltitudeAccess.ServiceLayer.Models;

namespace AltitudeAccess.DataAccessLayer.DAOInterfaces
{
    public interface IUserApplicationRoleDAO
    {
        int AddUserApplicationRole(UserApplicationRole userApplicationRole);
        UserApplicationRole GetUserApplicationRole(User user, int applicationId);
    }
}
