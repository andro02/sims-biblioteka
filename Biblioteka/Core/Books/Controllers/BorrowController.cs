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
