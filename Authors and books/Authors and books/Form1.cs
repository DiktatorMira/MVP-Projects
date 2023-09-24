using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

namespace Authors_and_books
{
    public partial class Form1 : Form, IView
    {
        public event Action<string> AddAuthor;
        public event Action<string> DeleteAuthor;
        public event Action<string, string> EditAuthor;
        public event Action<string, string> AddBook;
        public event Action<string> DeleteBook;
        public event Action<string, string> EditBook;
        public event Action<string> Filter;
        public event EventHandler<EventArgs> SaveToFile;
        public event EventHandler<EventArgs> LoadFromFile;
        public Form1()
        {
            InitializeComponent();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
        }
        void IView.PrintAuthors(List<Author> authors_list)
        {
            authors.Items.Clear();
            foreach (var i in authors_list) authors.Items.Add(i.author_name);
            if (authors.Items.Count > 0) authors.SelectedIndex = 0;
        }
        void IView.PrintBooks(List<Book> books_list)
        {
            books.Items.Clear();
            foreach (var i in books_list) books.Items.Add(i.book_name);
        }
        void IView.PrintFilter(List<Book> books_list)
        {
            books.Items.Clear();
            foreach (var i in books_list) books.Items.Add($"{i.book_name}");
        }
        void IView.Error(string message) => MessageBox.Show(message, "Ошибка стоп 000000", MessageBoxButtons.OK, MessageBoxIcon.Error);
        private void menu11_Click(object sender, EventArgs e) => LoadFromFile?.Invoke(this, EventArgs.Empty);
        private void menu12_Click(object sender, EventArgs e) => SaveToFile?.Invoke(this, EventArgs.Empty);
        private void menu21_Click(object sender, EventArgs e)
        {
            using (var addAuthor = new AddAuthor())
            {
                if (addAuthor.ShowDialog() == DialogResult.OK) AddAuthor?.Invoke(addAuthor.authorname);
            }
        }
        private void menu22_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы точно хотите удалить автора?", "?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes && authors.SelectedItem != null)
            {
                string select = authors.SelectedItem.ToString();
                DeleteAuthor?.Invoke(select);
            }
        }
        private void menu23_Click(object sender, EventArgs e)
        {
            using (var editAuthor = new EditAuthor())
            {
                if (editAuthor.ShowDialog() == DialogResult.OK) EditAuthor?.Invoke(editAuthor.oldname, editAuthor.newname);
            }
        }
        private void menu24_Click(object sender, EventArgs e)
        {
            using (var addBook = new AddBook())
            {
                if (addBook.ShowDialog() == DialogResult.OK) AddBook?.Invoke(addBook.authorname, addBook.bookname);
            }
        }
        private void menu25_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы точно хотите удалить книгу?", "?",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes && books.SelectedItem != null)
            {
                string selectedBook = books.SelectedItem.ToString();
                DeleteBook?.Invoke(selectedBook);
            }
        }
        private void menu26_Click(object sender, EventArgs e)
        {
            using (var editBook = new EditBook())
            {
                if (editBook.ShowDialog() == DialogResult.OK) EditBook?.Invoke(editBook.oldname, editBook.newname);
            }
        }
        private void authors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filtration.Checked == true && authors.SelectedItem != null) Filter?.Invoke(authors.SelectedItem.ToString());
        }
        private void filtration_CheckedChanged(object sender, EventArgs e)
        {
            if (filtration.Checked == true && authors.SelectedItem != null) Filter?.Invoke(authors.SelectedItem.ToString());
            else Filter?.Invoke(null);
        }
    }
}