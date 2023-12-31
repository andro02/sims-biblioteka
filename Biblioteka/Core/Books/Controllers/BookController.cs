﻿using Biblioteka.Core.BookCopys.Controllers;
using Biblioteka.Core.Books.DAOs;
using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Borrows.DAOs;
using Biblioteka.Core.Libraries.Controllers;
using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Biblioteka.Core.Books.Controllers
{
    public class BookController
    {
        private BookDAO _books;

        public BookController()
        {
            _books = new BookDAO();
        }

        public List<Book> GetAllBooks()
        {
            return _books.GetAll();
        }

        public Book? GetBookByISBN(string isbn)
        {
            return _books.GetByISBN(isbn);
        }

        public bool IsAlreadyTaken(string isbn)
        {
            Book? book = GetBookByISBN(isbn);
            if (book == null)
                return false;
            return true;
        }

        public List<MostReadBook> GetMostReadBooks(DateTime? start, DateTime? end)
        {
            BorrowDAO _borrows = new BorrowDAO();

            Dictionary<Book,int> countByBook = new Dictionary<Book,int>();
            BookCopyController bookCopyController = new BookCopyController();

            foreach (Borrow borrow in _borrows.GetAll())
            {
                if (start != null && end != null)
                {
                    if(borrow.ReturnBy > start && borrow.ReturnBy < end)
                    {
                        if (borrow.IsReturned)
                        {
                            Book book = this.GetBookByISBN(bookCopyController.GetBookCopyById(borrow.BookCopyId).ISBN);
                            if (!countByBook.ContainsKey(book))
                            {
                                countByBook.Add(book, 0);
                            }
                            countByBook[book] += 1;
                        }
                    }
                }
                else
                {
                    if (borrow.IsReturned)
                    {
                        Book book = this.GetBookByISBN(bookCopyController.GetBookCopyById(borrow.BookCopyId).ISBN);
                        if (!countByBook.ContainsKey(book))
                        {
                            countByBook.Add(book, 0);
                        }
                        countByBook[book] += 1;
                    }
                }
            }
            List<MostReadBook> mostReadBooks = new List<MostReadBook>();
            foreach (var item in countByBook)
            {
                mostReadBooks.Add(new MostReadBook(item.Key,item.Value));
            }
            return mostReadBooks.OrderByDescending(x => x.Count).ToList();
        }

        public List<Book> GetBooks(int libraryBranchId, string isbnFilter, string titleFilter, string authorsFilter, string descriptionFilter)
        {
            List<Book> books = new List<Book>();
            BookCopyController bookCopyController = new BookCopyController();
            foreach (BookCopy bookCopy in bookCopyController.GetAllBookCopies())
            {
                if (bookCopy.LibraryBranchId == libraryBranchId)
                {
                    Book? book = GetBookByISBN(bookCopy.ISBN);
                    if (book != null)
                        if (!books.Contains(book))
                        {
                            if (isbnFilter != null)
                                if (!book.ISBN.ToLower().Contains(isbnFilter.ToLower()))
                                    continue;
                            if (titleFilter != null)
                                if (!book.Title.ToLower().Contains(titleFilter.ToLower()))
                                    continue;
                            if (authorsFilter != null)
                                if (!book.Authors.ToLower().Contains(authorsFilter.ToLower()))
                                    continue;
                            if (descriptionFilter != null)
                                if (!book.Description.ToLower().Contains(descriptionFilter.ToLower()))
                                    continue;
                            books.Add(book);
                        }
                }
            }
            return books;
        }

        public void Create(Book book)
        {
            _books.Add(book);
        }

        public void Delete(Book book)
        {
            _books.Remove(book);
        }

        public void Subscribe(IObserver observer)
        {
            _books.Subscribe(observer);
        }
    }
}
