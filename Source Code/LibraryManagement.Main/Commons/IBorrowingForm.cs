using System.Data;

namespace LibraryManagement.Commons {
    public interface IBorrowingForm : ISimpleForm {
        DataTable DataBorrowingBooks { get; set; }

        void InitForm();
    }
}
