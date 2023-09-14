using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.Storages
{
    public class NotificationStorage
    {
        private const string StoragePath = "../../Data/notifications.csv";

        private readonly Serializer<Notification> _serializer;

        public NotificationStorage()
        {
            _serializer = new Serializer<Notification>();
        }

        public List<Notification> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Notification> notifications)
        {
            _serializer.ToCSV(StoragePath, notifications);
        }
    }
}
