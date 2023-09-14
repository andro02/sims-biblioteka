using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Users.DAOs;
using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users
{
    public class LibrarianController
    {
        private LibrarianDAO _librarians;

        public LibrarianController()
        {
            _librarians = new LibrarianDAO();
        }

        public List<Librarian> GetAllLibrarians()
        {
            return _librarians.GetAll();
        }

        public Librarian? GetLibrarianByUsername(string username)
        {
            return _librarians.GetByUsername(username);
        }

        public void Create(Librarian librarian)
        {
            _librarians.Add(librarian);
        }

        public void Delete(Librarian librarian)
        {
            _librarians.Remove(librarian);
        }
        public void Update(Librarian librarian)
        {
            _librarians.Change(librarian);
        }

        public void Subscribe(IObserver observer)
        {
            _librarians.Subscribe(observer);
        }
    }
}
