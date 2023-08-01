using Biblioteka.Core.Users.Models;
using Biblioteka.Core.Users.Storages;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Admins.DAOs
{
    public class AdminDAO : ISubject
    {
        private List<IObserver> _observers;
        private AdminStorage _storage;
        private List<Admin> _admins;

        public AdminDAO()
        {
            _storage = new AdminStorage();
            _admins = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Add(Admin user)
        {
            _admins.Add(user);
            _storage.Save(_admins);
            NotifyObservers();
        }

        public void Remove(Admin user)
        {
            _admins.Remove(user);
            _storage.Save(_admins);
            NotifyObservers();
        }

        public List<Admin> GetAll()
        {
            return _admins;
        }

        public Admin? GetByUsername(string username)
        {
            return _admins.Find(x => x.Username == username);
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
