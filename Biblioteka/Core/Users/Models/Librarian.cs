using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.Models
{
    public class Librarian : User, ISerializable
    {
        public int LibraryBranchId { get; set; }

        public Librarian() : base()
        {
            this.UserType = UserType.Librarian;
            this.LibraryBranchId = -1;
        }

        public Librarian(string username, string password, int libraryBranchId) : base(UserType.Librarian, username, password)
        {
            this.LibraryBranchId = libraryBranchId;
        }

        public override string[] ToCSV()
        {
            string[] csvValues = { this.UserType.ToString(), this.Username, this.Password, this.LibraryBranchId.ToString() };
            return csvValues;
        }

        public override void FromCSV(string[] values)
        {
            this.UserType = UserType.Librarian;
            this.Username = values[1];
            this.Password = values[2];
            this.LibraryBranchId = int.Parse(values[3]);
        }

        public override string ToString()
    {
            return $"{this.UserType} {this.Username} {this.Password} {this.LibraryBranchId}";
        }
    }
}
