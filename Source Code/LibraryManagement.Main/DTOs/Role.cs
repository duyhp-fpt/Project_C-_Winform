namespace LibraryManagement.DTOs {
    public class Role {

        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public Role() {
        }

        public Role(int roleID, string roleName) {
            RoleID = roleID;
            RoleName = roleName;
        }
    }
}
