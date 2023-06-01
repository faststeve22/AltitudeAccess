using AltitudeAccess.DataAccessLayer.DAOInterfaces;
using AltitudeAccess.DataAccessLayer.DatabaseEntities;
using AltitudeAccess.DataAccessLayer.Factory;
using AltitudeAccess.ServiceLayer.Models;
using System.Data;

namespace AltitudeAccess.DataAccessLayer.DAOs
{
    public class UserApplicationRoleDAO : IUserApplicationRoleDAO
    {
        public readonly IDbConnectionFactory _connectionFactory;
        public UserApplicationRoleDAO(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public int AddUserApplicationRole(UserApplicationRole userApplicationRole)
        {
            using (IDbConnection conn = _connectionFactory.CreateConnection())
            {
                conn.Open();

                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO UserApplicationRole (user_id, role_id, application_id) OUTPUT INSERTED.user_application_role_id, INSERTED.user_id, INSERTED.role_id, INSERTED.application_id VALUES (@userId, @roleId, @applicationId)";
                AddParameter(cmd, "@userId", userApplicationRole.UserId);
                AddParameter(cmd, "@roleId", userApplicationRole.RoleId);
                AddParameter(cmd, "@applicationId", userApplicationRole.ApplicationId);
                return cmd.ExecuteNonQuery();
            }
        }


        public UserApplicationRole GetUserApplicationRole(User user, int applicationId)
        {
                using (IDbConnection conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    IDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT user_application_role_id, role_id, application_id, user_id from UserApplicationRole WHERE user_id = @userId AND application_id = @applicationId";
                    AddParameter(cmd, "@userId", user.UserId);
                    AddParameter(cmd, "@applicationId", applicationId);
                    IDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int UserApplicationRoleId = Convert.ToInt32(reader["user_application_role_id"]);
                        int UserId = Convert.ToInt32(reader["user_id"]);
                        int RoleId = Convert.ToInt32(reader["role_id"]);
                        int ApplicationId = Convert.ToInt32(reader["application_id"]);
                        DbUserApplicationRole ApplicationRole = new DbUserApplicationRole(UserApplicationRoleId, UserId, RoleId, ApplicationId);
                        return new UserApplicationRole(ApplicationRole.UserApplicationRoleId, ApplicationRole.UserId, ApplicationRole.RoleId, ApplicationRole.ApplicationId);
                    }
                    return null;

                }
        }

        private static void AddParameter(IDbCommand cmd, string paramName,  object value)
        {
            var Parameter = cmd.CreateParameter();
            Parameter.ParameterName = paramName;
            Parameter.Value = value;
            cmd.Parameters.Add(Parameter);
        }
    }
}
