using Biblioteka.Core.Books.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.Storages
{
    public class ReservationStorage
    {
        private const string StoragePath = "../../Data/reservations.csv";

        private readonly Serializer<Reservation> _serializer;

        public ReservationStorage()
        {
            _serializer = new Serializer<Reservation>();
        }

        public List<Reservation> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Reservation> reservations)
        {
            _serializer.ToCSV(StoragePath, reservations);
        }
    }
}
