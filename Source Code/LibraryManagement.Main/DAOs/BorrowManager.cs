using LibraryManagement.Commons;
using LibraryManagement.DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement.DAOs {
    public class BorrowManager : Manager {
        public DataTable GetBorrowingBooks(string studentEmail) {
            DataTable dtBorrow = new DataTable();

            string sql = "select Borrows.BookID as BookID, BookName, BorrowDate, ReturnDate, Returned " +
                "from Borrows, Books where Borrows.BookID = Books.BookID and " +
                "StudentEmail = @StudentEmail and Returned = '0' " +
                "order by ReturnDate";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@StudentEmail", studentEmail);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }
                da.Fill(dtBorrow);
            } catch (SqlException se) {
                throw new Exception(se.Message);
            } finally {
                cnn.Close();
            }

            return dtBorrow;
        }

        public bool LendBook(Borrow borrow) {
            bool result = false;

            string sql = "insert into Borrows(StaffID, StudentEmail, BorrowDate, ReturnDate, BookID, Returned) " +
                "values(@StaffID, @StudentEmail, GETDATE(), @ReturnDate, @BookID, '0')";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@StaffID", borrow.StaffID);
            cmd.Parameters.AddWithValue("@StudentEmail", borrow.StudentEmail);
            cmd.Parameters.AddWithValue("@ReturnDate", borrow.ReturnDate.ToString());
            cmd.Parameters.AddWithValue("@BookID", borrow.BookID);

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

        public bool CheckLentBook(int BookID, string StudentEmail) {
            bool result = false;

            string sql = "select BorrowID from Borrows " +
                "where StudentEmail = @StudentEmail and BookID = @BookID and Returned = '0'";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@BookID", BookID);
            cmd.Parameters.AddWithValue("@StudentEmail", StudentEmail);

            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) {
                    return true;
                }

            } catch (SqlException se) {
                throw new Exception(se.Message);
            } finally {
                cnn.Close();
            }

            return result;
        }

        public bool RecieveBook(string studentEmail, int bookID) {
            bool result = false;

            string sql = "update Borrows set Returned = '1' " +
                "where StudentEmail = @StudentEmail and BookID = @BookID and Returned = '0'";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@StudentEmail", studentEmail);
            cmd.Parameters.AddWithValue("@BookID", bookID);

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
