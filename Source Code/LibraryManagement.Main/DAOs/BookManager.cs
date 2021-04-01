using LibraryManagement.Commons;
using LibraryManagement.DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement.DAOs {
    public class BookManager : Manager {

        public DataTable GetBooks() {
            DataTable dtBook = new DataTable();

            string sql = "select BookID, BookName, Quantity, BookPrice, Author from Books";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }
                da.Fill(dtBook);
            } catch (SqlException se) {
                throw new Exception(se.Message);
            } finally {
                cnn.Close();
            }

            return dtBook;
        }

        public Book GetBook(int BookID) {
            Book result = null;

            string sql = "select BookID, BookName, Quantity, BookPrice, Author " +
                "from Books where BookID = @BookID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@BookID", BookID);

            try {
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) {
                    int ID = reader.GetInt32(0);
                    string BookName = reader.GetString(1);
                    int Quantity = reader.GetInt32(2);
                    float BookPrice = (float)reader.GetDouble(3);
                    string Author = reader.GetString(4);
                    result = new Book(BookID, BookName, Quantity, BookPrice, Author);
                }

            } catch (SqlException se) {
                throw new Exception(se.Message);
            } finally {
                cnn.Close();
            }

            return result;
        }

        public bool AddBook(Book book) {
            bool result = false;

            string sql = "insert into Books(BookName, Quantity, BookPrice, Author) " +
                "values(@BookName, @Quantity, @BookPrice, @Author)";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@BookName", book.BookName);
            cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
            cmd.Parameters.AddWithValue("@BookPrice", book.BookPrice);
            cmd.Parameters.AddWithValue("@Author", book.Author);

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

        public bool UpdateBook(Book book) {
            bool result = false;

            string sql = "update Books set BookName = @BookName, Quantity = @Quantity, BookPrice = @BookPrice, Author = @Author " +
                "where BookID = @BookID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@BookID", book.BookID);
            cmd.Parameters.AddWithValue("@BookName", book.BookName);
            cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
            cmd.Parameters.AddWithValue("@BookPrice", book.BookPrice);
            cmd.Parameters.AddWithValue("@Author", book.Author);

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


        public bool DeleteBook(int BookID) {
            bool result = false;

            string sql = "delete Books where BookID = @BookID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@BookID", BookID);

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
