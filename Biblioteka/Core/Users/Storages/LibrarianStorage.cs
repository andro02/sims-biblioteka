using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.Storages
{
    public class LibrarianStorage
    {
        private const string StoragePath = "../../Data/librarians.csv";

        private readonly Serializer<Librarian> _serializer;

        public LibrarianStorage()
        {
            _serializer = new Serializer<Librarian>();
        }

        public List<Librarian> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Librarian> Librarians)
        {
            _serializer.ToCSV(StoragePath, Librarians);
        }
    }
}
