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

namespace Biblioteka.GUI.Librarians
{
    /// <summary>
    /// Interaction logic for LibrarianFirstTierHomeWindow.xaml
    /// </summary>
    public partial class LibrarianFirstTierHomeWindow : Window
    {
        private UserController _userController;

        public LibrarianFirstTierHomeWindow(UserController userController)
        {
            InitializeComponent();
            DataContext = this;

            _userController = userController;
        }

        private void RegisterNewClientButton_Click(object sender, RoutedEventArgs e)
        {
            NewClientInformationWindow newClientInformationWindow = new NewClientInformationWindow(_userController);
            newClientInformationWindow.Show();
        }
    }
}
