using LibraryManagement.Commons;
using LibraryManagement.DTOs;
using LibraryManagement.Presenters;
using LibraryManagement.Utils;
using System;
using System.Windows.Forms;

namespace LibraryManagement.Views {
    public partial class RecieveBookForm : Form, IRecieveBookForm {
        User user;
        string bookID;
        string bookName;

        bool studentEmailStatus = false;

        public RecieveBookForm(User user, string BookID, string BookName) {
            InitializeComponent();
            this.user = user;
            this.bookID = BookID;
            this.bookName = BookName;
        }

        // private function
        private void InitForm() {
            lbErrorStudentEmail.Text = "";

            txtBookID.Text = bookID;
            txtBookName.Text = bookName;
        }

        private void validateLend() {
            btnRecieve.Enabled = studentEmailStatus;
        }

        // implement interface
        public string StudentEmail { get => txtStudentEmail.Text; set => txtStudentEmail.Text = value; }
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

        private void RecieveBookForm_Load(object sender, EventArgs e) {
            InitForm();
        }

        private void RecieveBookForm_FormClosed(object sender, FormClosedEventArgs e) {
            Form newForm = new LibrarianForm(user);

            this.Hide();
            newForm.ShowDialog();
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e) {
            this.Close();
        }

        // presenter handler (interact with model)
        private void btnRecieve_Click(object sender, EventArgs e) {
            new BorrowPresenter(this).handleRecieveBook();
        }
    }
}
