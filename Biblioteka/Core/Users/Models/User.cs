using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.Models
{
    public class User : ISerializable
    {
        public UserType UserType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        {
            this.UserType = UserType.Librarian;
            this.Username = string.Empty;
            this.Password = string.Empty;
        }

        public User(UserType userType, string username, string password)
        {
            this.UserType = userType;
            this.Username = username;
            this.Password = password;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.UserType.ToString(), this.Username, this.Password };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            Enum.TryParse<UserType>(values[0], out UserType userType);
            this.UserType = userType;
            this.Username = values[1];
            this.Password = values[2];
        }

        public override string ToString()
        {
            return $"{this.UserType} {this.Username} {this.Password}";
        }
    }
}
