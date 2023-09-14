using Biblioteka.Core.Libraries.Controllers;
using Biblioteka.Core.Libraries.Models;
using Biblioteka.Core.Users;
using Biblioteka.Core.Users.Controllers;
using Biblioteka.Core.Users.Models;
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
    public partial class NewLibrarianWindow : Window
    {
        public Librarian Librarian { get; set; }
        public List<LibrarianTier> LibrarianTiers { get; set; }
        public string SelectedLibrarianTier { get; set; }
        public ObservableCollection<LibraryBranch> LibraryBranches { get; set; }

        private UserController _userController;
        private LibrarianController _librarianController;
        private LibraryBranchController _libraryBranchController;
        private LibraryController _libraryController;

        public NewLibrarianWindow(UserController userController)
        {
            InitializeComponent();
            DataContext = this;

            _userController = userController;
            _librarianController = new LibrarianController();
            _libraryBranchController = new LibraryBranchController();
            _libraryController = new LibraryController();

            Librarian = new Librarian();
            LibrarianTiers = new List<LibrarianTier>(Enum.GetValues(typeof(LibrarianTier)).Cast<LibrarianTier>().ToList());
            LibraryBranches = new ObservableCollection<LibraryBranch>(_libraryBranchController.GetAllLibraryBranches());

            LoadComboBox();
        }
        private void LoadComboBox()
        {
            foreach (LibrarianTier clientType in LibrarianTiers)
            {
                LibrarianTierComboBox.Items.Add(clientType.ToString());
            }
            LibrarianTierComboBox.SelectedValue = LibrarianTierComboBox.Items[0];
            SelectedLibrarianTier = LibrarianTierComboBox.Items[0].ToString();

            foreach (LibraryBranch libraryBranch in LibraryBranches)
            {
                Library library = _libraryController.GetLibraryById(_libraryBranchController.GetLibraryId(libraryBranch.Id));
                BranchesComboBox.Items.Add(library.Name + ", " + libraryBranch.Location);
            }
            BranchesComboBox.SelectedValue = BranchesComboBox.Items[0];
        }

        private void AddNewLibrarianButton_Click(object sender, RoutedEventArgs e)
        {
            int libraryBranchId = LibraryBranches[BranchesComboBox.SelectedIndex].Id;

            if (!IsLibrarianValid())
                return;

            Librarian.UserType = UserType.Librarian;
            Enum.TryParse<LibrarianTier>(SelectedLibrarianTier, out LibrarianTier librarianTier);
            Librarian.LibrarianTier = librarianTier;
            _librarianController.Create(new Librarian(Librarian.Username, Librarian.Password, libraryBranchId, Librarian.LibrarianTier));
            _userController.Create(new User(Librarian.UserType, Librarian.Username, Librarian.Password));
            MessageBox.Show("Librarian successfully registered.", "Success");
            ResetInputFields();
        }
        private bool IsLibrarianValid()
        {
            if (Librarian.Username.Length < 3 || Librarian.Username.Length > 20)
            {
                MessageBox.Show("Username must be between 3 and 20 characters long.", "Error");
                return false;
            }
            if (_userController.IsAlreadyTaken(Librarian.Username))
            {
                MessageBox.Show("Username is already taken.", "Error");
                return false;
            }
            if (Librarian.Password.Length < 3 || Librarian.Password.Length > 20)
            {
                MessageBox.Show("Password must be between 3 and 20 characters long.", "Error");
                return false;
            }
            return true;
        }
        private void ResetInputFields()
        {
            UsernameTextBox.Text = string.Empty;
            PasswordTextBox.Text = string.Empty;
            LibrarianTierComboBox.SelectedValue = LibrarianTierComboBox.Items[0];
            SelectedLibrarianTier = LibrarianTierComboBox.Items[0].ToString();
            BranchesComboBox.SelectedValue = BranchesComboBox.Items[0];
        }
    }
}
