using Biblioteka.Core.Books.Controllers;
using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Libraries.Controllers;
using Biblioteka.Core.Libraries.Models;
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
    /// Interaction logic for BookReturnsInformationWindow.xaml
    /// </summary>
    public partial class BookReturnsInformationWindow : Window, IObserver
    {
        public ObservableCollection<Borrow> Borrows { get; set; }
        public Borrow SelectedBorrow { get; set; }

        private BorrowController _borrowController;
        private Librarian _librarian;
        public BookReturnsInformationWindow(Librarian librarian)
        {
            InitializeComponent();
            DataContext = this;

            _borrowController = new BorrowController();
            _borrowController.Subscribe(this);
            _librarian = librarian;

            Borrows = new ObservableCollection<Borrow>(_borrowController.GetLibraryBranchBorrows(_librarian.LibraryBranchId));
        }

        private void BorrowsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedBorrow == null)
            {
                ReturnedButton.IsEnabled = false;
                return;
            }
            ReturnedButton.IsEnabled = true;
        }

        private void ReturnedButton_Click(object sender, RoutedEventArgs e)
        {
            _borrowController.ReturnBookCopy(SelectedBorrow);
            MessageBox.Show("Book copy returned successfully.", "Success");
        }

        public void UpdateBorrowsList()
        {
            Borrows.Clear();
            foreach (Borrow borrow in _borrowController.GetLibraryBranchBorrows(_librarian.LibraryBranchId))
            {
                Borrows.Add(borrow);
            }
        }

        public void Update()
        {
            UpdateBorrowsList();
        }
    }
}
