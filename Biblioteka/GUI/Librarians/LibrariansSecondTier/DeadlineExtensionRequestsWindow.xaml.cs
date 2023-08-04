using Biblioteka.Core.BookCopys.Controllers;
using Biblioteka.Core.Books.Controllers;
using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Observer;
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

namespace Biblioteka.GUI.Librarians.LibrariansSecondTier
{
    /// <summary>
    /// Interaction logic for DeadlineExtensionRequestsWindow.xaml
    /// </summary>
    public partial class DeadlineExtensionRequestsWindow : Window, IObserver
    {
        public ObservableCollection<DeadlineExtensionRequest> DeadlineExtensionRequests { get; set; }  
        public DeadlineExtensionRequest SelectedDeadlineExtensionRequest { get; set; }

        private Librarian _librarian;
        private DeadlineExtensionRequestController _deadlineExtensionRequestController;
        private BorrowController _borrowController;
        public DeadlineExtensionRequestsWindow(Librarian librarian)
        {
            InitializeComponent();
            DataContext = this;

            _deadlineExtensionRequestController = new DeadlineExtensionRequestController();
            _deadlineExtensionRequestController.Subscribe(this);
            _borrowController = new BorrowController();

            DeadlineExtensionRequests = new ObservableCollection<DeadlineExtensionRequest>(_deadlineExtensionRequestController.GetLibraryBranchDeadlineExtensionRequests(librarian.LibraryBranchId));
        }

        private void DeadlineExtensionRequestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedDeadlineExtensionRequest == null)
            {
                AcceptRequestButton.IsEnabled = false;
                RejectRequestButton.IsEnabled = false;
                return;
            }
            AcceptRequestButton.IsEnabled = true;
            RejectRequestButton.IsEnabled = true;
        }

        private void AcceptRequestButton_Click(object sender, RoutedEventArgs e)
        {
            _deadlineExtensionRequestController.ExtendBorrowDeadline(SelectedDeadlineExtensionRequest);
            MessageBox.Show("Deadline extension request successfully accepted.", "Success");
        }

        private void RejectRequestButton_Click(object sender, RoutedEventArgs e)
        {
            _deadlineExtensionRequestController.Delete(SelectedDeadlineExtensionRequest);
            MessageBox.Show("Deadline extension request successfully rejected.", "Success");
        }
        private void UpdateDeadlineExtensionRequestsList()
        {
            DeadlineExtensionRequests.Clear();
            if (SelectedDeadlineExtensionRequest == null)
                return;
            foreach (DeadlineExtensionRequest deadlineExtensionRequest in _deadlineExtensionRequestController.GetLibraryBranchDeadlineExtensionRequests(_librarian.LibraryBranchId))
            {
                DeadlineExtensionRequests.Add(deadlineExtensionRequest);
            }
        }

        public void Update()
        {
            UpdateDeadlineExtensionRequestsList();
        }
    }
}
