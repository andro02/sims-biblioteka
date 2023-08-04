using Biblioteka.Core.BookCopys.Controllers;
using Biblioteka.Core.Books.DAOs;
using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Borrows.DAOs;
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

        public DeadlineExtensionRequest? GetDeadlineExtensionRequest(int id)
        {
            return _deadlineExtensionRequests.GetById(id);
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

            int borrowId = deadlineExtensionRequest.BorrowId;
            Borrow borrow = borrowController.GetBorrowById(borrowId);
            borrow.ReturnBy = borrow.ReturnBy.AddDays(7);
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
