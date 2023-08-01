using Biblioteka.Core.Users.Models;
using Biblioteka.Core.Users.Storages;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.DAOs
{
    public class LibrarianDAO : ISubject
    {
        private List<IObserver> _observers;
        private LibrarianStorage _storage;
        private List<Librarian> _librarians;

        public LibrarianDAO()
        {
            _storage = new LibrarianStorage();
            _librarians = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Add(Librarian user)
        {
            _librarians.Add(user);
            _storage.Save(_librarians);
            NotifyObservers();
        }

        public void Remove(Librarian user)
        {
            _librarians.Remove(user);
            _storage.Save(_librarians);
            NotifyObservers();
        }

        public List<Librarian> GetAll()
        {
            return _librarians;
        }

        public Librarian? GetByUsername(string username)
        {
            return _librarians.Find(x => x.Username == username);
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
