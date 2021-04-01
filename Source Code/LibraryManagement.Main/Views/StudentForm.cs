using LibraryManagement.Commons;
using LibraryManagement.DTOs;
using LibraryManagement.Presenters;
using System;
using System.Data;
using System.Windows.Forms;

namespace LibraryManagement.Views {
    public partial class StudentForm : Form, IStudentForm {
        User user;
        DataTable dataBook;

        public StudentForm(User user) {
            InitializeComponent();
            this.user = user;
        }

        // implement interface
        public DataTable DataBook { get => this.dataBook; set => this.dataBook = value; }

        public void showMessage(string msg) {
            MessageBox.Show(msg);
        }

        public void InitForm() {
            lbHeader.Text = "Welcome " + this.user.UserName;

            txtBookID.DataBindings.Clear();
            txtBookName.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();
            txtBookPrice.DataBindings.Clear();
            txtAuthor.DataBindings.Clear();

            txtBookID.DataBindings.Add("Text", dataBook, "BookID");
            txtBookName.DataBindings.Add("Text", dataBook, "BookName");
            txtQuantity.DataBindings.Add("Text", dataBook, "Quantity");
            txtBookPrice.DataBindings.Add("Text", dataBook, "BookPrice");
            txtAuthor.DataBindings.Add("Text", dataBook, "Author");

            dgvBookList.DataSource = dataBook;

            lbTotalQuantity.Text = "Total Quantity : " + dataBook.Compute("SUM(Quantity)", String.Empty);
        }

        // view event handler
        private void txtSearch_TextChanged(object sender, EventArgs e) {
            DataView defaultView = dataBook.DefaultView;
            string filter = "BookName like '%" + txtSearch.Text + "%'";
            defaultView.RowFilter = filter;

            lbTotalQuantity.Text = "Total Quantity : " + dataBook.Compute("SUM(Quantity)", filter);
        }

        private void btnViewProfile_Click(object sender, EventArgs e) {
            ProfileForm profileForm = new ProfileForm(user);

            this.Hide();
            profileForm.ShowDialog();
            this.Close();
        }

        private void btnViewBorrowing_Click(object sender, EventArgs e) {
            BorrowingForm borrowingForm = new BorrowingForm(user);
            borrowingForm.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e) {
            this.user = null;
            LoginForm loginForm = new LoginForm();

            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        // presenter handler (interact with model)
        private void StudentForm_Load(object sender, EventArgs e) {
            new BookPresenter(this).handleLoadBooksForStudent();
        }

    }
}
