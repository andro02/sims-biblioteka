using Biblioteka.Core.BookCopys.Controllers;
using Biblioteka.Core.Books.DAOs;
using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Borrows.DAOs;
using Biblioteka.Core.Libraries.Controllers;
using Biblioteka.Core.Users.Controllers;
using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.Controllers
{
    public class BorrowController
    {
        private BorrowDAO _borrows;

        public BorrowController()
        {
            _borrows = new BorrowDAO();
        }

        public List<Borrow> GetAllBorrows()
        {
            return _borrows.GetAll();
        }

        public Borrow? GetBorrowById(int id)
        {
            return _borrows.GetById(id);
        }

        public List<Borrow> GetClientBorrows(string username)
        {
            List<Borrow> borrows = new List<Borrow>();
            foreach (Borrow borrow in GetAllBorrows())
            {
                if (borrow.ClientUsername == username)
                    borrows.Add(borrow);
            }
            return borrows;
        }

        public List<Borrow> GetLibraryBranchBorrows(int libraryBranchId)
        {
            BookCopyController bookCopyController = new BookCopyController();

            List<Borrow> borrows = new List<Borrow>();
            foreach (Borrow borrow in GetAllBorrows())
            {
                if (borrow.IsReturned == true)
                    continue;

                BookCopy bookCopy = bookCopyController.GetBookCopyById(borrow.BookCopyId);

                if (bookCopy.LibraryBranchId == libraryBranchId)
                    borrows.Add(borrow);
            }
            return borrows;
        }

        public void ReturnBookCopy(Borrow borrow)
        {
            BookCopyController  bookCopyController = new BookCopyController();

            borrow.IsReturned = true;
            Update(borrow);

            BookCopy bookCopy = bookCopyController.GetBookCopyById(borrow.BookCopyId);
            bookCopy.Status = BookCopyStatus.Available;
            bookCopyController.Update(bookCopy);
        }

        public int GetBorrowsCount(string username)
        {
            int borrowsCount = 0;
            foreach (Borrow borrow in GetAllBorrows())
            {
                if (borrow.ClientUsername == username)
                    borrowsCount++;
            }
            return borrowsCount;
        }

        public bool IsAlreadyExtended(Borrow borrow)
        {
            ClientController clientController = new ClientController();
            LibraryRulesController libraryRulesController = new LibraryRulesController();
            BookCopyController bookCopyController = new BookCopyController();
            LibraryBranchController libraryBranchController = new LibraryBranchController();

            BookCopy bookCopy = bookCopyController.GetBookCopyById(borrow.BookCopyId);
            int libraryId = libraryBranchController.GetLibraryId(bookCopy.LibraryBranchId);
            Client client = clientController.GetClientByUsername(borrow.ClientUsername);

            int borrowDurationLimit = libraryRulesController.GetBorrowDurationLimit(libraryId, client.ClientType);
            if ((borrow.ReturnBy - borrow.BorrowedAt).TotalDays > borrowDurationLimit)
                return true;
            return false;
        }

        public void Create(Borrow borrow)
        {
            _borrows.Add(borrow);
        }

        public void Update(Borrow borrow)
        {
            _borrows.Change(borrow);
        }

        public void Delete(Borrow borrow)
        {
            _borrows.Remove(borrow);
        }

        public void Subscribe(IObserver observer)
        {
            _borrows.Subscribe(observer);
        }
    }
}
