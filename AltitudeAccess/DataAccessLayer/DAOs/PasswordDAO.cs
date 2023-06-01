using AltitudeAccess.DataAccessLayer.DAOInterfaces;
using AltitudeAccess.DataAccessLayer.Factory;
using System.Data;

namespace AltitudeAccess.DataAccessLayer.DAOs
{
    public class PasswordDAO : IPasswordDAO
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public PasswordDAO(IDbConnectionFactory connectionFactory) 
        {
            _connectionFactory = connectionFactory;
        }

        public int AddPassword(int userId, string passwordHash)
        {
            using (IDbConnection conn = _connectionFactory.CreateConnection())
            {
                conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT into Password (user_id, password_hash) VALUES (@userId, @passwordHash)";
                AddParameter(cmd, "@userId", userId);
                AddParameter(cmd, "@passwordHash", passwordHash);
                return cmd.ExecuteNonQuery();       
            }
        }

        public string GetPasswordHash(int userId)
        {
                using (IDbConnection conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    IDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT password_hash FROM Password WHERE user_id = @userId";
                    AddParameter(cmd, "@userId", userId);
                    IDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return Convert.ToString(reader["password_hash"]);
                    }
                    return null;
                }
        }

        public int UpdatePasswordHash(int userId, string passwordHash)
        {
                using (IDbConnection conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    IDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "UPDATE Password SET password_hash = @passwordHash WHERE user_id = @userId";
                    AddParameter(cmd, "@userId", userId);
                    AddParameter(cmd, "@passwordHash", passwordHash);
                    return cmd.ExecuteNonQuery();
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
