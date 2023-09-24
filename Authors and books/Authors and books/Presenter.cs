using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Authors_and_books {
    public class Presenter {
        private readonly IView view;
        private Model model;

        public Presenter(IView view) {
            this.view = view;
            model = new Model();
            view.AddAuthor += AddAutor;
            view.DeleteAuthor += DeleteAuthor;
            view.EditAuthor += EditAuthor;
            view.AddBook += AddBook;
            view.DeleteBook += DeleteBook;
            view.EditBook += EditBook;
            view.Filter += Filter;
            view.SaveToFile += new EventHandler<EventArgs>(SaveToFile);
            view.LoadFromFile += new EventHandler<EventArgs>(LoadFromFile);
        }
        private void SaveToFile(object sender, EventArgs e) => model.SaveToFile();
        private void LoadFromFile(object sender, EventArgs e) {
            model.LoadFromFile();
            Update();
        }
        private void Filter(string authorname) {
            if (authorname != null) {
                var filtered = model.books_list.Where(b => b.author_of_book.author_name == authorname).ToList();
                view.PrintFilter(filtered);
            }
            else Update();
        }
        private void AddAutor(string authorname){
            if (model.authors_list.Find(i => i.author_name == authorname) != null) view.Error("Такой автор существует!");
            else {
                Author temp = new Author();
                temp.author_name = authorname;
                model.authors_list.Add(temp);
                Update();
            }
        }
        private void AddBook(string authorname, string bookname) {
            Author author = model.authors_list.FirstOrDefault(i => i.author_name == authorname);
            if (author != null) {
                Book temp = new Book { book_name = bookname, author_of_book = author };
                model.books_list.Add(temp);
            }
            else view.Error("Такого автора не существует!");
            Update();
        }
        private void EditAuthor(string oldname, string newname){
            Author temp = model.authors_list.FirstOrDefault(i => i.author_name == oldname);
            if (temp != null)  {
                temp.author_name = newname;
                foreach (var i in model.books_list.Where(i => i.author_of_book.author_name == oldname)) {
                    i.author_of_book.author_name = newname;
                }
            }
            else view.Error("Такого автора не существует!");
            Update();
        }
        private void DeleteAuthor(string authorname) {
            Author temp = model.authors_list.FirstOrDefault(i => i.author_name == authorname);
            if (temp != null) {
                model.authors_list.Remove(temp);
                model.books_list.RemoveAll(i => i.author_of_book.author_name == authorname);
            }
            Update();
        }
        private void EditBook(string oldname, string newname) {
            Book temp = model.books_list.FirstOrDefault(i => i.book_name == oldname);
            if (temp != null) temp.book_name = newname;
            else  view.Error("Такой книги не существует!");
            Update();
        }
        private void DeleteBook(string name) {
            Book temp = model.books_list.FirstOrDefault(i => i.book_name == name);
            if (temp != null) model.books_list.Remove(temp);
            Update();
        }
        private void Update() {
            view.PrintAuthors(model.authors_list);
            view.PrintBooks(model.books_list);
        }
    }
}
