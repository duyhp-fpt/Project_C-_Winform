using LibraryManagement.Commons;
using LibraryManagement.DAOs;
using LibraryManagement.DTOs;
using System;
using System.Data;

namespace LibraryManagement.Presenters {
    public class BookPresenter {
        BookManager bookManager = new BookManager();
        IStudentForm studentFormView;
        ILibrarianForm librarianFormView;

        public BookPresenter(IStudentForm studentFormView) {
            this.studentFormView = studentFormView;
        }

        public BookPresenter(ILibrarianForm librarianFormView) {
            this.librarianFormView = librarianFormView;
        }

        public void handleLoadBooksForStudent() {
            try {
                DataTable dtBook = bookManager.GetBooks();
                studentFormView.DataBook = dtBook;
                studentFormView.InitForm();
            } catch (Exception ex) {
                studentFormView.showMessage(ex.Message);
            }
        }

        public void handleLoadBooksForLibrarian() {
            try {
                DataTable dtBook = bookManager.GetBooks();
                librarianFormView.DataBook = dtBook;
                librarianFormView.InitForm();
            } catch (Exception ex) {
                librarianFormView.showMessage(ex.Message);
            }
        }

        public void handleAddBook() {
            int BookID = int.Parse(librarianFormView.BookID);
            string BookName = librarianFormView.BookName;
            int Quantity = int.Parse(librarianFormView.Quantity);
            float BookPrice = float.Parse(librarianFormView.BookPrice);
            string Author = librarianFormView.Author;

            try {
                bool result = bookManager.AddBook(new Book(BookID, BookName, Quantity, BookPrice, Author));
                if (!result) {
                    librarianFormView.showMessage("Add Book Fail");
                    return;
                }

                librarianFormView.showMessage("Add Book Success!");
                handleLoadBooksForLibrarian();

            } catch (Exception ex) {
                librarianFormView.showMessage(ex.Message);
            }
        }

        public void handleUpdateBook() {
            int BookID = int.Parse(librarianFormView.BookID);
            string BookName = librarianFormView.BookName;
            int Quantity = int.Parse(librarianFormView.Quantity);
            float BookPrice = float.Parse(librarianFormView.BookPrice);
            string Author = librarianFormView.Author;

            try {
                bool result = bookManager.UpdateBook(new Book(BookID, BookName, Quantity, BookPrice, Author));
                if (!result) {
                    librarianFormView.showMessage("Update Book Fail");
                    return;
                }

                librarianFormView.showMessage("Update Book Success!");
                handleLoadBooksForLibrarian();

            } catch (Exception ex) {
                librarianFormView.showMessage(ex.Message);
            }
        }

        public void handleDeleteBook() {
            int BookID = int.Parse(librarianFormView.BookID);

            try {
                bool result = bookManager.DeleteBook(BookID);
                if (!result) {
                    librarianFormView.showMessage("Delete Book Fail");
                    return;
                }

                librarianFormView.showMessage("Delete Book Success!");
                handleLoadBooksForLibrarian();

            } catch (Exception ex) {
                librarianFormView.showMessage(ex.Message);
            }
        }
    }
}
