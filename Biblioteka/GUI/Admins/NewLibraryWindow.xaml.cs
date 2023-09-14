using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Libraries.Controllers;
using Biblioteka.Core.Libraries.Models;
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
    /// Interaction logic for NewLibraryWindow.xaml
    /// </summary>
    public partial class NewLibraryWindow : Window
    {
        public Library Library { get; set; }
        private LibraryController _libraryController;
        public NewLibraryWindow()
        {
            InitializeComponent();
            DataContext = this;

            Library = new Library();
            _libraryController = new LibraryController();
        }

        private void AddLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLibraryValid())
                return;

            _libraryController.Create(new Library(-1, Library.Name));
            MessageBox.Show("Library successfully added.", "Success");
            NameTextBox.Text = String.Empty;
        }
        private bool IsLibraryValid()
        {
            if (Library.Name.Length == 0)
            {
                MessageBox.Show("No name entered.", "Error");
                return false;
            }
            return true;
        }
    }
}
