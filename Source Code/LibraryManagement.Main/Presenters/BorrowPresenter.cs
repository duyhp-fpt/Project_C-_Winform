using LibraryManagement.Commons;
using LibraryManagement.DAOs;
using LibraryManagement.DTOs;
using System;
using System.Data;

namespace LibraryManagement.Presenters {
    public class BorrowPresenter {
        BorrowManager borrowManager = new BorrowManager();
        ILendBookForm lendFormView;
        IRecieveBookForm recieveFormView;
        IBorrowingForm borrowingFormView;

        public BorrowPresenter(ILendBookForm lendFormView) {
            this.lendFormView = lendFormView;
        }

        public BorrowPresenter(IRecieveBookForm recieveFormView) {
            this.recieveFormView = recieveFormView;
        }

        public BorrowPresenter(IBorrowingForm borrowingFormView) {
            this.borrowingFormView = borrowingFormView;
        }

        public void handleLendBook() {
            int StaffID = int.Parse(lendFormView.StaffID);
            string StudentEmail = lendFormView.StudentEmail;
            DateTime ReturnDate = lendFormView.ReturnDate;
            int BookID = int.Parse(lendFormView.BookID);

            try {
                // validate email existed
                User student = new UserManager().GetUser(StudentEmail);

                if (student == null) {
                    lendFormView.showMessage("Email not exist");
                    return;
                }

                // validate book quantity
                Book book = new BookManager().GetBook(BookID);

                if (book.Quantity <= 0) {
                    lendFormView.showMessage("This book is out of stock");
                    return;
                }

                // validate student cannot lend the same book util they return it
                bool lentResult = borrowManager.CheckLentBook(BookID, StudentEmail);

                if (lentResult) {
                    lendFormView.showMessage("Student has borrowed this book!");
                    return;
                }

                // update book quantity
                book.Quantity -= 1;

                bool quantityResult = new BookManager().UpdateBook(book);

                if (!quantityResult) {
                    lendFormView.showMessage("Update quantity error. Lend Book Fail!");
                    return;
                }

                // Lend book to student
                bool result = borrowManager.LendBook(new Borrow(-1, StaffID, StudentEmail, DateTime.Now, ReturnDate, BookID, false));
                if (!result) {
                    lendFormView.showMessage("Lend Book Fail");
                    return;
                }

                lendFormView.showMessage("Lend Book Success!");

            } catch (Exception ex) {
                lendFormView.showMessage(ex.Message);
            }
        }

        public void handleRecieveBook() {
            string StudentEmail = recieveFormView.StudentEmail;
            int BookID = int.Parse(recieveFormView.BookID);

            try {
                // validate email existed
                User student = new UserManager().GetUser(StudentEmail);

                if (student == null) {
                    recieveFormView.showMessage("Email not exist");
                    return;
                }

                // validate book exist
                Book book = new BookManager().GetBook(BookID);

                if (book == null) {
                    recieveFormView.showMessage("This book not exist");
                    return;
                }

                // update book quantity
                book.Quantity += 1;

                bool quantityResult = new BookManager().UpdateBook(book);

                if (!quantityResult) {
                    recieveFormView.showMessage("Update quantity error. Recieve Book Fail!");
                    return;
                }

                // Recieve book from student
                bool result = borrowManager.RecieveBook(StudentEmail, BookID);
                if (!result) {
                    recieveFormView.showMessage("Recieve Book Fail. Maybe student hasn't borrowed or has returned this book");
                    return;
                }

                recieveFormView.showMessage("Recieve Book Success!");

            } catch (Exception ex) {
                recieveFormView.showMessage(ex.Message);
            }
        }

        public void handleLoadBorrowingBooks(string studentEmail) {
            try {
                DataTable dtBook = borrowManager.GetBorrowingBooks(studentEmail);
                borrowingFormView.DataBorrowingBooks = dtBook;
                borrowingFormView.InitForm();
            } catch (Exception ex) {
                borrowingFormView.showMessage(ex.Message);
            }
        }
    }
}
