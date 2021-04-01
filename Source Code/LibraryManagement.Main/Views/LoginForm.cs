using LibraryManagement.Commons;
using LibraryManagement.DTOs;
using LibraryManagement.Presenters;
using LibraryManagement.Utils;
using System;
using System.Windows.Forms;

namespace LibraryManagement.Views {
    public partial class LoginForm : Form, ILoginForm {
        bool emailStatus = false;
        bool passwordStatus = false;

        public LoginForm() {
            InitializeComponent();
        }

        // private function
        private void validateLogin() {
            btnLogin.Enabled = emailStatus && passwordStatus;
        }

        // implement interface
        public string Email { get => txtEmail.Text; set => txtEmail.Text = value; }
        public string Password { get => txtPassword.Text; set => txtPassword.Text = value; }

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
        private void LoginForm_Load(object sender, EventArgs e) {
            lbErrorEmail.Text = "";
            lbErrorPassword.Text = "";
            btnLogin.Enabled = false;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e) {
            emailStatus = Validation.checkEmail(txtEmail.Text);

            if (emailStatus) lbErrorEmail.Text = "";
            else lbErrorEmail.Text = "Invalid email";

            validateLogin();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e) {
            passwordStatus = Validation.checkPassword(txtPassword.Text);

            if (passwordStatus) lbErrorPassword.Text = "";
            else lbErrorPassword.Text = "Password must than 6 characters";

            validateLogin();
        }

        private void btnRegisterNow_Click(object sender, EventArgs e) {
            RegisterForm registerForm = new RegisterForm();
            this.Hide();
            registerForm.ShowDialog();
            this.Close();
        }

        // presenter handler (interact with model)
        private void btnLogin_Click(object sender, EventArgs e) {
            new AuthenPresenter(this).handleLogin();
        }

    }
}
