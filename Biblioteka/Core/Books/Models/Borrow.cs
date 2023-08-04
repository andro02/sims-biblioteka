using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.Models
{
    public class Borrow : ISerializable
    {
        public int Id { get; set; }
        public string ClientUsername { get; set; }
        public int BookCopyId { get; set; }
        public DateTime BorrowedAt { get; set; }
        public DateTime ReturnBy { get; set; }
        public bool IsReturned { get; set; }

        public Borrow()
        {
            this.Id = -1;
            this.ClientUsername = string.Empty;
            this.BookCopyId = -1;
            this.BorrowedAt = DateTime.MinValue;
            this.ReturnBy = DateTime.MinValue;
            this.IsReturned = false;
        }

        public Borrow(int id, string clientUsername, int bookCopyid, DateTime borrowedAt, DateTime returnBy, bool isReturned)
        {
            this.Id = id;
            this.ClientUsername = clientUsername;
            this.BookCopyId = bookCopyid;
            this.BorrowedAt = borrowedAt;
            this.ReturnBy = returnBy;
            this.IsReturned = isReturned;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.ClientUsername, this.BookCopyId.ToString(), this.BorrowedAt.ToString(), this.ReturnBy.ToString(), this.IsReturned.ToString() };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.ClientUsername = values[1];
            this.BookCopyId = int.Parse(values[2]);
            this.BorrowedAt = DateTime.Parse(values[3]);
            this.ReturnBy = DateTime.Parse(values[4]);
            this.IsReturned = bool.Parse(values[5]);
        }

        public override string ToString()
        {
            return $"{this.Id} {this.ClientUsername} {this.BookCopyId} {this.BorrowedAt} {this.ReturnBy} {this.IsReturned}";
        }
    }
}
