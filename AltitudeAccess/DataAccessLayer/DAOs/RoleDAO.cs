using AltitudeAccess.DataAccessLayer.DAOInterfaces;
using AltitudeAccess.DataAccessLayer.DatabaseEntities;
using AltitudeAccess.DataAccessLayer.Factory;
using AltitudeAccess.ServiceLayer.Models;
using System.Data;

namespace AltitudeAccess.DataAccessLayer.DAOs
{
    public class RoleDAO : IRoleDAO
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public RoleDAO(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Role GetUserRole(int roleId)
        {
            using (IDbConnection conn = _connectionFactory.CreateConnection())
            {
                conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select role_id, role_name from UserRoles WHERE role_id = @roleId";
                AddParameter(cmd, "@roleId", roleId);
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int RoleId = Convert.ToInt32(reader["role_id"]);
                    string RoleName = Convert.ToString(reader["role_name"]);
                    DbRole RetrievedRole = new DbRole(RoleId, RoleName);
                    return new Role(RetrievedRole.RoleId, RetrievedRole.RoleName);
                }
                return null;
            }
        }

        private static void AddParameter(IDbCommand cmd, string paramName, object value)
        {
            var Parameter = cmd.CreateParameter();
            Parameter.ParameterName = paramName; 
            Parameter.Value = value;
            cmd.Parameters.Add(Parameter);
        }

        /* public int AddUserRole(string roleNmae)
        {
            UserRoles roles = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into UserRoles (role_name) OUTPUT INSERTED role_id Values (@roleName)", conn);
                cmd.Parameters.AddWithValue("@roleName", roleName);
                int insertedRoleId = (int)cmd.ExecuteScalar();
                return insertedRoleId;
            }
        } 

        public bool RemoveUserRole(int roleName)
        {
            UserRoles roles = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM UserRoles WHERE roleName = @roleName", conn);
                cmd.Parameters.AddWithValue("@roleName", roleName);
                int RowsAffected = cmd.ExecuteNonQuery();
                return RowsAffected > 0;
            }
        }  */
    }
}
