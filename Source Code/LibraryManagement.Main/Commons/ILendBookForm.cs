using System;

namespace LibraryManagement.Commons {
    public interface ILendBookForm : ISimpleForm {
        string StaffID { get; set; }
        string StudentEmail { get; set; }
        DateTime ReturnDate { get; set; }
        string BookID { get; set; }
    }
}
