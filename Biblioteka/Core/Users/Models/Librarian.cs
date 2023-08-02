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
        public LibrarianTier LibrarianTier { get; set; } 

        public Librarian() : base()
        {
            this.UserType = UserType.Librarian;
            this.LibraryBranchId = -1;
            this.LibrarianTier = LibrarianTier.First;
        }

        public Librarian(string username, string password, int libraryBranchId, LibrarianTier librarianTier) : base(UserType.Librarian, username, password)
        {
            this.LibraryBranchId = libraryBranchId;
            this.LibrarianTier = librarianTier;
        }

        public override string[] ToCSV()
        {
            string[] csvValues = { this.UserType.ToString(), this.Username, this.Password, this.LibraryBranchId.ToString(), this.LibrarianTier.ToString() };
            return csvValues;
        }

        public override void FromCSV(string[] values)
        {
            this.UserType = UserType.Librarian;
            this.Username = values[1];
            this.Password = values[2];
            this.LibraryBranchId = int.Parse(values[3]);
            Enum.TryParse<LibrarianTier>(values[4], out LibrarianTier librarianTier);
            this.LibrarianTier = librarianTier;
        }

        public override string ToString()
    {
            return $"{this.UserType} {this.Username} {this.Password} {this.LibraryBranchId} {this.LibrarianTier}";
        }
    }
}
