using LibraryManagement.Commons;
using LibraryManagement.DTOs;
using LibraryManagement.Presenters;
using LibraryManagement.Utils;
using System;
using System.Data;
using System.Windows.Forms;

namespace LibraryManagement.Views {
    public partial class AdminForm : Form, IAdminForm {
        User user;
        DataTable dataUser;

        bool userNameStatus = false;
        bool emailStatus = false;
        bool passwordStatus = false;

        string filterName;
        string filterStatus;

        public AdminForm(User user) {
            InitializeComponent();
            this.user = user;
        }

        // private function
        private void validateAddOrUpdateUser() {
            bool valiate = userNameStatus && emailStatus && passwordStatus;
            btnAdd.Enabled = valiate;
            btnUpdate.Enabled = valiate;
        }

        private void filterUser() {
            DataView defaultView = dataUser.DefaultView;
            string statusQuery = filterStatus.Equals("All") ? "" : (filterStatus.Equals("Active") ? " and Status = 1" : " and Status = 0");
            string filter = "UserName like '%" + filterName + "%'" + statusQuery;
            defaultView.RowFilter = filter;

            lbTotalQuantity.Text = "Total Quantity : " + dataUser.Compute("Count(UserID)", filter);
        }

        // implement interface
        public DataTable DataUser { get => this.dataUser; set => this.dataUser = value; }

        public string UserID { get => txtUserID.Text; set => txtUserID.Text = value; }
        public string RoleID { get => txtRoleID.Text; set => txtRoleID.Text = value; }
        public string UserName { get => txtUserName.Text; set => txtUserName.Text = value; }
        public string Email { get => txtEmail.Text; set => txtEmail.Text = value; }
        public string Password { get => txtPassword.Text; set => txtPassword.Text = value; }
        public string Status { get => txtStatus.Text; set => txtStatus.Text = value; }

        public void showMessage(string msg) {
            MessageBox.Show(msg);
        }

        public void InitForm() {
            lbHeader.Text = "Welcome " + this.user.UserName;

            lbErrorUserName.Text = "";
            lbErrorEmail.Text = "";
            lbErrorPassword.Text = "";

            txtUserID.DataBindings.Clear();
            txtRoleID.DataBindings.Clear();
            txtUserName.DataBindings.Clear();
            txtEmail.DataBindings.Clear();
            txtStatus.DataBindings.Clear();

            txtUserID.DataBindings.Add("Text", dataUser, "UserID");
            txtRoleID.DataBindings.Add("Text", dataUser, "RoleID");
            txtUserName.DataBindings.Add("Text", dataUser, "UserName");
            txtEmail.DataBindings.Add("Text", dataUser, "Email");
            txtStatus.DataBindings.Add("Text", dataUser, "Status");

            dgvUserList.DataSource = dataUser;

            lbTotalQuantity.Text = "Total Quantity : " + dataUser.Compute("COUNT(UserID)", String.Empty);

            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;

            selectStatus.Text = "All";
        }

        // view event handler
        private void txtUserName_TextChanged(object sender, EventArgs e) {
            userNameStatus = Validation.checkEmpty(txtUserName.Text);

            if (userNameStatus) lbErrorUserName.Text = "";
            else lbErrorUserName.Text = "User name not empty";

            validateAddOrUpdateUser();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e) {
            emailStatus = Validation.checkEmail(txtEmail.Text);

            if (emailStatus) lbErrorEmail.Text = "";
            else lbErrorEmail.Text = "Invalid email";

            validateAddOrUpdateUser();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e) {
            passwordStatus = Validation.checkPassword(txtPassword.Text);

            if (passwordStatus) lbErrorPassword.Text = "";
            else lbErrorPassword.Text = "Password must than 6 characters";

            validateAddOrUpdateUser();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) {
            this.filterName = txtSearch.Text;
            filterUser();
        }

        private void selectStatus_SelectedIndexChanged(object sender, EventArgs e) {
            this.filterStatus = selectStatus.SelectedItem.ToString();
            filterUser();
        }

        private void btnViewProfile_Click(object sender, EventArgs e) {
            ProfileForm profileForm = new ProfileForm(user);

            this.Hide();
            profileForm.ShowDialog();
            this.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e) {
            this.user = null;
            LoginForm loginForm = new LoginForm();

            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        // presenter handler (interact with model)
        private void AdminForm_Load(object sender, EventArgs e) {
            new UserPresenter(this).handleLoadUsers();
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            new UserPresenter(this).handleAddUser();
        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            new UserPresenter(this).handleUpdateUser();
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            new UserPresenter(this).handleDeleteUser();
        }
    }
}
