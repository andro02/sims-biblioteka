using Biblioteka.Core.Books.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.Storages
{
    public class BookStorage
    {
        private const string StoragePath = "../../Data/books.csv";

        private readonly Serializer<Book> _serializer;

        public BookStorage()
        {
            _serializer = new Serializer<Book>();
        }

        public List<Book> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Book> books)
        {
            _serializer.ToCSV(StoragePath, books);
        }
    }
}
