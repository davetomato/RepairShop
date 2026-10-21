using System;
using System.Collections.Generic;
using FixFlow.Models;
using Microsoft.Data.SqlClient;

namespace FixFlow.Data
{
    public class UserRepository
    {
        private const string SelectUsers =
            "SELECT UserId, Username, PasswordHash, Role FROM dbo.Users ";

        private static User Read(SqlDataReader reader)
        {
            UserRole role = (UserRole)Enum.Parse(typeof(UserRole), reader.GetString(3));
            return User.Create(role, reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
        }

        private List<User> Query(string tail, Action<SqlCommand> addParameters)
        {
            List<User> users = new List<User>();
            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(SelectUsers + tail, conn))
            {
                if (addParameters != null)
                    addParameters(cmd);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        users.Add(Read(reader));
                }
            }
            return users;
        }

        public List<User> GetAll()
        {
            return Query("ORDER BY Username", null);
        }

        public List<User> GetByRole(UserRole role)
        {
            return Query(
                "WHERE Role = @Role ORDER BY Username",
                cmd => cmd.Parameters.AddWithValue("@Role", role.ToString()));
        }

        public User GetById(int userId)
        {
            List<User> result = Query(
                "WHERE UserId = @UserId",
                cmd => cmd.Parameters.AddWithValue("@UserId", userId));
            return result.Count > 0 ? result[0] : null;
        }

        public User GetByUsername(string username)
        {
            List<User> result = Query(
                "WHERE Username = @Username",
                cmd => cmd.Parameters.AddWithValue("@Username", username ?? string.Empty));
            return result.Count > 0 ? result[0] : null;
        }

        public void Add(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO dbo.Users (Username, PasswordHash, Role) " +
                    "OUTPUT INSERTED.UserId " +
                    "VALUES (@Username, @PasswordHash, @Role)", conn))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@Role", user.Role.ToString());
                    conn.Open();
                    user.UserId = (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                if (DbHelper.IsDuplicateError(ex))
                    throw new InvalidOperationException("That username is already taken.");
                throw;
            }
        }

        public void UpdateDetails(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE dbo.Users SET Username = @Username, Role = @Role WHERE UserId = @UserId", conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", user.UserId);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Role", user.Role.ToString());
                    conn.Open();
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("User not found.");
                }
            }
            catch (SqlException ex)
            {
                if (DbHelper.IsDuplicateError(ex))
                    throw new InvalidOperationException("That username is already taken.");
                throw;
            }
        }

        public void UpdatePassword(int userId, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash cannot be empty.");

            using (SqlConnection conn = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE dbo.Users SET PasswordHash = @PasswordHash WHERE UserId = @UserId", conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                conn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException("User not found.");
            }
        }

        public void Delete(int userId)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM dbo.Users WHERE UserId = @UserId", conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                if (DbHelper.IsForeignKeyError(ex))
                    throw new InvalidOperationException(
                        "This user is assigned to repair orders, so the user cannot be deleted.");
                throw;
            }
        }
    }
}
