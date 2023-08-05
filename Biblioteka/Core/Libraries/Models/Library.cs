using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Libraries.Models
{
    public class Library : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Library()
        {
            this.Id = -1;
            this.Name = string.Empty;
        }

        public Library(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }   

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.Name };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.Name = values[1];
        }

        public override string ToString()
        {
            return $"{this.Id} {this.Name}";
        }
    }
}
