using LibraryManagement.Commons;
using LibraryManagement.DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement.DAOs {
    public class AuthenManager : Manager {

        public User Login(string EmailInput, string PasswordInput) {
            User result = null;

            string sql = "select UserID, RoleID, UserName, Email, Status from Users " +
                "where Email = @Email and Password = HASHBYTES('SHA2_256', @Password)";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@Email", EmailInput);
            cmd.Parameters.AddWithValue("@Password", PasswordInput);

            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) {
                    int UserID = reader.GetInt32(0);
                    int RoleID = reader.GetInt32(1);
                    string UserName = reader.GetString(2);
                    string Email = reader.GetString(3);
                    string Password = "••••••••";
                    bool Status = reader.GetBoolean(4);

                    result = new User(UserID, RoleID, UserName, Email, Password, Status);
                }

            } catch (SqlException se) {
                throw new Exception(se.Message);
            } finally {
                cnn.Close();
            }

            return result;
        }

        public User Register(User user) {
            User result = null;

            string sql = "insert into Users(UserName, Email, Password) " +
                "values(@UserName, @Email, HASHBYTES('SHA2_256', @Password))";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);

            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }

                bool isSuccess = cmd.ExecuteNonQuery() > 0;
                if (isSuccess) {
                    UserManager userManager = new UserManager();
                    result = userManager.GetUser(user.Email);
                }
            } catch (SqlException se) {
                throw new Exception(se.Message);
            }

            return result;
        }
    }
}
