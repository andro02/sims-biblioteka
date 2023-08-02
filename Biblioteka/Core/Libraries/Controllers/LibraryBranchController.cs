using Biblioteka.Core.Libraries.DAOs;
using Biblioteka.Core.Libraries.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Libraries.Controllers
{
    public class LibraryBranchController
    {
        private LibraryBranchDAO _libraryBranches;

        public LibraryBranchController()
        {
            _libraryBranches = new LibraryBranchDAO();
        }

        public List<LibraryBranch> GetAllLibraryBranches()
        {
            return _libraryBranches.GetAll();
        }

        public LibraryBranch? GetLibraryBranchById(int id)
        {
            return _libraryBranches.GetById(id);
        }

        public void Create(LibraryBranch libraryBranches)
        {
            _libraryBranches.Add(libraryBranches);
        }

        public void Delete(LibraryBranch libraryBranches)
        {
            _libraryBranches.Remove(libraryBranches);
        }

        public void Subscribe(IObserver observer)
        {
            _libraryBranches.Subscribe(observer);
        }
    }
}
