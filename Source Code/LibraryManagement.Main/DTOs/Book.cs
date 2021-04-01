namespace LibraryManagement.DTOs {
    public class Book {

        public int BookID { get; set; }
        public string BookName { get; set; }
        public int Quantity { get; set; }
        public float BookPrice { get; set; }
        public string Author { get; set; }

        public Book() {
        }

        public Book(int bookID, string bookName, int quantity, float bookPrice, string author) {
            BookID = bookID;
            BookName = bookName;
            Quantity = quantity;
            BookPrice = bookPrice;
            Author = author;
        }
    }
}
