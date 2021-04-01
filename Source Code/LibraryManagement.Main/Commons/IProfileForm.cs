using System;

namespace LibraryManagement.Commons {
    public interface IProfileForm : IActionForm {
        string UserID { get; set; }
        string RoleID { get; set; }
        string Status { get; set; }
        string Email { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string PasswordConfirm { get; set; }
    }
}
