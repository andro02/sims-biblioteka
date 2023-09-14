using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Books.Storages;
using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.DAOs
{ 
    public class ReservationDAO : ISubject
    {
        private List<IObserver> _observers;
        private ReservationStorage _storage;
        private List<Reservation> _reservations;

        public ReservationDAO()
        {
            _storage = new ReservationStorage();
            _reservations = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            if (_reservations.Count > 0)
                return _reservations.Max(x => x.Id) + 1;
            return 1;
        }

        public void Add(Reservation reservation)
        {
            reservation.Id = NextId();
            _reservations.Add(reservation);
            _storage.Save(_reservations);
            NotifyObservers();
        }

        public void Change(Reservation updatedReservation)
        {
            Reservation reservation = _reservations.Find(r => r.Id == updatedReservation.Id);
            if (reservation != null)
            {
                reservation.IsActive = updatedReservation.IsActive;
                reservation.ReservedAt = updatedReservation.ReservedAt;
                _storage.Save(_reservations);
                NotifyObservers();
            }
        }

        public void Remove(Reservation reservation)
        {
            _reservations.Remove(reservation);
            _storage.Save(_reservations);
            NotifyObservers();
        }

        public List<Reservation> GetAll()
        {
            return _reservations;
        }

        public Reservation? GetById(int id)
        {
            return _reservations.Find(x => x.Id == id);
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
