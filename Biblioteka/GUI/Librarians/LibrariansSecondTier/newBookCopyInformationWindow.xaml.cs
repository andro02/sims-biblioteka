using Biblioteka.Core.BookCopys.Controllers;
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

namespace Biblioteka.GUI.Librarians.LibrariansSecondTier
{
    /// <summary>
    /// Interaction logic for NewBookCopyInformationWindow.xaml
    /// </summary>
    public partial class NewBookCopyInformationWindow : Window
    {
        public BookCopy BookCopy { get; set; }
        public List<CoverType> CoverTypes { get; set; }
        public List<string> ISBNs { get; set; }
        public CoverType SelectedCoverType { get; set; }
        public string PublishingYear { get; set; }

        private BookController _bookController;
        private BookCopyController _bookCopyController;

        public NewBookCopyInformationWindow()
        {
            InitializeComponent();
            DataContext = this;

            _bookController = new BookController();
            _bookCopyController = new BookCopyController();

            BookCopy = new BookCopy();
            CoverTypes = new List<CoverType>(Enum.GetValues(typeof(CoverType)).Cast<CoverType>().ToList());
            ISBNs = _bookController.GetAllBooks().Select(x => x.ISBN).ToList();
            LoadComboBox();
        }

        private void AddNewBookCopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsBookCopyValid())
                return;

            _bookCopyController.Create(BookCopy);
            MessageBox.Show("Book copy successfully added.", "Success");
            ResetInputFields();
        }

        private bool IsBookCopyValid()
        {
            if (BookCopy.Language.Length == 0)
            {
                MessageBox.Show("No language provided.", "Error");
                return false;
            }
            if (BookCopy.Format.Length == 0)
            {
                MessageBox.Show("No format provided.", "Error");
                return false;
            }
            if (BookCopy.Publisher.Length == 0)
            {
                MessageBox.Show("No publisher provided.", "Error");
                return false;
            }
            int publishingYear;
            if (!int.TryParse(PublishingYear, out publishingYear))
            {
                MessageBox.Show("Publishing year is not a valid number.", "Error");
                return false;
            }
            BookCopy.PublishingYear = publishingYear;
            if (BookCopy.PublishingYear < 1900 || BookCopy.PublishingYear > DateTime.Now.Year)
            {
                MessageBox.Show($"Publishing year must be between 1900 and {DateTime.Now.Year}.", "Error");
                return false;
            }
            return true;
        }

        private void ResetInputFields()
        {
            ISBNsComboBox.SelectedValue = ISBNsComboBox.Items[0];
            LanguageTextBox.Text = string.Empty;
            FormatTextBox.Text = string.Empty;
            PublisherTextBox.Text = string.Empty;
            PublishingYearTextBox.Text = string.Empty;
            CoverTypesComboBox.SelectedValue = CoverTypesComboBox.Items[0];
        }

        private void LoadComboBox()
        {
            if (ISBNsComboBox.Items.Count == 0)
            {
                MessageBox.Show("There are no books in the system.", "Error");
                AddNewBookCopyButton.IsEnabled = false;
            }
            else
            {
                ISBNsComboBox.SelectedValue = ISBNsComboBox.Items[0];
            }
            foreach (CoverType coverType in CoverTypes)
            {
                CoverTypesComboBox.Items.Add(coverType.ToString());
            }
            CoverTypesComboBox.SelectedValue = CoverTypesComboBox.Items[0];
        }
    }
}
