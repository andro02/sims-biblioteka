using Biblioteka.Core.Users.Controllers;
using Biblioteka.Core.Users.Models;
using Biblioteka.GUI.Librarians.LibrariansFirstTier;
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

namespace Biblioteka.GUI.Librarians
{
    /// <summary>
    /// Interaction logic for LibrarianFirstTierHomeWindow.xaml
    /// </summary>
    public partial class LibrarianFirstTierHomeWindow : Window
    {
        private UserController _userController;
        private Librarian _librarian;

        public LibrarianFirstTierHomeWindow(UserController userController, Librarian librarian)
        {
            InitializeComponent();
            DataContext = this;

            _userController = userController;
            _librarian = librarian;
        }

        private void RegisterNewClientButton_Click(object sender, RoutedEventArgs e)
        {
            NewClientInformationWindow newClientInformationWindow = new NewClientInformationWindow(_userController);
            newClientInformationWindow.Show();
        }

        private void RegisterNewMembershipCardButton_Click(object sender, RoutedEventArgs e)
        {
            NewMembershipCardInformationWindow newMembershipCardInformationWindow = new NewMembershipCardInformationWindow(_librarian);
            newMembershipCardInformationWindow.Show();
        }

        private void ExtendMembershipButton_Click(object sender, RoutedEventArgs e)
        {
            ExtendMembershipWindow extendMembershipWindow = new ExtendMembershipWindow(_librarian);
            extendMembershipWindow.Show();
        }
    }
}
