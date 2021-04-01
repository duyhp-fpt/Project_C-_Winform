using System.Data;

namespace LibraryManagement.Commons {
    public interface IStudentForm: IDashboardForm {
        DataTable DataBook { get; set; }
    }
}
