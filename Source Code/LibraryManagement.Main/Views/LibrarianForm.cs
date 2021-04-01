using LibraryManagement.Commons;
using LibraryManagement.DTOs;
using LibraryManagement.Presenters;
using LibraryManagement.Utils;
using System;
using System.Data;
using System.Windows.Forms;

namespace LibraryManagement.Views {
    public partial class LibrarianForm : Form, ILibrarianForm {
        User user;
        DataTable dataBook;

        bool bookNameStatus = false;
        bool quantityStatus = false;
        bool bookPriceStatus = false;
        bool authorStatus = false;

        public LibrarianForm(User user) {
            InitializeComponent();
            this.user = user;
        }

        // private function
        private void validateAddOrUpdateBook() {
            bool valiate = bookNameStatus && quantityStatus && bookPriceStatus && authorStatus;
            btnAdd.Enabled = valiate;
            btnUpdate.Enabled = valiate;
        }

        // implement interface
        public DataTable DataBook { get => this.dataBook; set => this.dataBook = value; }

        public string BookID { get => txtBookID.Text; set => txtBookID.Text = value; }
        public string BookName { get => txtBookName.Text; set => txtBookName.Text = value; }
        public string Quantity { get => txtQuantity.Text; set => txtQuantity.Text = value; }
        public string BookPrice { get => txtBookPrice.Text; set => txtBookPrice.Text = value; }
        public string Author { get => txtAuthor.Text; set => txtAuthor.Text = value; }

        public void showMessage(string msg) {
            MessageBox.Show(msg);
        }

        public void InitForm() {
            lbHeader.Text = "Welcome " + this.user.UserName;

            lbErrorBookName.Text = "";
            lbErrorQuantity.Text = "";
            lbErrorBookPrice.Text = "";
            lbErrorAuthor.Text = "";

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

            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
        }

        // view event handler
        private void txtBookName_TextChanged(object sender, EventArgs e) {
            bookNameStatus = Validation.checkEmpty(txtBookName.Text);

            if (bookNameStatus) lbErrorBookName.Text = "";
            else lbErrorBookName.Text = "Book name not empty";

            validateAddOrUpdateBook();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e) {
            quantityStatus = Validation.checkInteger(txtQuantity.Text) && int.Parse(txtQuantity.Text) >= 0;

            if (quantityStatus) lbErrorQuantity.Text = "";
            else lbErrorQuantity.Text = "Quantity must integer and >= 0";

            validateAddOrUpdateBook();
        }

        private void txtBookPrice_TextChanged(object sender, EventArgs e) {
            bookPriceStatus = Validation.checkFloat(txtBookPrice.Text) && float.Parse(txtBookPrice.Text) >= 0;

            if (bookPriceStatus) lbErrorBookPrice.Text = "";
            else lbErrorBookPrice.Text = "Quantity must integer and >= 0";

            validateAddOrUpdateBook();
        }

        private void txtAuthor_TextChanged(object sender, EventArgs e) {
            authorStatus = Validation.checkEmpty(txtAuthor.Text);

            if (authorStatus) lbErrorAuthor.Text = "";
            else lbErrorAuthor.Text = "Author not empty";

            validateAddOrUpdateBook();
        }

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
            //this.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e) {
            this.user = null;
            LoginForm loginForm = new LoginForm();

            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        private void btnLendBook_Click(object sender, EventArgs e) {
            LendBookForm lendBookForm = new LendBookForm(user, txtBookID.Text, txtBookName.Text);

            this.Hide();
            lendBookForm.ShowDialog();
            this.Close();
        }

        private void btnRecieveBook_Click(object sender, EventArgs e) {
            RecieveBookForm recieveBookForm = new RecieveBookForm(user, txtBookID.Text, txtBookName.Text);

            this.Hide();
            recieveBookForm.ShowDialog();
            this.Close();
        }

        // presenter handler (interact with model)
        private void LibrarianForm_Load(object sender, EventArgs e) {
            new BookPresenter(this).handleLoadBooksForLibrarian();
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            new BookPresenter(this).handleAddBook();
        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            new BookPresenter(this).handleUpdateBook();
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            new BookPresenter(this).handleDeleteBook();
        }

    }
}
