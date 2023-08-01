using Biblioteka.Core.BookCopys.DAOs;
using Biblioteka.Core.Books.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.BookCopys.Controllers
{
    public class BookCopyController
    {
        private BookCopyDAO _bookCopies;

        public BookCopyController()
        {
            _bookCopies = new BookCopyDAO();
        }

        public List<BookCopy> GetAllBookCopies()
        {
            return _bookCopies.GetAll();
        }

        public BookCopy? GetBookCopyById(int id)
        {
            return _bookCopies.GetById(id);
        }

        public void Create(BookCopy BookCopy)
        {
            _bookCopies.Add(BookCopy);
        }

        public void Delete(BookCopy BookCopy)
        {
            _bookCopies.Remove(BookCopy);
        }

        public void Subscribe(IObserver observer)
        {
            _bookCopies.Subscribe(observer);
        }
    }
}
