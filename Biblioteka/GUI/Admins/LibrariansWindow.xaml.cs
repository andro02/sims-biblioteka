using Biblioteka.Core.Books.Controllers;
using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Libraries.Models;
using Biblioteka.Core.Users;
using Biblioteka.Core.Users.Controllers;
using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Biblioteka.GUI.Admins
{
    public partial class LibrariansWindow : Window, IObserver
    {
        public Librarian SelectedLibrarian { get; set; }
        public ObservableCollection<Librarian> Librarians { get; set; }

        private UserController _userController;
        private LibrarianController _librarianController;
        public LibrariansWindow(UserController userController)
        {
            InitializeComponent();
            DataContext = this;
            _userController = userController;
            _librarianController = new LibrarianController();
            _librarianController.Subscribe(this);

            Librarians = new ObservableCollection<Librarian>(_librarianController.GetAllLibrarians());
        }
        private void LibrariansDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedLibrarian == null)
            {
                UpdateButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                return;
            }
            UpdateButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
        }

        private void AddNewLibrarian_Click(object sender, RoutedEventArgs e)
        {
            NewLibrarianWindow newLibrarianWindow = new NewLibrarianWindow(_userController);
            newLibrarianWindow.Show();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLibrarian == null)
            {
                MessageBox.Show("Choose librarian to update.");
            }
            else
            {
                UpdateLibrarianWindow updateLibrarianWindow = new UpdateLibrarianWindow(_userController, _librarianController, SelectedLibrarian);
                updateLibrarianWindow.Show();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLibrarian == null)
            {
                MessageBox.Show("Choose librarian to delete.");
            }
            else
            {
                MessageBoxResult result = ConfirmLibrarianDeletion();

                if (result == MessageBoxResult.Yes)
                {
                    string librarianUsername = SelectedLibrarian.Username;
                    _librarianController.Delete(_librarianController.GetLibrarianByUsername(librarianUsername));
                }
            }
        }
        private MessageBoxResult ConfirmLibrarianDeletion()
        {
            string sMessageBoxText = $"Are you sure that you want to delete this librarian?";
            string sCaption = "Confirm";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            return result;
        }
        public void UpdateLibrariansList()
        {
            Librarians.Clear();
            foreach (Librarian librarian in _librarianController.GetAllLibrarians())
            {
                Librarians.Add(librarian);
            }
        }

        public void Update()
        {
            UpdateLibrariansList();
        }

    }
}
