using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.Storages
{
    public class ClientStorage
    {
        private const string StoragePath = "../../Data/clients.csv";

        private readonly Serializer<Client> _serializer;

        public ClientStorage()
        {
            _serializer = new Serializer<Client>();
        }

        public List<Client> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Client> Clients)
        {
            _serializer.ToCSV(StoragePath, Clients);
        }
    }
}
