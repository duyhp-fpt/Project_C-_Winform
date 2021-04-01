using LibraryManagement.Commons;
using LibraryManagement.DTOs;
using LibraryManagement.Presenters;
using LibraryManagement.Utils;
using System;
using System.Windows.Forms;

namespace LibraryManagement.Views {
    public partial class LendBookForm : Form, ILendBookForm {
        User user;
        string bookID;
        string bookName;

        bool studentEmailStatus = false;

        public LendBookForm(User user, string BookID, string BookName) {
            InitializeComponent();
            this.user = user;
            this.bookID = BookID;
            this.bookName = BookName;
        }

        // private function
        private void InitForm() {
            lbErrorStudentEmail.Text = "";

            txtStaffID.Text = user.UserID.ToString();
            txtBookID.Text = bookID;
            txtBookName.Text = bookName;
            returnDate.MinDate = DateTime.Today.AddDays(1);
        }

        private void validateLend() {
            btnLend.Enabled = studentEmailStatus;
        }

        // implement interface
        public string StaffID { get => txtStaffID.Text; set => txtStaffID.Text = value; }
        public string StudentEmail { get => txtStudentEmail.Text; set => txtStudentEmail.Text = value; }
        public DateTime ReturnDate { get => returnDate.Value.Date; set => returnDate.Value = value; }
        public string BookID { get => txtBookID.Text; set => txtBookID.Text = value; }

        public void showMessage(string msg) {
            MessageBox.Show(msg);
        }

        // view event handler
        private void txtStudentEmail_TextChanged(object sender, EventArgs e) {
            studentEmailStatus = Validation.checkEmail(txtStudentEmail.Text);

            if (studentEmailStatus) lbErrorStudentEmail.Text = "";
            else lbErrorStudentEmail.Text = "Invalid email";

            validateLend();
        }

        private void LendBookForm_Load(object sender, EventArgs e) {
            InitForm();
        }

        private void LendBookForm_FormClosed(object sender, FormClosedEventArgs e) {
            Form newForm = new LibrarianForm(user);

            this.Hide();
            newForm.ShowDialog();
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e) {
            this.Close();
        }

        // presenter handler (interact with model)
        private void btnLend_Click(object sender, EventArgs e) {
            new BorrowPresenter(this).handleLendBook();
        }
    }
}
