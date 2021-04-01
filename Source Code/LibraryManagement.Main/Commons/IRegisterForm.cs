namespace LibraryManagement.Commons {
    public interface IRegisterForm : IActionForm {
        string UserName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string PasswordConfirm { get; set; }
    }
}
