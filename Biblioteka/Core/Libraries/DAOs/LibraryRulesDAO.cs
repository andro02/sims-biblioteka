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
    public class LibraryRulesDAO : ISubject
    {
        private List<IObserver> _observers;
        private LibraryRulesStorage _storage;
        private List<LibraryRules> _libraryRules;

        public LibraryRulesDAO()
        {
            _storage = new LibraryRulesStorage();
            _libraryRules = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_libraryRules.Count > 0)
                return _libraryRules.Max(x => x.Id) + 1;
            return 1;
        }

        public void Add(LibraryRules libraryRules)
        {
            libraryRules.Id = NextId();
            _libraryRules.Add(libraryRules);
            _storage.Save(_libraryRules);
            NotifyObservers();
        }

        public void Remove(LibraryRules libraryRules)
        {
            _libraryRules.Remove(libraryRules);
            _storage.Save(_libraryRules);
            NotifyObservers();
        }

        public List<LibraryRules> GetAll()
        {
            return _libraryRules;
        }

        public LibraryRules? GetByLibraryId(int libraryId)
        {
            return _libraryRules.Find(x => x.LibraryId == libraryId);
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
