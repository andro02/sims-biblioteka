using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.Models
{
    public class BookCopy : ISerializable
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int LibraryBranchId { get; set; }

        public BookCopy()
        {
            this.Id = -1;
            this.BookId = -1;
            this.LibraryBranchId = -1;
        }

        public BookCopy(int id, int bookId, int libraryBranchId)
        {
            this.Id = id;
            this.BookId = bookId;
            this.LibraryBranchId = libraryBranchId;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.BookId.ToString(), this.LibraryBranchId.ToString() };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.BookId = int.Parse(values[1]);
            this.LibraryBranchId = int.Parse(values[2]);
        }

        public override string ToString()
        {
            return $"{this.Id} {this.BookId} {this.LibraryBranchId}";
        }
    }
}
