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
    public class UserController
    {
        private UserDAO _users;

        public UserController()
        {
            _users = new UserDAO();
        }

        public List<User> GetAllUsers()
        {
            return _users.GetAll();
        }

        public User? GetUserByUsername(string username)
        {
            return _users.GetByUsername(username);
        }

        public User? TryLogin(string username, string password)
        {
            User? user = GetUserByUsername(username);

            if (user == null)
                return null;
            if (user.Password != password)
                return null;
            return user;
        }

        public void Create(User user)
        {
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }

        public void Subscribe(IObserver observer)
        {
            _users.Subscribe(observer);
        }
    }
}
