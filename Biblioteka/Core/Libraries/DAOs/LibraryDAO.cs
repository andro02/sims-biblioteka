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

        public int NextId()
        {
            if (_libraries.Count > 0)
                return _libraries.Max(x => x.Id) + 1;
            return 1;
        }

        public void Add(Library library)
        {
            library.Id = NextId();
            _libraries.Add(library);
            _storage.Save(_libraries);
            NotifyObservers();
        }

        public void Remove(Library library)
        {
            _libraries.Remove(library);
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
