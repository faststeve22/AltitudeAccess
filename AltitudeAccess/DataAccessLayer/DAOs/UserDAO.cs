using AltitudeAccess.DataAccessLayer.DAOInterfaces;
using AltitudeAccess.DataAccessLayer.DatabaseEntities;
using AltitudeAccess.DataAccessLayer.Factory;
using AltitudeAccess.ServiceLayer.Models;
using System.Data;
using System.Data.Common;

namespace AltitudeAccess.DataAccessLayer.DAOs
{
    public class UserDAO : IUserDAO
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public UserDAO(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public User GetUserById(int userId)
        {
            try
            {
                using (IDbConnection conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    IDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "Select user_id, username, first_name, last_name, email_address, role_id from DbUser WHERE user_id = @userId";
                    AddParameter(cmd, "@userId", userId);
                    IDataReader reader = cmd.ExecuteReader();
                    DbUser user = userReader(reader);
                    return ConvertToDomainUserModel(user);
                }
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsUserNameAvailable(string username)
        {
            using (IDbConnection conn = _connectionFactory.CreateConnection())
            {
                conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select username from DbUser WHERE username = @username";
                AddParameter(cmd, "@username", username);
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return false;
                }
                return true;
            }
        }

        public User GetUserByUsername(string username)
        {
            using (IDbConnection conn = _connectionFactory.CreateConnection())
            {
                conn.Open();
                IDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select user_id, username, first_name, last_name, email_address from DbUser WHERE username = @username";
                AddParameter(cmd, "@username", username);
                IDataReader reader = cmd.ExecuteReader();
                DbUser userDBModel = userReader(reader);
                return ConvertToDomainUserModel(userDBModel);
            }
        }

        public User AddUser(User user)
        {
            try
            {
                using (IDbConnection conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    IDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "INSERT into DbUser (username, first_name, last_name, email_address) OUTPUT INSERTED.user_id, INSERTED.username, INSERTED.first_name, INSERTED.last_name, INSERTED.email_address VALUES (@username, @firstName, @lastName, @emailAddress)";
                    AddParameter(cmd, "@username", user.Username);
                    AddParameter(cmd, "@firstName", user.FirstName);
                    AddParameter(cmd, "@lastName", user.LastName);
                    AddParameter(cmd, "@emailAddress", user.EmailAddress);

                    IDataReader reader = cmd.ExecuteReader();
                    DbUser userDBModel = userReader(reader);
                    return ConvertToDomainUserModel(userDBModel);
                }
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string UpdatePasswordHash(User user, string passwordHash)
        {
            try
            {
                using (IDbConnection conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    IDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "DECLARE @UpdatedRows TABLE (password_hash VARCHAR(MAX)) UPDATE DbUser SET password_hash = @passwordHash OUTPUT INSERTED.password_hash INTO @UpdatedRows WHERE user_id = @userId SELECT password_hash FROM @UpdatedRows";
                    AddParameter(cmd, "@userId", user.UserId);
                    AddParameter(cmd, "@passwordHash", passwordHash);
                    string updatedPasswordHash = cmd.ExecuteScalar() as string;
                    return updatedPasswordHash;
                }
            }
            catch (DbException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DbUser userReader(IDataReader reader)
        {
            try
            {
                if (reader.Read())
                {
                    int UserId = Convert.ToInt32(reader["user_id"]);
                    string Username = Convert.ToString(reader["username"]);
                    string FirstName = Convert.ToString(reader["first_name"]);
                    string LastName = Convert.ToString(reader["last_name"]);
                    string EmailAddress = Convert.ToString(reader["email_address"]);
                    DbUser user = new DbUser(UserId, Username, FirstName, LastName, EmailAddress);
                    return user;
                }
                else
                {
                    DbUser user = null;
                    return user;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User ConvertToDomainUserModel(DbUser userDBModel)
        {
            if (userDBModel != null)
            {
                int UserId = userDBModel.UserId;
                string Username = userDBModel.Username;
                string FirstName = userDBModel.FirstName;
                string LastName = userDBModel.LastName;
                string EmailAddress = userDBModel.EmailAddress;
                User user = new User(UserId, Username, FirstName, LastName, EmailAddress);
                return user;
            }
            else
            {
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

    }
}
