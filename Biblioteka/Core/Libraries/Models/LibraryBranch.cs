using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Libraries.Models
{
    public class LibraryBranch : ISerializable
    {
        public int Id { get; set; }
        public int LibraryId { get; set; }
        public string Location { get; set; }
        public LibraryBranch()
        {
            this.Id = -1;
            this.LibraryId = -1;
            this.Location = string.Empty;
        }

        public LibraryBranch(int id, int libraryId, string location)
        {
            this.Id = id;
            this.LibraryId = libraryId;
            this.Location = location;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.LibraryId.ToString(), this.Location };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.LibraryId = int.Parse(values[1]);
            this.Location = values[2];
        }

        public override string ToString()
        {
            return $"{this.Id} {this.LibraryId} {this.Location}";
        }
    }
}
