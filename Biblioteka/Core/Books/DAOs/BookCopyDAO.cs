using Biblioteka.Core.BookCopys.Storages;
using Biblioteka.Core.Books.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.BookCopys.DAOs
{
    public class BookCopyDAO : ISubject
    {
        private List<IObserver> _observers;
        private BookCopyStorage _storage;
        private List<BookCopy> _bookCopies;

        public BookCopyDAO()
        {
            _storage = new BookCopyStorage();
            _bookCopies = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_bookCopies.Count > 0)
                return _bookCopies.Max(x => x.Id) + 1;
            return 1;
        }

        public void Add(BookCopy book)
        {
            book.Id = NextId();
            _bookCopies.Add(book);
            _storage.Save(_bookCopies);
            NotifyObservers();
        }

        public void Remove(BookCopy book)
        {
            _bookCopies.Remove(book);
            _storage.Save(_bookCopies);
            NotifyObservers();
        }

        public List<BookCopy> GetAll()
        {
            return _bookCopies;
        }

        public BookCopy? GetById(int id)
        {
            return _bookCopies.Find(x => x.Id == id);
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
