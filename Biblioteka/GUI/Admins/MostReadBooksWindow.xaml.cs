using Biblioteka.Core.Books.Controllers;
using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Libraries.Controllers;
using Biblioteka.Core.Users;
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
    public partial class MostReadBooksWindow : Window
    {
        private BookController _bookController;
        private LibraryBranchController _libraryBranchController;
        private LibraryController _libraryController;
        public ObservableCollection<MostReadBook> MostReadBooks { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public MostReadBooksWindow()
        {
            InitializeComponent();
            DataContext = this;

            _bookController = new BookController();
            _libraryBranchController = new LibraryBranchController();
            _libraryController = new LibraryController();

            MostReadBooks = new ObservableCollection<MostReadBook>(_bookController.GetMostReadBooks(null, null));
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime start;
            DateTime end;
            if (DateTime.TryParse(StartDate, out start) && DateTime.TryParse(EndDate, out end))
            {
                MostReadBooks.Clear();
                foreach (MostReadBook mrb in _bookController.GetMostReadBooks(start, end))
                {
                    MostReadBooks.Add(mrb);
                }
            }
            else
            {
                MessageBox.Show("Dates not valid!", "Error");
            }
        }
    }
}
