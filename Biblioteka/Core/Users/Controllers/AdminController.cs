using Biblioteka.Core.Admins.DAOs;
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
    public class AdminController
    {
        private AdminDAO _admins;

        public AdminController()
        {
            _admins = new AdminDAO();
        }

        public List<Admin> GetAllAdmins()
        {
            return _admins.GetAll();
        }

        public Admin? GetAdminByUsername(string username)
        {
            return _admins.GetByUsername(username);
        }

        public void Create(Admin admin)
        {
            _admins.Add(admin);
        }

        public void Delete(Admin admin)
        {
            _admins.Remove(admin);
        }

        public void Subscribe(IObserver observer)
        {
            _admins.Subscribe(observer);
        }
    }
}
