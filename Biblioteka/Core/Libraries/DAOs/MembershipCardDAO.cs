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

        public int NextId(MembershipCard membershipCard)
        {
            if (_membershipCards.Where(x => x.LibraryId == membershipCard.LibraryId).Count() > 0)
                return _membershipCards.Where(x => x.LibraryId == membershipCard.LibraryId).Max(x => x.Id) + 1;
            return 1;
        }

        public void Add(MembershipCard membershipCard)
        {
            membershipCard.Id = NextId(membershipCard);
            _membershipCards.Add(membershipCard);
            _storage.Save(_membershipCards);
            NotifyObservers();
        }

        public void Change(MembershipCard membershipCard)
        {
            _storage.Save(_membershipCards);
            NotifyObservers();
        }

        public void Remove(MembershipCard membershipCard)
        {
            _membershipCards.Remove(membershipCard);
            _storage.Save(_membershipCards);
            NotifyObservers();
        }

        public List<MembershipCard> GetAll()
        {
            return _membershipCards;
        }

        public MembershipCard? GetById(string clientUsername, int libraryId)
        {
            return _membershipCards.Find(x => x.ClientUsername == clientUsername && x.LibraryId == libraryId);
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
