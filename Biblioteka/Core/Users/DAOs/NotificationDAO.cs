using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Books.Storages;
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
    public class NotificationDAO
    {
        private List<IObserver> _observers;
        private NotificationStorage _storage;
        private List<Notification> _notifications;

        public NotificationDAO()
        {
            _storage = new NotificationStorage();
            _notifications = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_notifications.Count > 0)
                return _notifications.Max(x => x.Id) + 1;
            return 1;
        }

        public void Add(Notification notification)
        {
            notification.Id = NextId();
            _notifications.Add(notification);
            _storage.Save(_notifications);
            NotifyObservers();
        }

        public void Remove(Notification notification)
        {
            _notifications.Remove(notification);
            _storage.Save(_notifications);
            NotifyObservers();
        }

        public List<Notification> GetAll()
        {
            return _notifications;
        }

        public Notification? GetById(int id)
        {
            return _notifications.Find(x => x.Id == id);
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
