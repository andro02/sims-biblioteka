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
    public class LibraryBranchDAO : ISubject
    {
        private List<IObserver> _observers;
        private LibraryBranchStorage _storage;
        private List<LibraryBranch> _libraryBranches;

        public LibraryBranchDAO()
        {
            _storage = new LibraryBranchStorage();
            _libraryBranches = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Add(LibraryBranch user)
        {
            _libraryBranches.Add(user);
            _storage.Save(_libraryBranches);
            NotifyObservers();
        }

        public void Remove(LibraryBranch user)
        {
            _libraryBranches.Remove(user);
            _storage.Save(_libraryBranches);
            NotifyObservers();
        }

        public List<LibraryBranch> GetAll()
        {
            return _libraryBranches;
        }

        public LibraryBranch? GetById(int id)
        {
            return _libraryBranches.Find(x => x.Id == id);
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
