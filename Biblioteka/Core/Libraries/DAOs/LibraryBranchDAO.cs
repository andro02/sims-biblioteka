using Biblioteka.Core.Libraries.Models;
using Biblioteka.Core.Libraries.Storages;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Libraries.DAOs
{
    public class LibraryDAO : ISubject
    {
        private List<IObserver> _observers;
        private LibraryStorage _storage;
        private List<Library> _libraries;

        public LibraryDAO()
        {
            _storage = new LibraryStorage();
            _libraries = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Add(Library user)
        {
            _libraries.Add(user);
            _storage.Save(_libraries);
            NotifyObservers();
        }

        public void Remove(Library user)
        {
            _libraries.Remove(user);
            _storage.Save(_libraries);
            NotifyObservers();
        }

        public List<Library> GetAll()
        {
            return _libraries;
        }

        public Library? GetById(int id)
        {
            return _libraries.Find(x => x.Id == id);
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
