using AltitudeAccess.DataAccessLayer.DAOInterfaces;
using AltitudeAccess.DataAccessLayer.Factory;

namespace AltitudeAccess.DataAccessLayer.DAOs
{
    public class ApplicationDAO : IApplicationDAO
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public ApplicationDAO(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        // public int AddApplication()
       
        // public int DeleteApplication()

        // public int RenameApplication()
    }
}
