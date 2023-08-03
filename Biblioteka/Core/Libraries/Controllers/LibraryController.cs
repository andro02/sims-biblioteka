using Biblioteka.Core.Libraries.DAOs;
using Biblioteka.Core.Libraries.Models;
using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Libraries.Controllers
{
    public class LibraryController
    {
        private LibraryDAO _libraries;

        public LibraryController()
        {
            _libraries = new LibraryDAO();
        }

        public List<Library> GetAllLibraries()
        {
            return _libraries.GetAll();
        }

        public Library? GetLibraryById(int id)
        {
            return _libraries.GetById(id);
        }

        public List<Library> GetFilteredLibraries(string nameFilter)
        {
            List<Library> libraries = new List<Library>();
            foreach (Library library in GetAllLibraries()) 
            { 
                if (library.Name.ToLower().Contains(nameFilter.ToLower()))
                    libraries.Add(library);
            }
            return libraries;
        }

        public void Create(Library librariess)
        {
            _libraries.Add(librariess);
        }

        public void Delete(Library librariess)
        {
            _libraries.Remove(librariess);
        }

        public void Subscribe(IObserver observer)
        {
            _libraries.Subscribe(observer);
        }
    }
}
