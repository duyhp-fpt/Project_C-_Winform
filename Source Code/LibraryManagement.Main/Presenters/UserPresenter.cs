using LibraryManagement.Commons;
using LibraryManagement.DAOs;
using LibraryManagement.DTOs;
using System;
using System.Data;

namespace LibraryManagement.Presenters {
    public class UserPresenter {
        UserManager userManager = new UserManager();
        IProfileForm profileFormView;
        IAdminForm adminFormView;

        public UserPresenter(IProfileForm profileFormView) {
            this.profileFormView = profileFormView;
        }

        public UserPresenter(IAdminForm adminFormView) {
            this.adminFormView = adminFormView;
        }

        public void handleUpdateProfile() {
            int UserID = int.Parse(profileFormView.UserID);
            int RoleID = int.Parse(profileFormView.RoleID);
            bool Status = bool.Parse(profileFormView.Status);
            string Email = profileFormView.Email;
            string UserName = profileFormView.UserName;
            string Password = profileFormView.Password;

            try {
                bool result = userManager.UpdateUser(new User(UserID, RoleID, UserName, Email, Password, Status));

                if (!result) {
                    profileFormView.showMessage("Update profile fail!");
                    return;
                }

                profileFormView.showMessage("Update profile success!");

            } catch (Exception ex) {
                profileFormView.showMessage(ex.Message);
            }
        }

        public void handleRefreshProfile(String UserID) {
            try {
                User user = userManager.GetUser(int.Parse(UserID));

                profileFormView.openNewForm(user);

            } catch (Exception ex) {
                profileFormView.showMessage(ex.Message);
            }
        }

        public void handleLoadUsers() {
            try {
                DataTable dtUser = userManager.GetUsers();
                adminFormView.DataUser = dtUser;
                adminFormView.InitForm();
            } catch (Exception ex) {
                adminFormView.showMessage(ex.Message);
            }
        }

        public void handleAddUser() {
            int UserID = int.Parse(adminFormView.UserID);
            int RoleID = int.Parse(adminFormView.RoleID);
            string UserName = adminFormView.UserName;
            string Email = adminFormView.Email;
            string Password = adminFormView.Password;
            bool Status = bool.Parse(adminFormView.Status);

            try {
                bool result = userManager.AddUser(new User(UserID, RoleID, UserName, Email, Password, Status));
                if (!result) {
                    adminFormView.showMessage("Add User Fail");
                    return;
                }

                adminFormView.showMessage("Add User Success!");
                handleLoadUsers();

            } catch (Exception ex) {
                adminFormView.showMessage(ex.Message);
            }
        }

        public void handleUpdateUser() {
            int UserID = int.Parse(adminFormView.UserID);
            int RoleID = int.Parse(adminFormView.RoleID);
            string UserName = adminFormView.UserName;
            string Email = adminFormView.Email;
            string Password = adminFormView.Password;
            bool Status = bool.Parse(adminFormView.Status);

            try {
                bool result = userManager.UpdateUser(new User(UserID, RoleID, UserName, Email, Password, Status));
                if (!result) {
                    adminFormView.showMessage("Update User Fail");
                    return;
                }

                adminFormView.showMessage("Update User Success!");
                handleLoadUsers();

            } catch (Exception ex) {
                adminFormView.showMessage(ex.Message);
            }
        }

        public void handleDeleteUser() {
            int UserID = int.Parse(adminFormView.UserID);

            try {
                bool result = userManager.DeleteUser(UserID);
                if (!result) {
                    adminFormView.showMessage("Delete User Fail");
                    return;
                }

                adminFormView.showMessage("Delete User Success!");
                handleLoadUsers();

            } catch (Exception ex) {
                adminFormView.showMessage(ex.Message);
            }
        }
    }
}
