using Biblioteka.Core.Books.Controllers;
using Biblioteka.Core.Books.Models;
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

namespace Biblioteka.GUI.Clients
{
    /// <summary>
    /// Interaction logic for ViewBorrowsWindow.xaml
    /// </summary>
    public partial class ViewBorrowsWindow : Window
    {
        public List<Borrow> Borrows { get; set; }
        public Borrow SelectedBorrow { get; set; }

        private Client _client;
        private BorrowController _borrowController;
        private DeadlineExtensionRequestController _deadlineExtensionRequestController;

        public ViewBorrowsWindow(Client client)
        {
            InitializeComponent();
            DataContext = this;

            _client = client;
            _borrowController = new BorrowController();
            _deadlineExtensionRequestController = new DeadlineExtensionRequestController();

            Borrows = new List<Borrow>(_borrowController.GetClientBorrows(client.Username));
        }

        private void BorrowsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedBorrow == null)
            {
                ExtendDeadlineButton.IsEnabled = false;
                return;
            }
            ExtendDeadlineButton.IsEnabled = true; 
        }

        private void ExtendDeadlineButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBorrow.IsReturned == true)
            {
                MessageBox.Show("Book copy has already been returned.", "Error");
                return;
            }
            _deadlineExtensionRequestController.Create(new DeadlineExtensionRequest(-1, SelectedBorrow.Id, DateTime.Now));
            MessageBox.Show("Request sent successfully.", "Success");
        }
    }
}
