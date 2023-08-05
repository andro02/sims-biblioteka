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
    public class DeadlineExtensionRequestController
    {
        private DeadlineExtensionRequestDAO _deadlineExtensionRequests;

        public DeadlineExtensionRequestController()
        {
            _deadlineExtensionRequests = new DeadlineExtensionRequestDAO();
        }

        public List<DeadlineExtensionRequest> GetAllDeadlineExtensionRequests()
        {
            return _deadlineExtensionRequests.GetAll();
        }

        public DeadlineExtensionRequest? GetDeadlineExtensionRequestById(int id)
        {
            return _deadlineExtensionRequests.GetById(id);
        }

        public DeadlineExtensionRequest? GetDeadlineExtensionRequestByBorrowId(int borrowId)
        {
            foreach (DeadlineExtensionRequest deadlineExtensionRequest in GetAllDeadlineExtensionRequests())
            {
                if (deadlineExtensionRequest.BorrowId == borrowId)
                    return deadlineExtensionRequest;
            }
            return null;
        }

        public bool DoesDeadlineExtensionRequestExist(int borrowId)
        {
            DeadlineExtensionRequest deadlineExtensionRequest = GetDeadlineExtensionRequestByBorrowId(borrowId);
            if (deadlineExtensionRequest == null)
                return false;
            return true;
        }

        public List<DeadlineExtensionRequest> GetLibraryBranchDeadlineExtensionRequests(int libraryBranchId)
        {
            BorrowController borrowController = new BorrowController();
            BookCopyController bookCopyController = new BookCopyController();

            List<DeadlineExtensionRequest> deadlineExtensionRequests = new List<DeadlineExtensionRequest>();

            foreach (DeadlineExtensionRequest deadlineExtensionRequest in GetAllDeadlineExtensionRequests())
            {
                int borrowId = deadlineExtensionRequest.BorrowId;
                Borrow borrow = borrowController.GetBorrowById(borrowId);
                if (borrow.IsReturned == true)
                {
                    Delete(deadlineExtensionRequest);
                    continue;
                }

                int bookCopyId = borrow.BookCopyId;
                BookCopy bookCopy = bookCopyController.GetBookCopyById(bookCopyId);
                
                if (bookCopy.LibraryBranchId == libraryBranchId)
                        deadlineExtensionRequests.Add(deadlineExtensionRequest);
            }

            return deadlineExtensionRequests;
        }

        public void ExtendBorrowDeadline(DeadlineExtensionRequest deadlineExtensionRequest)
        {
            BorrowController borrowController = new BorrowController();
            LibraryRulesController libraryRulesController = new LibraryRulesController();
            ClientController clientController = new ClientController();
            BookCopyController bookCopyController = new BookCopyController();
            LibraryBranchController libraryBranchController = new LibraryBranchController();

            Borrow borrow = borrowController.GetBorrowById(deadlineExtensionRequest.BorrowId);
            BookCopy bookCopy = bookCopyController.GetBookCopyById(borrow.BookCopyId);
            Client client = clientController.GetClientByUsername(borrow.ClientUsername);

            int libraryId = libraryBranchController.GetLibraryId(bookCopy.LibraryBranchId);
            int borrowDurationLimit = libraryRulesController.GetBorrowDurationLimit(libraryId, client.ClientType);

            borrow.ReturnBy = borrow.ReturnBy.AddDays(borrowDurationLimit);
            borrowController.Update(borrow);

            Delete(deadlineExtensionRequest);
        }

        public void Create(DeadlineExtensionRequest deadlineExtensionRequest)
        {
            _deadlineExtensionRequests.Add(deadlineExtensionRequest);
        }

        public void Delete(DeadlineExtensionRequest deadlineExtensionRequest)
        {
            _deadlineExtensionRequests.Remove(deadlineExtensionRequest);
        }

        public void Subscribe(IObserver observer)
        {
            _deadlineExtensionRequests.Subscribe(observer);
        }
    }
}
