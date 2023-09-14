using Biblioteka.Core.BookCopys.Controllers;
using Biblioteka.Core.Books.Controllers;
using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Libraries.Controllers;
using Biblioteka.Core.Libraries.Models;
using Biblioteka.Core.Users.Controllers;
using Biblioteka.Core.Users.Models;
using Biblioteka.GUI.Librarians;
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

namespace Biblioteka.GUI.Clients
{
    /// <summary>
    /// Interaction logic for ClientHomeWindow.xaml
    /// </summary>
    public partial class ClientHomeWindow : Window
    {
        public ObservableCollection<Library> Libraries { get; set; }
        public ObservableCollection<LibraryBranch> LibraryBranches { get; set; }
        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<BookCopy> BookCopies { get; set; }
        public Library SelectedLibrary { get; set; }
        public LibraryBranch SelectedLibraryBranch { get; set; }
        public Book SelectedBook { get; set; }
        public BookCopy SelectedBookCopy { get; set; }
        public List<CoverType> CoverTypes { get; set; }
        public string LibraryNameFilter { get; set; }
        public string LibraryBranchLocationFilter { get; set; }
        public string BookISBNFilter { get; set; }
        public string BookTitleFilter { get; set; }
        public string BookAuthorsFilter { get; set; }
        public string BookDescriptionFilter { get; set; }
        public string BookCopyLanguageFilter { get; set; }
        public string BookCopyFormatFilter { get; set; }
        public string BookCopyCoverTypeFilter { get; set; }
        public string BookCopyPublisherFilter { get; set; }
        public string BookCopyPublishingYearFilter { get; set; }

        private LibraryController _libraryController;
        private LibraryBranchController _libraryBranchController;
        private BookController _bookController;
        private BookCopyController _bookCopyController;
        private NotificationController _notificationController;
        private ReservationController _reservationController;
        private Client _client;

        public ClientHomeWindow(Client client, NotificationController notificationController, ReservationController reservationController)
        {
            InitializeComponent();
            DataContext = this;

            _libraryController = new LibraryController();
            _libraryBranchController = new LibraryBranchController();
            _bookController = new BookController();
            _bookCopyController = new BookCopyController();
            _notificationController = notificationController;
            _reservationController = reservationController;
            _client = client;

            Libraries = new ObservableCollection<Library>(_libraryController.GetAllLibraries());
            LibraryBranches = new ObservableCollection<LibraryBranch>();
            Books = new ObservableCollection<Book>();
            BookCopies = new ObservableCollection<BookCopy>();
            CoverTypes = new List<CoverType>(Enum.GetValues(typeof(CoverType)).Cast<CoverType>().ToList());
            LoadComboBox();

            foreach (Notification notification in _notificationController.GetAllNotifications().ToList())
            {
                if (notification.ClientUsername == _client.Username)
                {
                    MessageBox.Show($"Book {notification.ISBN} successfully reserved.", "Success");
                    _notificationController.Delete(notification);
                }
            }
        }

        private void LoadComboBox()
        {
            CoverTypeComboBox.Items.Add("Any");
            foreach (CoverType coverType in CoverTypes)
            {
                CoverTypeComboBox.Items.Add(coverType.ToString());
            }
            CoverTypeComboBox.SelectedValue = CoverTypeComboBox.Items[0];
        }

        private void LibrariesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LibraryBranches.Clear();
            if (SelectedLibrary == null)
                return;
            foreach (LibraryBranch libraryBranch in _libraryBranchController.GetLibraryBranches(SelectedLibrary.Id, LibraryBranchLocationFilter))
            {
                LibraryBranches.Add(libraryBranch);
            }
        }

        private void LibraryBranchesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Books.Clear();
            if (SelectedLibraryBranch == null)
                return;
            foreach (Book book in _bookController.GetBooks(SelectedLibraryBranch.Id, BookISBNFilter, BookTitleFilter, BookAuthorsFilter, BookDescriptionFilter))
            {
                Books.Add(book);
            }
        }

        private void BooksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookCopies.Clear();
            if (SelectedLibraryBranch == null || SelectedBook == null)
                return;
            foreach (BookCopy bookCopy in _bookCopyController.GetBookCopies(SelectedLibraryBranch.Id, SelectedBook.ISBN, BookCopyLanguageFilter, BookCopyFormatFilter, BookCopyCoverTypeFilter, BookCopyPublisherFilter, BookCopyPublishingYearFilter))
            {
                BookCopies.Add(bookCopy);
            }
            if (_bookCopyController.IsBookCopyAvailable(new List<BookCopy>(BookCopies)))
                ReserveButton.IsEnabled = false;
            else 
                ReserveButton.IsEnabled = true;
        }

        private void BookCopiesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void LibrariesFilterChanged(object sender, EventArgs e)
        {
            Libraries.Clear();
            foreach (Library library in _libraryController.GetFilteredLibraries(LibraryNameFilter))
            {
                Libraries.Add(library);
            }
        }

        private void LibraryBranchesFilterChanged(object sender, EventArgs e)
        {
            LibraryBranches.Clear();
            if (SelectedLibrary == null)
                return;
            foreach (LibraryBranch libraryBranch in _libraryBranchController.GetLibraryBranches(SelectedLibrary.Id, LibraryBranchLocationFilter))
            {
                LibraryBranches.Add(libraryBranch);
            }
        }

        private void BooksFilterChanged(object sender, EventArgs e)
        {
            Books.Clear();
            if (SelectedLibraryBranch == null)
                return;
            foreach (Book book in _bookController.GetBooks(SelectedLibraryBranch.Id, BookISBNFilter, BookTitleFilter, BookAuthorsFilter, BookDescriptionFilter))
            {
                Books.Add(book);
            }
        }

        private void BookCopiesFilterChanged(object sender, EventArgs e)
        {
            BookCopies.Clear();
            if (SelectedLibraryBranch == null || SelectedBook == null)
                return;
            foreach (BookCopy bookCopy in _bookCopyController.GetBookCopies(SelectedLibraryBranch.Id, SelectedBook.ISBN, BookCopyLanguageFilter, BookCopyFormatFilter, BookCopyCoverTypeFilter, BookCopyPublisherFilter, BookCopyPublishingYearFilter))
            {
                BookCopies.Add(bookCopy);
            }
        }

        private void ViewBorrowsButton_Click(object sender, RoutedEventArgs e)
        {
            ViewBorrowsWindow viewBorrowsWindow = new ViewBorrowsWindow(_client);
            viewBorrowsWindow.Show();
        }
        private void ReservedButton_Click(object sender, RoutedEventArgs e)
        {
            Reservation reservation = new Reservation(-1, SelectedBook.ISBN, _client.Username, DateTime.Now, false);
            _reservationController.Create(reservation);
            MessageBox.Show("Reservation successfully created!", "Success");

        }
    }
}
