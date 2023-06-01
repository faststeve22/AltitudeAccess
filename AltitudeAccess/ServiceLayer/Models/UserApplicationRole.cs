using AltitudeAccess.PresentationLayer.DTOs;

namespace AltitudeAccess.ServiceLayer.Models
{
    public class UserApplicationRole
    {
        public int UserApplicationRoleId { get; private set; }
        public int UserId { get; private set; }
        public int RoleId { get; private set; } = 1;
        public int ApplicationId { get; private set; }


        public UserApplicationRole(int userApplicationRole, int userId, int roleId, int applicationid)
        {
            UserApplicationRoleId = userApplicationRole;
            UserId = userId;
            RoleId = roleId;
            ApplicationId = applicationid;
        }

        public UserApplicationRole(RegistrationRequestDTO request, User user)
        {
            UserId = user.UserId;
            ApplicationId = request.ApplicationId;
        }
    }
}
