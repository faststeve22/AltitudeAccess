namespace AltitudeAccess.DataAccessLayer.DatabaseEntities
{
    public class DbUserApplicationRole
    {   public int UserApplicationRoleId { get; private set; }
        public int UserId {  get; private set; }
        public int RoleId { get; private set; }
        public int ApplicationId { get; private set; }
        public DbUserApplicationRole(int userApplicationRoleId, int userId, int roleId, int applicationId) 
        {
            UserApplicationRoleId = userApplicationRoleId;
            UserId = userId;
            RoleId = roleId;
            ApplicationId = applicationId;
        }
    }
}
