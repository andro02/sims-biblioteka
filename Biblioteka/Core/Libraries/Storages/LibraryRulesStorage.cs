using Biblioteka.Core.Libraries.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Libraries.Storages
{
    public class LibraryRulesStorage
    {
        private const string StoragePath = "../../Data/libraryRules.csv";

        private readonly Serializer<LibraryRules> _serializer;

        public LibraryRulesStorage()
        {
            _serializer = new Serializer<LibraryRules>();
        }

        public List<LibraryRules> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<LibraryRules> libraryRules)
        {
            _serializer.ToCSV(StoragePath, libraryRules);
        }
    }
}
