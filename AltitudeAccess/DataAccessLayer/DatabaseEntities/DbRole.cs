namespace AltitudeAccess.DataAccessLayer.DatabaseEntities
{
    public class DbRole
    {
        public int RoleId { get; private set; }
        public string RoleName { get; private set; }
        public DbRole(int roleId, string roleName)
        {
            RoleId = roleId;
            RoleName = roleName;
        }
    }
}
