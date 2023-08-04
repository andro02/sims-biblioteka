using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Books.Storages;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Borrows.DAOs
{
    public class BorrowDAO : ISubject
    {
        private List<IObserver> _observers;
        private BorrowStorage _storage;
        private List<Borrow> _borrows;

        public BorrowDAO()
        {
            _storage = new BorrowStorage();
            _borrows = _storage.Load();
            _observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (_borrows.Count > 0)
                return _borrows.Max(x => x.Id) + 1;
            return 1;
        }

        public void Add(Borrow borrow)
        {
            borrow.Id = NextId();
            _borrows.Add(borrow);
            _storage.Save(_borrows);
            NotifyObservers();
        }

        public void Change(Borrow borrow)
        {
            _storage.Save(_borrows);
            NotifyObservers();
        }

        public void Remove(Borrow borrow)
        {
            _borrows.Remove(borrow);
            _storage.Save(_borrows);
            NotifyObservers();
        }

        public List<Borrow> GetAll()
        {
            return _borrows;
        }

        public Borrow? GetById(int id)
        {
            return _borrows.Find(x => x.Id == id);
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
