namespace LibraryManagement.Commons {
    public interface ILoginForm: IActionForm {
        string Email { get; set; }
        string Password { get; set; }
    }
}
