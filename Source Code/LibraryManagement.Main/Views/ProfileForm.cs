using LibraryManagement.Commons;
using LibraryManagement.DTOs;
using LibraryManagement.Presenters;
using LibraryManagement.Utils;
using System;
using System.Windows.Forms;

namespace LibraryManagement.Views {
    public partial class ProfileForm : Form, IProfileForm {
        User user;
        bool userNameStatus = false;
        bool passwordStatus = false;
        bool passwordConfirmStatus = false;

        public ProfileForm(User user) {
            InitializeComponent();
            this.user = user;
        }

        // private function
        private void InitForm() {
            lbErrorUserName.Text = "";
            lbErrorPassword.Text = "";
            lbErrorPasswordConfirm.Text = "";

            txtUserID.Text = user.UserID.ToString();
            txtRoleID.Text = user.RoleID.ToString();
            txtStatus.Text = user.Status.ToString();
            txtEmail.Text = user.Email;
            txtUserName.Text = user.UserName;
            txtPassword.Text = "";
            txtPasswordConfirm.Text = "";
        }

        private void validateUpdateProfile() {
            btnUpdateProfile.Enabled = userNameStatus && passwordStatus && passwordConfirmStatus;
        }

        // implement interface
        public string UserID { get => txtUserID.Text; set => txtUserID.Text = value; }
        public string RoleID { get => txtRoleID.Text; set => txtRoleID.Text = value; }
        public string Status { get => txtStatus.Text; set => txtStatus.Text = value; }
        public string Email { get => txtEmail.Text; set => txtEmail.Text = value; }
        public string UserName { get => txtUserName.Text; set => txtUserName.Text = value; }
        public string Password { get => txtPassword.Text; set => txtPassword.Text = value; }
        public string PasswordConfirm { get => txtPasswordConfirm.Text; set => txtPasswordConfirm.Text = value; }

        public void showMessage(string msg) {
            MessageBox.Show(msg);
        }

        public void openNewForm(Object obj) {
            User user = (User)obj;

            Form newForm;

            if (user.RoleID == 1) {
                newForm = new StudentForm(user);
            } else if (user.RoleID == 2) {
                newForm = new LibrarianForm(user);
            } else {
                newForm = new AdminForm(user);
            }

            this.Hide();
            newForm.ShowDialog();
            this.Close();
        }

        // view event handler
        private void ProfileForm_Load(object sender, EventArgs e) {
            InitForm();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e) {
            userNameStatus = Validation.checkEmpty(txtUserName.Text);

            if (userNameStatus) lbErrorUserName.Text = "";
            else lbErrorUserName.Text = "Invalid userName";

            validateUpdateProfile();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e) {
            passwordStatus = Validation.checkPassword(txtPassword.Text);

            if (passwordStatus) lbErrorPassword.Text = "";
            else lbErrorPassword.Text = "Password must than 6 characters";

            validateUpdateProfile();
        }

        private void txtPasswordConfirm_TextChanged(object sender, EventArgs e) {
            passwordConfirmStatus = txtPassword.Text.Equals(txtPasswordConfirm.Text);

            if (passwordConfirmStatus) lbErrorPasswordConfirm.Text = "";
            else lbErrorPasswordConfirm.Text = "Password and PasswordConfirm is not the same";

            validateUpdateProfile();
        }

        private void btnBack_Click(object sender, EventArgs e) {
            this.Close();
        }

        // presenter handler (interact with model)
        private void btnUpdateProfile_Click(object sender, EventArgs e) {
            new UserPresenter(this).handleUpdateProfile();
        }

        private void ProfileForm_FormClosed(object sender, FormClosedEventArgs e) {
            new UserPresenter(this).handleRefreshProfile(txtUserID.Text);
        }
    }
}
