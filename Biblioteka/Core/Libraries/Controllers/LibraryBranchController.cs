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

        public int GetLibraryId(int libraryBranchId)
        {
            LibraryBranch? libraryBranch = GetLibraryBranchById(libraryBranchId);
            if (libraryBranch == null)
                return -1;
            return libraryBranch.LibraryId;
        }

        public List<LibraryBranch> GetLibraryBranches(int libraryId, string locationFilter)
        {
            List<LibraryBranch> libraryBranches = new List<LibraryBranch>();
            foreach (LibraryBranch libraryBranch in GetAllLibraryBranches())
            {
                if (libraryBranch.LibraryId == libraryId)
                    if (locationFilter == null)
                        libraryBranches.Add(libraryBranch);
                    else if (libraryBranch.Location.ToLower().Contains(locationFilter.ToLower()))
                        libraryBranches.Add(libraryBranch);
            }
            return libraryBranches;
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
