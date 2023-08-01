using Biblioteka.Core.Books.DAOs;
using Biblioteka.Core.Books.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.Controllers
{
    public class BookController
    {
        private BookDAO _books;

        public BookController()
        {
            _books = new BookDAO();
        }

        public List<Book> GetAllBooks()
        {
            return _books.GetAll();
        }

        public Book? GetBookById(int id)
        {
            return _books.GetById(id);
        }

        public void Create(Book Book)
        {
            _books.Add(Book);
        }

        public void Delete(Book Book)
        {
            _books.Remove(Book);
        }

        public void Subscribe(IObserver observer)
        {
            _books.Subscribe(observer);
        }
    }
}
