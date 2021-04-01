using LibraryManagement.Commons;
using LibraryManagement.DTOs;
using LibraryManagement.Presenters;
using System;
using System.Data;
using System.Windows.Forms;

namespace LibraryManagement.Views {
    public partial class BorrowingForm : Form, IBorrowingForm {
        User user;
        DataTable dataBorrowingBooks;

        public BorrowingForm(User user) {
            InitializeComponent();
            this.user = user;
        }
        
        // implement interface
        public DataTable DataBorrowingBooks { get => this.dataBorrowingBooks; set => this.dataBorrowingBooks = value; }

        public void showMessage(string msg) {
            MessageBox.Show(msg);
        }

        public void InitForm() {
            dgvBorrowingBooks.DataSource = dataBorrowingBooks;
        }

        // presenter handler (interact with model)
        private void BorrowingForm_Load(object sender, EventArgs e) {
            new BorrowPresenter(this).handleLoadBorrowingBooks(user.Email);
        }
    }
}
