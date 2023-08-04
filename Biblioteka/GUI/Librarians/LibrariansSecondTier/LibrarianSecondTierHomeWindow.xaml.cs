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
        private Librarian _librarian;

        public LibrarianSecondTierHomeWindow(Librarian librarian)
        {
            InitializeComponent();
            DataContext = this;

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
            BookBorrowingInformationWindow bookBorrowingInformationWindow = new BookBorrowingInformationWindow(_librarian);
            bookBorrowingInformationWindow.Show();
        }

        private void ViewDeadlineExtensionRequestsButton_Click(object sender, RoutedEventArgs e)
        {
            DeadlineExtensionRequestsWindow deadlineExtensionRequestsWindow = new DeadlineExtensionRequestsWindow(_librarian);
            deadlineExtensionRequestsWindow.Show();
        }
    }
}
