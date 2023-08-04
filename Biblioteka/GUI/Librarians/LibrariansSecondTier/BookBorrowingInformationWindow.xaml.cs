using Biblioteka.Core.BookCopys.Controllers;
using Biblioteka.Core.Books.Controllers;
using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Libraries.Controllers;
using Biblioteka.Core.Libraries.Models;
using Biblioteka.Core.Users.Controllers;
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
    /// Interaction logic for BookBorrowingInformationWindow.xaml
    /// </summary>
    public partial class BookBorrowingInformationWindow : Window, IObserver
    {
        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<BookCopy> BookCopies { get; set; }
        public Book SelectedBook { get; set; }
        public BookCopy SelectedBookCopy { get; set; }
        public List<CoverType> CoverTypes { get; set; }
        public string BookISBNFilter { get; set; }
        public string BookTitleFilter { get; set; }
        public string BookAuthorsFilter { get; set; }
        public string BookDescriptionFilter { get; set; }
        public string BookCopyLanguageFilter { get; set; }
        public string BookCopyFormatFilter { get; set; }
        public string BookCopyCoverTypeFilter { get; set; }
        public string BookCopyPublisherFilter { get; set; }
        public string BookCopyPublishingYearFilter { get; set; }
        public string ClientUsername { get; set; }

        private BookController _bookController;
        private BookCopyController _bookCopyController;
        private ClientController _clientController;
        private BorrowController _borrowController;
        private MembershipCardController _membershipCardController;
        private LibraryBranchController _libraryBranchController;
        private Librarian _librarian;
        private int _libraryBranchId;

        public BookBorrowingInformationWindow(Librarian librarian)
        {
            InitializeComponent();
            DataContext = this;

            _bookController = new BookController();
            _bookController.Subscribe(this);
            _bookCopyController = new BookCopyController();
            _bookCopyController.Subscribe(this);
            _clientController = new ClientController();
            _borrowController = new BorrowController();
            _membershipCardController = new MembershipCardController();
            _libraryBranchController = new LibraryBranchController();
            _librarian = librarian;
            _libraryBranchId = _librarian.LibraryBranchId;

            Books = new ObservableCollection<Book>(_bookController.GetBooks(_libraryBranchId, BookISBNFilter, BookTitleFilter, BookAuthorsFilter, BookDescriptionFilter));
            BookCopies = new ObservableCollection<BookCopy>();
            CoverTypes = new List<CoverType>(Enum.GetValues(typeof(CoverType)).Cast<CoverType>().ToList());

            LoadComboBox();
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

        private void BooksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookCopies.Clear();
            if (SelectedBook == null)
                return;
            foreach (BookCopy bookCopy in _bookCopyController.GetBookCopies(_libraryBranchId, SelectedBook.ISBN, BookCopyLanguageFilter, BookCopyFormatFilter, BookCopyCoverTypeFilter, BookCopyPublisherFilter, BookCopyPublishingYearFilter))
            {
                BookCopies.Add(bookCopy);
            }
        }

        private void BookCopiesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButtonEnabling();
        }

        private void ClientUsernameTextChanged(object sender, EventArgs e)
        {
            CheckButtonEnabling();
        }

        private void CheckButtonEnabling()
        {
            if (SelectedBook == null || ClientUsername == string.Empty || ClientUsername == null)
            {
                GiveAwayButton.IsEnabled = false;
                return;
            }
            GiveAwayButton.IsEnabled = true;
        }


        private void BooksFilterChanged(object sender, EventArgs e)
        {
            Books.Clear();
            foreach (Book book in _bookController.GetBooks(_libraryBranchId, BookISBNFilter, BookTitleFilter, BookAuthorsFilter, BookDescriptionFilter))
            {
                Books.Add(book);
            }
        }

        private void BookCopiesFilterChanged(object sender, EventArgs e)
        {
            BookCopies.Clear();
            if (SelectedBook == null)
                return;
            foreach (BookCopy bookCopy in _bookCopyController.GetBookCopies(_libraryBranchId, SelectedBook.ISBN, BookCopyLanguageFilter, BookCopyFormatFilter, BookCopyCoverTypeFilter, BookCopyPublisherFilter, BookCopyPublishingYearFilter))
            {
                BookCopies.Add(bookCopy);
            }
        }

        private void GiveAwayButton_Click(object sender, EventArgs e)
        {
            if (!IsBorrowValid())
                return;

            SelectedBookCopy.Status = BookCopyStatus.Unavailable;
            _borrowController.Create(new Borrow(-1, ClientUsername, SelectedBookCopy.Id, DateTime.Now, DateTime.Now.AddDays(7), false));
            _bookCopyController.Update(SelectedBookCopy);
            MessageBox.Show($"Client {ClientUsername} has borrowed the book copy.", "Success");
            ResetInputFields();
        }

        private bool IsBorrowValid()
        {
            if (!_clientController.IsAlreadyTaken(ClientUsername))
            {
                MessageBox.Show("Provided client username does not exist in our system.", "Error");
                return false;
            }
            if (SelectedBookCopy.Status == BookCopyStatus.Unavailable)
            {
                MessageBox.Show("Chosen book copy is not available currently.", "Error");
                return false;
            }
            int libraryId = _libraryBranchController.GetLibraryId(_librarian.LibraryBranchId);
            MembershipCard? membershipCard = _membershipCardController.GetMembershipCardById(ClientUsername, libraryId);
            if (membershipCard == null)
            {
                MessageBox.Show("Provided client does not have membership in our library.", "Error");
                return false;
            }
            if (membershipCard.ValidUntil <= DateTime.Now)
            {
                MessageBox.Show("Provided client's membership has expired.", "Error");
                return false;
            }
            return true;
        }

        private void ResetInputFields()
        {
            ClientUsernameTextBox.Text = string.Empty;
        }

        private void UpdateBooksList()
        {
            Books.Clear();
            foreach (Book book in _bookController.GetBooks(_libraryBranchId, BookISBNFilter, BookTitleFilter, BookAuthorsFilter, BookDescriptionFilter))
            {
                Books.Add(book);
            }
        }

        private void UpdateBookCopiesList()
        {
            BookCopies.Clear();
            if (SelectedBook == null)
                return;
            foreach (BookCopy bookCopy in _bookCopyController.GetBookCopies(_libraryBranchId, SelectedBook.ISBN, BookCopyLanguageFilter, BookCopyFormatFilter, BookCopyCoverTypeFilter, BookCopyPublisherFilter, BookCopyPublishingYearFilter))
            {
                BookCopies.Add(bookCopy);
            }
        }

        public void Update()
        {
            UpdateBooksList();
            UpdateBookCopiesList();
        }
    }
}
