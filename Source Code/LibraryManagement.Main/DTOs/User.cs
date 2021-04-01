namespace LibraryManagement.DTOs {
    public class User {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }

        public User() {
        }

        public User(int userID, int roleID, string userName, string email, string password, bool status) {
            UserID = userID;
            RoleID = roleID;
            UserName = userName;
            Email = email;
            Password = password;
            Status = status;
        }
    }
}
