using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.Storages
{
    public class AdminStorage
    {
        private const string StoragePath = "../../Data/admins.csv";

        private readonly Serializer<Admin> _serializer;

        public AdminStorage()
        {
            _serializer = new Serializer<Admin>();
        }

        public List<Admin> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Admin> Admins)
        {
            _serializer.ToCSV(StoragePath, Admins);
        }
    }
}
