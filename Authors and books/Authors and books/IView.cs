using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Authors_and_books {
    public interface IView {
        event Action<string> AddAuthor;
        event Action<string> DeleteAuthor;
        event Action<string, string> EditAuthor;
        event Action<string, string> AddBook;
        event Action<string> DeleteBook;
        event Action<string, string> EditBook;
        event Action<string> Filter;
        event EventHandler<EventArgs> SaveToFile;
        event EventHandler<EventArgs> LoadFromFile;
        void PrintAuthors(List<Author> authors);
        void PrintBooks(List<Book> books);
        void PrintFilter(List<Book> books);
        void Error(string message);
    }
}
