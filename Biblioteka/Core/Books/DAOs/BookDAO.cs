using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Books.Storages;
using Biblioteka.Core.Users.Models;
using Biblioteka.Core.Users.Storages;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.DAOs
{
    public class BookDAO : ISubject
    {
        private List<IObserver> _observers;
        private BookStorage _storage;
        private List<Book> _books;

        public BookDAO()
        {
            _storage = new BookStorage();
            _books = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Add(Book book)
        {
            _books.Add(book);
            _storage.Save(_books);
            NotifyObservers();
        }

        public void Remove(Book book)
        {
            _books.Remove(book);
            _storage.Save(_books);
            NotifyObservers();
        }

        public List<Book> GetAll()
        {
            return _books;
        }

        public Book? GetByISBN(string isbn)
        {
            return _books.Find(x => x.ISBN == isbn);
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
