using AltitudeAccess.ServiceLayer.Models;

namespace AltitudeAccess.DataAccessLayer.DAOInterfaces
{
    public interface IRoleDAO
    {
        public Role GetUserRole(int roleId);
    }
}
