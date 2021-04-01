using System.Data;

namespace LibraryManagement.Commons {
    public interface IAdminForm: IDashboardForm {
        DataTable DataUser { get; set; }

        string UserID { get; set; }
        string RoleID { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string Status { get; set; }

    }
}
