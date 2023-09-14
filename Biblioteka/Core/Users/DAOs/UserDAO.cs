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
    public class UserDAO : ISubject
    {
        private List<IObserver> _observers;
        private UserStorage _storage;
        private List<User> _users;

        public UserDAO()
        {
            _storage = new UserStorage();
            _users = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Add(User user)
        {
            _users.Add(user);
            NotifyObservers();
        }

        public void Change(User user)
        {
            NotifyObservers();
        }

        public void Remove(User user)
        {
            _users.Remove(user);
            NotifyObservers();
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User? GetByUsername(string username)
        {
            return _users.Find(x => x.Username == username);
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
