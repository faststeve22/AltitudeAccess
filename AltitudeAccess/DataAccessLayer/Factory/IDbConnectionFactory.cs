using System.Data;

namespace AltitudeAccess.DataAccessLayer.Factory
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();

    }
}