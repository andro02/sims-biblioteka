using Biblioteka.Core.Users.Models;
using Biblioteka.Core.Users.Storages;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Libraries.DAOs
{
    public class MembershipCardDAO : ISubject
    {
        private List<IObserver> _observers;
        private MembershipCardStorage _storage;
        private List<MembershipCard> _membershipCards;

        public MembershipCardDAO()
        {
            _storage = new MembershipCardStorage();
            _membershipCards = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Add(MembershipCard user)
        {
            _membershipCards.Add(user);
            _storage.Save(_membershipCards);
            NotifyObservers();
        }

        public void Remove(MembershipCard user)
        {
            _membershipCards.Remove(user);
            _storage.Save(_membershipCards);
            NotifyObservers();
        }

        public List<MembershipCard> GetAll()
        {
            return _membershipCards;
        }

        public MembershipCard? GetById(int id)
        {
            return _membershipCards.Find(x => x.Id == id);
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
