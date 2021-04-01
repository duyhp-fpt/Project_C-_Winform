using System.Data;

namespace LibraryManagement.Commons {
    public interface ILibrarianForm : IDashboardForm{
        DataTable DataBook { get; set; }

        string BookID { get; set; }
        string BookName { get; set; }
        string Quantity { get; set; }
        string BookPrice { get; set; }
        string Author { get; set; }
    }
}
