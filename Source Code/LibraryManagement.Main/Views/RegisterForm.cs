using LibraryManagement.Commons;
using LibraryManagement.DTOs;
using LibraryManagement.Presenters;
using LibraryManagement.Utils;
using System;
using System.Windows.Forms;

namespace LibraryManagement.Views {
    public partial class RegisterForm : Form, IRegisterForm {
        bool userNameStatus = false;
        bool emailStatus = false;
        bool passwordStatus = false;
        bool passwordConfirmStatus = false;

        public RegisterForm() {
            InitializeComponent();
        }

        // private function
        private void validateRegister() {
            btnRegister.Enabled = userNameStatus && emailStatus && passwordStatus && passwordConfirmStatus;
        }

        // implement interface
        public string UserName { get => txtUserName.Text; set => txtUserName.Text = value; }
        public string Email { get => txtEmail.Text; set => txtEmail.Text = value; }
        public string Password { get => txtPassword.Text; set => txtPassword.Text = value; }
        public string PasswordConfirm { get => txtPasswordConfirm.Text; set => txtPasswordConfirm.Text = value; }

        public void showMessage(string msg) {
            MessageBox.Show(msg);
        }

        public void openNewForm(object obj) {
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
        private void RegisterForm_Load(object sender, EventArgs e) {
            lbErrorUserName.Text = "";
            lbErrorEmail.Text = "";
            lbErrorPassword.Text = "";
            lbErrorPasswordConfirm.Text = "";
            btnRegister.Enabled = false;
        }

        private void txtUserName_TextChanged(object sender, EventArgs e) {
            userNameStatus = Validation.checkEmpty(txtUserName.Text);

            if (userNameStatus) lbErrorUserName.Text = "";
            else lbErrorUserName.Text = "Invalid userName";

            validateRegister();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e) {
            emailStatus = Validation.checkEmail(txtEmail.Text);

            if (emailStatus) lbErrorEmail.Text = "";
            else lbErrorEmail.Text = "Invalid email";

            validateRegister();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e) {
            passwordStatus = Validation.checkPassword(txtPassword.Text);

            if (passwordStatus) lbErrorPassword.Text = "";
            else lbErrorPassword.Text = "Password must than 6 characters";

            validateRegister();
        }

        private void txtPasswordConfirm_TextChanged(object sender, EventArgs e) {
            passwordConfirmStatus = txtPassword.Text.Equals(txtPasswordConfirm.Text);

            if (passwordConfirmStatus) lbErrorPasswordConfirm.Text = "";
            else lbErrorPasswordConfirm.Text = "Password and PasswordConfirm is not the same";

            validateRegister();
        }

        private void btnGoToLogin_Click(object sender, EventArgs e) {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        // presenter handler (interact with model)
        private void btnRegister_Click(object sender, EventArgs e) {
            new AuthenPresenter(this).handleRegister();
        }
    }
}
