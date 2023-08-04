using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Books.Storages;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.DAOs
{
    public class DeadlineExtensionRequestDAO : ISubject
    {
        private List<IObserver> _observers;
        private DeadlineExtensionRequestStorage _storage;
        private List<DeadlineExtensionRequest> _deadlineExtensionRequests;

        public DeadlineExtensionRequestDAO()
        {
            _storage = new DeadlineExtensionRequestStorage();
            _deadlineExtensionRequests = _storage.Load();
            _observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (_deadlineExtensionRequests.Count > 0)
                return _deadlineExtensionRequests.Max(x => x.Id) + 1;
            return 1;
        }

        public void Add(DeadlineExtensionRequest deadlineExtensionRequest)
        {
            deadlineExtensionRequest.Id = NextId();
            _deadlineExtensionRequests.Add(deadlineExtensionRequest);
            _storage.Save(_deadlineExtensionRequests);
            NotifyObservers();
        }

        public void Remove(DeadlineExtensionRequest deadlineExtensionRequest)
        {
            _deadlineExtensionRequests.Remove(deadlineExtensionRequest);
            _storage.Save(_deadlineExtensionRequests);
            NotifyObservers();
        }

        public List<DeadlineExtensionRequest> GetAll()
        {
            return _deadlineExtensionRequests;
        }

        public DeadlineExtensionRequest? GetById(int id)
        {
            return _deadlineExtensionRequests.Find(x => x.Id == id);
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
