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
    public class ClientController
    {
        private ClientDAO _clients;

        public ClientController()
        {
            _clients = new ClientDAO();
        }

        public List<Client> GetAllClients()
        {
            return _clients.GetAll();
        }

        public Client? GetClientByUsername(string username)
        {
            return _clients.GetByUsername(username);
        }

        public bool IsAlreadyTaken(string username)
        {
            Client? client = GetClientByUsername(username);
            if (client == null)
                return false;
            return true;
        }

        public void Create(Client client)
        {
            _clients.Add(client);
        }

        public void Delete(Client client)
        {
            _clients.Remove(client);
        }

        public void Subscribe(IObserver observer)
        {
            _clients.Subscribe(observer);
        }
    }
}
