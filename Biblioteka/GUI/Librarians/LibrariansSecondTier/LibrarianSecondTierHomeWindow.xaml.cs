using Biblioteka.Core.Users.Controllers;
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
        private UserController _userController;

        public LibrarianSecondTierHomeWindow(UserController userController)
        {
            InitializeComponent();
            DataContext = this;

            _userController = userController;
        }

        private void AddNewBookButton_Click(object sender, RoutedEventArgs e)
        {
            NewBookInformationWindow newBookInformationWindow = new NewBookInformationWindow();
            newBookInformationWindow.Show();
        }

        private void AddNewBookCopyButton_Click(object sender, RoutedEventArgs e)
        {
            NewBookCopyInformationWindow newBookCopyInformationWindow = new NewBookCopyInformationWindow();
            newBookCopyInformationWindow.Show();
        }
    }
}
