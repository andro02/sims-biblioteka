using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Biblioteka.Core.Books.Models
{
    public class MostReadBook
    {
        public Book Book { get; set; }
        public int Count { get; set; }
        public MostReadBook()
        {
            Book = new Book();
            Count = 0;
        }

        public MostReadBook(Book book, int count)
        {
            this.Book = book;
            this.Count = count;
        }
    }
}
