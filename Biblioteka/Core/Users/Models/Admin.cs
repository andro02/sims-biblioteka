using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.Models
{
    public class Admin : User, ISerializable
    {
        public Admin() : base()
        {
            this.UserType = UserType.Admin;
        }

        public Admin(string username, string password) : base(UserType.Admin, username, password)
        {
        }

        public override string[] ToCSV()
        {
            string[] csvValues = { this.UserType.ToString(), this.Username, this.Password };
            return csvValues;
        }

        public override void FromCSV(string[] values)
        {
            this.UserType = UserType.Admin;
            this.Username = values[1];
            this.Password = values[2];
        }

        public override string ToString()
        {
            return $"{this.UserType} {this.Username} {this.Password}";
        }
    }
}
