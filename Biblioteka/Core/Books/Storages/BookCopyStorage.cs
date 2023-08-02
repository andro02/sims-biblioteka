using Biblioteka.Core.Books.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.BookCopys.Storages
{
    public class BookCopyStorage
    {
        private const string StoragePath = "../../Data/bookCopies.csv";

        private readonly Serializer<BookCopy> _serializer;

        public BookCopyStorage()
        {
            _serializer = new Serializer<BookCopy>();
        }

        public List<BookCopy> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<BookCopy> bookCopies)
        {
            _serializer.ToCSV(StoragePath, bookCopies);
        }
    }
}
