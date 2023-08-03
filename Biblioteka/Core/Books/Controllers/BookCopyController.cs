using Biblioteka.Core.BookCopys.DAOs;
using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Users.Models;
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

        public List<BookCopy> GetBookCopies(int libraryBranchId, string isbn, string languageFilter, string formatFilter, string coverTypeFilter, string publisherFilter, string publishingYearFilter)
        {
            List<BookCopy> bookCopies = new List<BookCopy>();
            foreach (BookCopy bookCopy in GetAllBookCopies())
            {
                if (bookCopy.LibraryBranchId == libraryBranchId && bookCopy.ISBN == isbn)
                {
                    if (languageFilter != null)
                        if (!bookCopy.Language.ToLower().Contains(languageFilter.ToLower()))
                            continue;
                    if (formatFilter != null)
                        if (!bookCopy.Format.ToLower().Contains(formatFilter.ToLower()))
                            continue;
                    if (coverTypeFilter != null)
                    {
                        Enum.TryParse<CoverType>(coverTypeFilter, out CoverType coverType);
                        if (bookCopy.CoverType != coverType)
                            continue;
                    }
                    if (publisherFilter != null)
                        if (!bookCopy.Publisher.ToLower().Contains(publisherFilter.ToLower()))
                            continue;
                    if (publishingYearFilter != null)
                        if (!bookCopy.PublishingYear.ToString().ToLower().Contains(publishingYearFilter.ToLower()))
                            continue;
                    bookCopies.Add(bookCopy);
                }
            }
            return bookCopies;
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
