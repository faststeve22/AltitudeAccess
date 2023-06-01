using AltitudeAccess.PresentationLayer.DTOs;

namespace AltitudeAccess.ServiceLayer.Models
{
    public class Role
    {
        public int RoleId { get; private set; }
        public string RoleName { get; private set; }
        public Role(int roleId, string roleName)
        {
            RoleId = roleId;
            RoleName = roleName;
        }
    }
}
