using Biblioteka.Core.Libraries.Controllers;
using Biblioteka.Core.Libraries.Models;
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
    /// <summary>
    /// Interaction logic for NewLibraryBranchWindow.xaml
    /// </summary>
    public partial class NewLibraryBranchWindow : Window
    {
        public LibraryBranch LibraryBranch { get; set; }
        public ObservableCollection<Library> Libraries { get; set; }
        private LibraryBranchController _libraryBranchController;
        private LibraryController _libraryControler;
        public NewLibraryBranchWindow()
        {
            InitializeComponent();
            DataContext = this;

            LibraryBranch = new LibraryBranch();

            _libraryBranchController = new LibraryBranchController();
            _libraryControler = new LibraryController();

            Libraries = new ObservableCollection<Library>(_libraryControler.GetAllLibraries());

            LoadComboBox();
        }

        private void LoadComboBox()
        {
            foreach (Library library in Libraries)
            {
                LibrariesComboBox.Items.Add(library.Name);
            }
            LibrariesComboBox.SelectedValue = LibrariesComboBox.Items[0];
        }

        private void AddNewBranchButton_Click(object sender, RoutedEventArgs e)
        {
            int libraryId = Libraries[LibrariesComboBox.SelectedIndex].Id;

            if (!IsLibraryValid())
                return;

            _libraryBranchController.Create(new LibraryBranch(-1, libraryId, LibraryBranch.Location));
            MessageBox.Show("Library branch successfully added.", "Success");

            LibrariesComboBox.SelectedValue = LibrariesComboBox.Items[0];
            LocationTextBox.Text = String.Empty;

        }
        private bool IsLibraryValid()
        {
            if (LibraryBranch.Location.Length == 0)
            {
                MessageBox.Show("No location entered.", "Error");
                return false;
            }
            return true;
        }
    }
}
