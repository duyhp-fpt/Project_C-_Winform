using System;

namespace LibraryManagement.DTOs {
    public class Borrow {

        public int BorrowID { get; set; }
        public int StaffID { get; set; }
        public string StudentEmail { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int BookID { get; set; }
        public bool Returned { get; set; }

        public Borrow() {
        }

        public Borrow(int borrowID, int staffID, string studentEmail, DateTime borrowDate, DateTime returnDate, int bookID, bool returned) {
            BorrowID = borrowID;
            StaffID = staffID;
            StudentEmail = studentEmail;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            BookID = bookID;
            Returned = returned;
        }
    }
}
