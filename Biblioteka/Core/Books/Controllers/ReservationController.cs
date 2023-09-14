using Biblioteka.Core.BookCopys.Controllers;
using Biblioteka.Core.Books.DAOs;
using Biblioteka.Core.Books.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.Controllers
{
    public class ReservationController
    {
        private ReservationDAO _reservations;

        public ReservationController()
        {
            _reservations = new ReservationDAO();
        }

        public List<Reservation> GetAllReservations()
        {
            return _reservations.GetAll();
        }

        public Reservation? GetReservationById(int id)
        {
            return _reservations.GetById(id);
        }

        public List<Reservation> GetReservations(bool isActive)
        {
            return GetAllReservations().Where(x => x.IsActive == isActive).OrderBy(x => x.ReservedAt).ToList();
        }

        public Reservation? FindActiveReservation(string clientUsername, string isbn)
        {
            foreach (Reservation reservation in GetAllReservations())
            {
                if (reservation.ClientUsername == clientUsername && reservation.ISBN == isbn && reservation.IsActive)
                    return reservation;
            }
            return null;
        }

        public void Create(Reservation reservation)
        {
            _reservations.Add(reservation);
        }

        public void Update(Reservation reservation)
        {
            _reservations.Change(reservation);
        }

        public void Delete(Reservation reservation)
        {
            _reservations.Remove(reservation);
        }

        public void Subscribe(IObserver observer)
        {
            _reservations.Subscribe(observer);
        }
    }
}
