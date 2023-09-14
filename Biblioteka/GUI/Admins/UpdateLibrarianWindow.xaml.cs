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
    /// <summary>
    /// Interaction logic for UpdateLibrarianWindow.xaml
    /// </summary>
    public partial class UpdateLibrarianWindow : Window
    {
        public Librarian SelectedLibrarian { get; set; }
        public string SelectedLibrarianTier { get; set; }
        public Librarian Librarian { get; set; }
        public ObservableCollection<LibraryBranch> LibraryBranches { get; set; }
        public List<LibrarianTier> LibrarianTiers { get; set; }
        public string OldLibrarianUsername { get; set; }

        private UserController _userController;
        private LibrarianController _librarianController;
        private LibraryController _libraryController;
        private LibraryBranchController _libraryBranchController;
        public UpdateLibrarianWindow(UserController userController, LibrarianController librarianController, Librarian selectedLibrarian)
        {
            InitializeComponent();
            DataContext = this;

            _userController = userController;
            _librarianController = librarianController;
            _libraryController = new LibraryController();
            _libraryBranchController = new LibraryBranchController();

            Librarian = selectedLibrarian;
            OldLibrarianUsername = selectedLibrarian.Username;
            //Librarian = new Librarian();
            LibrarianTiers = new List<LibrarianTier>(Enum.GetValues(typeof(LibrarianTier)).Cast<LibrarianTier>().ToList());
            LibraryBranches = new ObservableCollection<LibraryBranch>(_libraryBranchController.GetAllLibraryBranches());

            LoadComboBox();

            UsernameTextBox.Text = Librarian.Username;
            PasswordTextBox.Text = Librarian.Password;
        }
        private void LoadComboBox()
        {
            foreach (LibrarianTier librarianTier in LibrarianTiers)
            {
                LibrarianTierComboBox.Items.Add(librarianTier.ToString());
            }
            LibrarianTierComboBox.SelectedValue = Librarian.LibrarianTier.ToString();

            Library library = _libraryController.GetLibraryById(_libraryBranchController.GetLibraryId(Librarian.LibraryBranchId));
            foreach (LibraryBranch libraryBranch in LibraryBranches)
            {
                if (library.Id == libraryBranch.LibraryId)
                {
                    BranchesComboBox.Items.Add(library.Name + ", " + libraryBranch.Location);
                }
            }

            LibraryBranch branch = _libraryBranchController.GetLibraryBranchById(Librarian.LibraryBranchId);
            BranchesComboBox.SelectedValue = library.Name + ", " + branch.Location;
        }

        private void UpdateLibrarianButton_Click(object sender, RoutedEventArgs e)
        {
            int libraryBranchId = LibraryBranches[BranchesComboBox.SelectedIndex].Id;
            Librarian.UserType = UserType.Librarian;
            Enum.TryParse<LibrarianTier>(SelectedLibrarianTier, out LibrarianTier librarianTier);
            Librarian.LibrarianTier = librarianTier;

            if (!IsLibrarianValid())
                return;

            _librarianController.Update(new Librarian(Librarian.Username, Librarian.Password, libraryBranchId, Librarian.LibrarianTier));
            _userController.Update(new User(Librarian.UserType, Librarian.Username, Librarian.Password));
            MessageBox.Show("Librarian successfully updated.", "Success");
        }

        private bool IsLibrarianValid()
        {
            if (Librarian.Username.Length < 3 || Librarian.Username.Length > 20)
            {
                MessageBox.Show("Username must be between 3 and 20 characters long.", "Error");
                return false;
            }
            if (_userController.IsAlreadyTaken(Librarian.Username) && OldLibrarianUsername != Librarian.Username)
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
    }
}
