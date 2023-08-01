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
