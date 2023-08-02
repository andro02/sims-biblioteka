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

namespace Biblioteka.GUI.Librarians
{
    /// <summary>
    /// Interaction logic for NewClientInformationWindow.xaml
    /// </summary>
    public partial class NewClientInformationWindow : Window
    {
        public Client Client { get; set; }
        public List<ClientType> ClientTypes { get; set; }
        public string SelectedClientType { get; set; }

        private UserController _userController;
        private ClientController _clientController;

        public NewClientInformationWindow(UserController userController)
        {
            InitializeComponent();
            DataContext = this;

            _userController = userController;
            _clientController = new ClientController();

            Client = new Client();
            ClientTypes = new List<ClientType>(Enum.GetValues(typeof(ClientType)).Cast<ClientType>().ToList());
            LoadComboBox();
        }

        private void RegisterNewClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsClientValid())
                return;

            Client.UserType = UserType.Client;
            Enum.TryParse<ClientType>(SelectedClientType, out ClientType clientType);
            Client.ClientType = clientType;
            _clientController.Create(new Client(Client.Username, Client.Password, Client.ClientType, Client.Name, Client.Surname));
            _userController.Create(new User(Client.UserType, Client.Username, Client.Password));
            MessageBox.Show("Client successfully registered.", "Success");
            ResetInputFields();
        }

        private bool IsClientValid() 
        {
            if (Client.Username.Length < 3 || Client.Username.Length > 20)
            {
                MessageBox.Show("Username must be between 3 and 20 characters long.", "Error");
                return false;
            }
            if (_userController.IsAlreadyTaken(Client.Username))
            {
                MessageBox.Show("Username is already taken.", "Error");
                return false;
            }
            if (Client.Password.Length < 3 || Client.Password.Length > 20)
            {
                MessageBox.Show("Password must be between 3 and 20 characters long.", "Error");
                return false;
            }
            if (Client.Name.Length < 3 || Client.Name.Length > 20)
            {
                MessageBox.Show("Name must be between 3 and 20 characters long.", "Error");
                return false;
            }
            if (Client.Surname.Length < 3 || Client.Surname.Length > 20)
            {
                MessageBox.Show("Surname must be between 3 and 20 characters long.", "Error");
                return false;
            }
            return true;
        }

        private void ResetInputFields()
        {
            UsernameTextBox.Text = string.Empty;
            PasswordTextBox.Text = string.Empty;
            NameTextBox.Text = string.Empty;
            SurnameTextBox.Text = string.Empty;
            ClientTypesComboBox.SelectedValue = ClientTypesComboBox.Items[0];
            SelectedClientType = ClientTypesComboBox.Items[0].ToString();
        }

        private void LoadComboBox()
        {
            foreach (ClientType clientType in ClientTypes)
            {
                ClientTypesComboBox.Items.Add(clientType.ToString());
            }
            ClientTypesComboBox.SelectedValue = ClientTypesComboBox.Items[0];
            SelectedClientType = ClientTypesComboBox.Items[0].ToString(); 
        }
    }
}
