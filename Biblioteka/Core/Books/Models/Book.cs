using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;

namespace Biblioteka.Core.Books.Models
{
    public class Book : ISerializable
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string Description { get; set; }

        public Book()
        {
            this.ISBN = string.Empty;
            this.Title = string.Empty;
            this.Authors = new List<string>();
            this.Description = string.Empty;
        }

        public Book(int id, string isbn, string title, List<string> authors, string description)
        {
            this.ISBN = isbn;
            this.Title = title;
            this.Authors = authors;
            this.Description = description;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.ISBN, this.Title, string.Join(";", this.Authors), this.Description };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.ISBN = values[0];
            this.Title = values[1];
            this.Authors = values[2].Split(';').ToList();
            this.Description = values[3];
        }

        public override string ToString()
        {
            return $"{this.ISBN} {this.Title} {this.Authors} {this.Description}";
        }
    }
}
