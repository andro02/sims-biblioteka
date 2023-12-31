﻿using Biblioteka.Core.Books.Controllers;
using Biblioteka.Core.Users.Controllers;
using Biblioteka.Core.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biblioteka.GUI.Librarians.LibrariansSecondTier
{
    /// <summary>
    /// Interaction logic for LibrarianSecondTierHomeWindow.xaml
    /// </summary>
    public partial class LibrarianSecondTierHomeWindow : Window
    {
        private ReservationController _reservationController;
        private Librarian _librarian;

        public LibrarianSecondTierHomeWindow(Librarian librarian, ReservationController reservationController)
        {
            InitializeComponent();
            DataContext = this;

            _reservationController = reservationController;
            _librarian = librarian;
        }

        private void AddNewBookButton_Click(object sender, RoutedEventArgs e)
        {
            NewBookInformationWindow newBookInformationWindow = new NewBookInformationWindow();
            newBookInformationWindow.Show();
        }

        private void AddNewBookCopyButton_Click(object sender, RoutedEventArgs e)
        {
            NewBookCopyInformationWindow newBookCopyInformationWindow = new NewBookCopyInformationWindow(_librarian);
            newBookCopyInformationWindow.Show();
        }

        private void BookBorrowingButton_Click(object sender, RoutedEventArgs e)
        {
            BookBorrowingInformationWindow bookBorrowingInformationWindow = new BookBorrowingInformationWindow(_librarian, _reservationController);
            bookBorrowingInformationWindow.Show();
        }

        private void ViewDeadlineExtensionRequestsButton_Click(object sender, RoutedEventArgs e)
        {
            DeadlineExtensionRequestsWindow deadlineExtensionRequestsWindow = new DeadlineExtensionRequestsWindow(_librarian);
            deadlineExtensionRequestsWindow.Show();
        }

        private void BookReturnsButton_Click(object sender, RoutedEventArgs e)
        {
            BookReturnsInformationWindow bookReturnsInformationWindow = new BookReturnsInformationWindow(_librarian);
            bookReturnsInformationWindow.Show();
        }
    }
}
