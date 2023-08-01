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
        public ClientType ClientType { get; set; }

        public Client() : base()
        {
            this.UserType = UserType.Client;
            this.ClientType = ClientType.Adult;
        }

        public Client(string username, string password, ClientType clientType) : base(UserType.Client, username, password)
        {
            this.ClientType = clientType;
        }

        public override string[] ToCSV()
        {
            string[] csvValues = { this.UserType.ToString(), this.Username, this.Password, this.ClientType.ToString() };
            return csvValues;   
        }

        public override void FromCSV(string[] values)
        {
            this.UserType = UserType.Client;
            this.Username = values[1];
            this.Password = values[2];
            Enum.TryParse<ClientType>(values[3], out ClientType clientType);
            this.ClientType = clientType;
        }

        public override string ToString()
        {
            return $"{this.UserType} {this.Username} {this.Password} {this.ClientType}";
        }
    }
}
