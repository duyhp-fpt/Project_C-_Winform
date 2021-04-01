using LibraryManagement.Commons;
using LibraryManagement.DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement.DAOs {
    public class UserManager: Manager {
        
        public DataTable GetUsers() {
            DataTable dtUser = new DataTable();

            string sql = "select UserID, RoleID, UserName, Email, Status from Users";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }
                da.Fill(dtUser);
            } catch (SqlException se) {
                throw new Exception(se.Message);
            } finally {
                cnn.Close();
            }

            return dtUser;
        }

        public User GetUser(int UserID) {
            User result = null;

            string sql = "select UserID, RoleID, UserName, Email, Status from Users " +
                "where UserID = @UserID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@UserID", UserID);

            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) {
                    int ID = reader.GetInt32(0);
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

        public User GetUser(string EmailInput) {
            User result = null;

            string sql = "select UserID, RoleID, UserName, Email, Status from Users " +
                "where Email = @Email";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@Email", EmailInput);

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

        public bool AddUser(User user) {
            bool result = false;

            string sql = "insert into Users(RoleID, UserName, Email, Password, Status) " +
                "values(@RoleID, @UserName, @Email, HASHBYTES('SHA2_256', @Password), @Status)";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@RoleID", user.RoleID);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Status", user.Status);

            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }

                result = cmd.ExecuteNonQuery() > 0;
            } catch (SqlException se) {
                throw new Exception(se.Message);
            }

            return result;
        }

        public bool UpdateUser(User user) {
            bool result = false;

            string sql = "update Users " +
                "set RoleID = @RoleID, UserName = @UserName, Email = @Email, Password = HASHBYTES('SHA2_256', @Password), Status = @Status " +
                "where UserID = @UserID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@RoleID", user.RoleID);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Status", user.Status);
            cmd.Parameters.AddWithValue("@UserID", user.UserID);

            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }

                result = cmd.ExecuteNonQuery() > 0;
            } catch (SqlException se) {
                throw new Exception(se.Message);
            }

            return result;
        }


        public bool DeleteUser(int UserID) {
            bool result = false;

            string sql = "update Users set Status = '0' where UserID = @UserID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@UserID", UserID);

            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }

                result = cmd.ExecuteNonQuery() > 0;
            } catch (SqlException se) {
                throw new Exception(se.Message);
            }

            return result;
        }
    }
}

