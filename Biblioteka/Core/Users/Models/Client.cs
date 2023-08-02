using Biblioteka.Core.Users.Controllers;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.Models
{
    public class Client : User, ISerializable
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public ClientType ClientType { get; set; }

        public Client() : base()
        {
            this.UserType = UserType.Client;
            this.ClientType = ClientType.Adult;
            this.Name = string.Empty;
            this.Surname = string.Empty;
        }

        public Client(string username, string password, ClientType clientType, string name, string surname) : base(UserType.Client, username, password)
        {
            this.ClientType = clientType;
            this.Name = name;
            this.Surname = surname;
        }

        public override string[] ToCSV()
        {
            string[] csvValues = { this.UserType.ToString(), this.Username, this.Password, this.ClientType.ToString(), this.Name, this.Surname };
            return csvValues;   
        }

        public override void FromCSV(string[] values)
        {
            this.UserType = UserType.Client;
            this.Username = values[1];
            this.Password = values[2];
            Enum.TryParse<ClientType>(values[3], out ClientType clientType);
            this.ClientType = clientType;
            this.Name = values[4];
            this.Surname = values[5];
        }

        public override string ToString()
        {
            return $"{this.UserType} {this.Username} {this.Password} {this.ClientType} {this.Name} {this.Surname}";
        }
    }
}
