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
    public class ClientDAO : ISubject
    {
        private List<IObserver> _observers;
        private ClientStorage _storage;
        private List<Client> _clients;

        public ClientDAO()
        {
            _storage = new ClientStorage();
            _clients = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Add(Client user)
        {
            _clients.Add(user);
            _storage.Save(_clients);
            NotifyObservers();
        }

        public void Remove(Client user)
        {
            _clients.Remove(user);
            _storage.Save(_clients);
            NotifyObservers();
        }

        public List<Client> GetAll()
        {
            return _clients;
        }

        public Client? GetByUsername(string username)
        {
            return _clients.Find(x => x.Username == username);
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
