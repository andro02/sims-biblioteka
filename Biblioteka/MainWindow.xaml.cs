using Biblioteka.Core.Books.Controllers;
using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Users;
using Biblioteka.Core.Users.Controllers;
using Biblioteka.Core.Users.Models;
using Biblioteka.GUI.Admins;
using Biblioteka.GUI.Librarians;
using Biblioteka.GUI.Librarians.LibrariansSecondTier;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Username { get; set; }
        public string Password { get; set; }

        private UserController _userController;
        private LibrarianController _librarianController;

        public MainWindow()
        {
            InitializeComponent();
            UsernameTextBox.Focus();
            DataContext = this;

            Username = string.Empty;
            Password = string.Empty;

            _userController = new UserController();
            _librarianController = new LibrarianController();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            User? user = _userController.TryLogin(Username, Password);
            if (user == null)
            {
                MessageBox.Show("Incorrect username or password. Please, try again.", "Error");
                return;
            }
            switch (user.UserType)
            {
                case UserType.Librarian:
                    {
                        Librarian librarian = _librarianController.GetLibrarianByUsername(Username);
                        if (librarian.LibrarianTier == LibrarianTier.First)
                        {
                            LibrarianFirstTierHomeWindow librarianFirstTierHomeWindow = new LibrarianFirstTierHomeWindow(_userController);
                            librarianFirstTierHomeWindow.Show();
                        }
                        else
                        {
                            LibrarianSecondTierHomeWindow librarianSecondTierHomeWindow = new LibrarianSecondTierHomeWindow(librarian);
                            librarianSecondTierHomeWindow.Show();
                        }
                        break;
                    }
                case UserType.Admin:
                    {
                        AdminHomeWindow adminHomeWindow = new AdminHomeWindow();
                        adminHomeWindow.Show();
                        break;
                    }
            }
        }
    }
}
