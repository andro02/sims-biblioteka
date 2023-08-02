using Biblioteka.Core.Books.Controllers;
using Biblioteka.Core.Books.Models;
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

namespace Biblioteka.GUI.Librarians.LibrariansSecondTier
{
    /// <summary>
    /// Interaction logic for NewBookInformationWindow.xaml
    /// </summary>
    public partial class NewBookInformationWindow : Window
    {
        public Book Book { get; set; }
        public string Authors { get; set; }

        private BookController _bookController;

        public NewBookInformationWindow()
        {
            InitializeComponent();
            DataContext = this;

            _bookController = new BookController();

            Book = new Book();
        }

        private void AddNewBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsBookValid())
                return;

            _bookController.Create(new Book(-1, Book.ISBN, Book.Title, Book.Authors, Book.Description));
            MessageBox.Show("Book successfully added.", "Success");
            ResetInputFields();
        }

        private bool IsBookValid()
        {
            if (Book.ISBN.Length < 10 || Book.ISBN.Length > 20)
            {
                MessageBox.Show("ISBN must be between 10 and 20 characters long.", "Error");
                return false;
            }
            if (_bookController.IsAlreadyTaken(Book.ISBN))
            {
                MessageBox.Show("ISBN is already in system.", "Error");
                return false;
            }
            if (Authors.Length == 0)
            {
                MessageBox.Show("No authors provided.", "Error");
                return false;
            }
            Book.Authors = Authors.Split(';').ToList();
            if (Book.Description.Length == 0)
            {
                MessageBox.Show("No description provided.", "Error");
                return false;
            }
            return true;
        }

        private void ResetInputFields()
        {
            ISBNTextBox.Text = string.Empty;
            TitleTextBox.Text = string.Empty;
            AuthorsTextBox.Text = string.Empty;
            DescriptionTextBox.Text = string.Empty;
        }
    }
}
