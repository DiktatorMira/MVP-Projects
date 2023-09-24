using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using System.Xml.Serialization;

namespace Authors_and_books {
    public class Author : IAuthor {
        public string author_name { get; set; }
        public Author() => author_name = null;
    }
    public class Book : IBook {
        public string book_name { get; set; }
        public Author author_of_book { get; set; }
        public Book() {
            book_name = string.Empty;
            author_of_book = new Author();
        }
    }
    [Serializable]
    public class LibraryData {
        public List<Author> authors { get; set; }
        public List<Book> books { get; set; }
    }
    public class Model : IModel {
        public List<Book> books_list { get; set; }
        public List<Author> authors_list { get; set; }
        public Model() {
            books_list = new List<Book>();
            authors_list = new List<Author>();
        }
        public void SaveToFile(){
            try {
                using (StreamWriter writer = new StreamWriter("Library.xml")) {
                    XmlSerializer serializer = new XmlSerializer(typeof(LibraryData));
                    LibraryData data = new LibraryData { authors = authors_list, books = books_list };
                    serializer.Serialize(writer, data);
                }
            }
            catch { MessageBox.Show("Не удалось провести серриализацию!", "!"); }
        }
        public void LoadFromFile() {
            try {
                if (File.Exists("Library.xml")) {
                    using (StreamReader reader = new StreamReader("Library.xml")) {
                        XmlSerializer serializer = new XmlSerializer(typeof(LibraryData));
                        LibraryData data = (LibraryData)serializer.Deserialize(reader);
                        authors_list = data.authors;
                        books_list = data.books;
                    }
                }
            }
            catch { MessageBox.Show("Не удалось провести десерриализацию!", "!"); }
        }
    }
}
