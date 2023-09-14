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

namespace Biblioteka.GUI.Admins
{
    /// <summary>
    /// Interaction logic for AdminHomeWindow.xaml
    /// </summary>
    public partial class AdminHomeWindow : Window
    {
        private UserController _userController;
        public AdminHomeWindow(UserController userController)
        {
            InitializeComponent();
            DataContext = this;

            _userController = userController;
        }

        private void AddNewLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            NewLibraryWindow newLibraryWindow = new NewLibraryWindow();
            newLibraryWindow.Show();
        }

        private void AddNewLibraryBranchButton_Click(object sender, RoutedEventArgs e)
        {
            NewLibraryBranchWindow newLibraryBranchWindow = new NewLibraryBranchWindow();
            newLibraryBranchWindow.Show();
        }

        private void LibrariansButton_Click(object sender, RoutedEventArgs e)
        {
            LibrariansWindow librariansWindow = new LibrariansWindow(_userController);
            librariansWindow.Show();
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            MostReadBooksWindow mostReadBooksWindow = new MostReadBooksWindow();
            mostReadBooksWindow.Show();
        }

    }
}
