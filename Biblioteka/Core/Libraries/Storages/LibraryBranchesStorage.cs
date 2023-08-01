using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Libraries.Storages
{
    public class LibraryBranchStorage
    {
        private const string StoragePath = "../../Data/libraryBranches.csv";

        private readonly Serializer<LibraryBranch> _serializer;

        public LibraryBranchStorage()
        {
            _serializer = new Serializer<LibraryBranch>();
        }

        public List<LibraryBranch> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<LibraryBranch> libraryBranches)
        {
            _serializer.ToCSV(StoragePath, libraryBranches);
        }
    }
}
