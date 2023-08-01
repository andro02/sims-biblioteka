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

        public BookCopy()
        {
            this.Id = -1;
            this.BookId = -1;
        }

        public BookCopy(int id, int bookId)
        {
            this.Id = id;
            this.BookId = bookId;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.BookId.ToString() };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.BookId = int.Parse(values[1]);
        }

        public override string ToString()
        {
            return $"{this.Id} {this.BookId}";
        }
    }
}
