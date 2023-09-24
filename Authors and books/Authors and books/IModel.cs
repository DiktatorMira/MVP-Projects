using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Authors_and_books {
    public interface IModel {
        List<Book> books_list { get; set; }
        List<Author> authors_list { get; set; }
        void SaveToFile();
        void LoadFromFile();
    }
    public interface IAuthor {
        string author_name { get; set; }
    }
    public interface IBook {
        string book_name { get; set; }
        Author author_of_book { get; set; }
    }
}
