using Biblioteka.Core.Libraries.Models;
using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Libraries.Storages
{
    public class LibraryStorage
    {
        private const string StoragePath = "../../Data/libraries.csv";

        private readonly Serializer<Library> _serializer;

        public LibraryStorage()
        {
            _serializer = new Serializer<Library>();
        }

        public List<Library> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Library> libraries)
        {
            _serializer.ToCSV(StoragePath, libraries);
        }
    }
}
