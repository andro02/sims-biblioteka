using Biblioteka.Core.Libraries.Controllers;
using Biblioteka.Core.Users.Controllers;
using Biblioteka.Core.Users.Models;
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

namespace Biblioteka.GUI.Librarians.LibrariansFirstTier
{
    /// <summary>
    /// Interaction logic for NewMembershipCardInformationWindow.xaml
    /// </summary>
    public partial class NewMembershipCardInformationWindow : Window
    {
        public string ClientUsername { get; set; }

        private ClientController _clientController;
        private MembershipCardController _membershipCardController;
        private LibraryBranchController _libraryBranchController;
        private Librarian _librarian;
        public NewMembershipCardInformationWindow(Librarian librarian)
        {
            InitializeComponent();
            DataContext = this;

            _clientController = new ClientController();
            _membershipCardController = new MembershipCardController();
            _libraryBranchController = new LibraryBranchController();
            _librarian = librarian;
        }

        private void RegisterNewMembershipCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsMembershipCardValid())
                return;

            int libraryId = _libraryBranchController.GetLibraryId(_librarian.LibraryBranchId);
            _membershipCardController.Create(new MembershipCard(-1, ClientUsername, libraryId, DateTime.Now.AddMonths(1)));
            MessageBox.Show("Membership card successfully registered.", "Success");
            ResetInputFields();
        }

        private bool IsMembershipCardValid()
        {
            if (ClientUsername.Length == 0)
            {
                MessageBox.Show("No client username provided.", "Error");
                return false;
            }
            if (!_clientController.IsAlreadyTaken(ClientUsername))
            {
                MessageBox.Show("Provided client username does not exist in our system.", "Error");
                return false;
            }
            int libraryId = _libraryBranchController.GetLibraryId(_librarian.LibraryBranchId);
            if (_membershipCardController.IsAlreadyRegistered(ClientUsername, libraryId))
            {
                MessageBox.Show("Membership card is already registered.", "Error");
                return false;
            }
            return true;
        }

        private void ResetInputFields()
        {
            ClientUsernameTextBox.Text = string.Empty;
        }
    }
}
