
namespace AltitudeAccess.DataAccessLayer.DAOInterfaces
{
    public interface IPasswordDAO
    {
        public int AddPassword(int userId, string passwordHash);
        public string? GetPasswordHash(int userId);
        public int UpdatePasswordHash(int userId, string passwordHash);
    }
}
