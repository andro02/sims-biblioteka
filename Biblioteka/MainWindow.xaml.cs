using Biblioteka.Core.BookCopys.Controllers;
using Biblioteka.Core.Books.Controllers;
using Biblioteka.Core.Books.Models;
using Biblioteka.Core.Users;
using Biblioteka.Core.Users.Controllers;
using Biblioteka.Core.Users.Models;
using Biblioteka.GUI.Admins;
using Biblioteka.GUI.Clients;
using Biblioteka.GUI.Librarians;
using Biblioteka.GUI.Librarians.LibrariansSecondTier;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Username { get; set; }
        public string Password { get; set; }

        private UserController _userController;
        private LibrarianController _librarianController;
        private ClientController _clientController;
        private ReservationController _reservationController;
        private BookCopyController _bookCopyController;
        private NotificationController _notificationController;

        public MainWindow()
        {
            InitializeComponent();
            UsernameTextBox.Focus();
            DataContext = this;

            Username = string.Empty;
            Password = string.Empty;

            _userController = new UserController();
            _librarianController = new LibrarianController();
            _clientController = new ClientController();
            _reservationController = new ReservationController();
            _bookCopyController = new BookCopyController();
            _notificationController = new NotificationController();
            
            List<Reservation> activeReservations = _reservationController.GetReservations(true);
            foreach(Reservation reservation in activeReservations.ToList())
            {
                BookCopy? bookCopy = _bookCopyController.FindReservedBookCopy(reservation.ISBN);
                if (reservation.ReservedAt.AddDays(2).CompareTo(DateTime.Now) < 0)
                {
                    _reservationController.Delete(reservation);

                    if (bookCopy != null)
                    {
                        bookCopy.Status = BookCopyStatus.Available;
                        _bookCopyController.Update(bookCopy);
                    }
                }
            }

            List<Reservation> inactiveReservations = _reservationController.GetReservations(false);
            foreach (Reservation reservation in inactiveReservations)
            {
                BookCopy? bookCopy = _bookCopyController.FindAvailableBookCopy(reservation.ISBN);
                if (bookCopy != null)
                {
                    bookCopy.Status = BookCopyStatus.Reserved;
                    _bookCopyController.Update(bookCopy);

                    reservation.IsActive = true;
                    reservation.ReservedAt = DateTime.Now;
                    _reservationController.Update(reservation);

                    _notificationController.Create(new Notification(-1, reservation.ClientUsername, reservation.ISBN));
                }
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            User? user = _userController.TryLogin(Username, Password);
            if (user == null)
            {
                MessageBox.Show("Incorrect username or password. Please, try again.", "Error");
                return;
            }
            switch (user.UserType)
            {
                case UserType.Librarian:
                    {
                        Librarian librarian = _librarianController.GetLibrarianByUsername(Username);
                        if (librarian.LibrarianTier == LibrarianTier.First)
                        {
                            LibrarianFirstTierHomeWindow librarianFirstTierHomeWindow = new LibrarianFirstTierHomeWindow(_userController, librarian);
                            librarianFirstTierHomeWindow.Show();
                        }
                        else
                        {
                            LibrarianSecondTierHomeWindow librarianSecondTierHomeWindow = new LibrarianSecondTierHomeWindow(librarian, _reservationController);
                            librarianSecondTierHomeWindow.Show();
                        }
                        break;
                    }
                case UserType.Admin:
                    {
                        AdminHomeWindow adminHomeWindow = new AdminHomeWindow(_userController);
                        adminHomeWindow.Show();
                        break;
                    }
                case UserType.Client:
                    {
                        Client client = _clientController.GetClientByUsername(Username);
                        ClientHomeWindow clientHomeWindow = new ClientHomeWindow(client, _notificationController, _reservationController);
                        clientHomeWindow.Show();
                        break;
                    }
            }
        }
    }
}
