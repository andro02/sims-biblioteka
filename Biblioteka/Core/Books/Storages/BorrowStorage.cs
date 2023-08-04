using Biblioteka.Core.Books.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.Storages
{
    public class BorrowStorage
    {
        private const string StoragePath = "../../Data/borrows.csv";

        private readonly Serializer<Borrow> _serializer;

        public BorrowStorage()
        {
            _serializer = new Serializer<Borrow>();
        }

        public List<Borrow> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<Borrow> borrows)
        {
            _serializer.ToCSV(StoragePath, borrows);
        }
    }
}
