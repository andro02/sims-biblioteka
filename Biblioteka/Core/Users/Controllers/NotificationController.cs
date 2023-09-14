using Biblioteka.Core.BookCopys.Controllers;
using Biblioteka.Core.Books.DAOs;
using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Users.DAOs;
using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.Controllers
{
    public class NotificationController
    {
        private NotificationDAO _notifications;

        public NotificationController()
        {
            _notifications = new NotificationDAO();
        }

        public List<Notification> GetAllNotifications()
        {
            return _notifications.GetAll();
        }

        public Notification? GetNotificationById(int id)
        {
            return _notifications.GetById(id);
        }

       
        public void Create(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void Delete(Notification notification)
        {
            _notifications.Remove(notification);
        }

        public void Subscribe(IObserver observer)
        {
            _notifications.Subscribe(observer);
        }
    }
}
