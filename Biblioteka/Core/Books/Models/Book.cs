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
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string Language { get; set; }
        public string Format { get; set; }
        public string CoverType { get; set; }
        public string Publisher { get; set; }
        public string PublishingYear { get; set; }

        public Book()
        {
            this.Id = -1;
            this.ISBN = string.Empty;
            this.Title = string.Empty;
            this.Authors = new List<string>();
            this.Language = string.Empty;
            this.Format = string.Empty;
            this.CoverType = string.Empty;
            this.Publisher = string.Empty;
            this.PublishingYear = string.Empty;
        }

        public Book(int id, string isbn, string title, List<string> authors, string language, string format, string coverType, string publisher, string publishingYear)
        {
            this.Id = id;
            this.ISBN = isbn;
            this.Title = title;
            this.Authors = authors;
            this.Language = language;
            this.Format = format;
            this.CoverType = coverType;
            this.Publisher = publisher;
            this.PublishingYear = publishingYear;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.ISBN, this.Title, string.Join(";", this.Authors), this.Language, this.Format, this.CoverType, this.Publisher, this.PublishingYear };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.ISBN = values[1];
            this.Title = values[2];
            this.Authors = values[3].Split(';').ToList();
            this.Language = values[4];
            this.Format = values[5];
            this.CoverType = values[6];
            this.Publisher = values[7];
            this.PublishingYear = values[8];
        }

        public override string ToString()
        {
            return $"{this.Id} {this.ISBN} {this.Title} {this.Authors} {this.Language} {this.Format} {this.CoverType} {this.Publisher} {this.PublishingYear}";
        }
    }
}
