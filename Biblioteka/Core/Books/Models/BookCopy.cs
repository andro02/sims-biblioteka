using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Serializer;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.Models
{
    public class BookCopy : ISerializable
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public int LibraryBranchId { get; set; }
        public string Language { get; set; }
        public string Format { get; set; }
        public CoverType CoverType { get; set; }
        public string Publisher { get; set; }
        public int PublishingYear { get; set; }

        public BookCopy()
        {
            this.Id = -1;
            this.ISBN = string.Empty;
            this.LibraryBranchId = -1;
            this.Language = string.Empty;
            this.Format = string.Empty;
            this.CoverType = CoverType.Soft;
            this.Publisher = string.Empty;
            this.PublishingYear = -1;
        }

        public BookCopy(int id, string isbn, int libraryBranchId, string language, string format, CoverType coverType, string publisher, int publishingYear)
        {
            this.Id = id;
            this.ISBN = isbn;
            this.LibraryBranchId = libraryBranchId;
            this.Language = language;
            this.Format = format;
            this.CoverType = coverType;
            this.Publisher = publisher;
            this.PublishingYear = publishingYear;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.ISBN, this.LibraryBranchId.ToString(), this.Language, this.Format, this.CoverType.ToString(), this.Publisher, this.PublishingYear.ToString() };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.ISBN = values[1];
            this.LibraryBranchId = int.Parse(values[2]);
            this.Language = values[3];
            this.Format = values[4];
            Enum.TryParse<CoverType>(values[5], out CoverType coverType);
            this.CoverType = coverType;
            this.Publisher = values[6];
            this.PublishingYear = int.Parse(values[7]);
        }

        public override string ToString()
        {
            return $"{this.Id} {this.ISBN} {this.LibraryBranchId} {this.Language} {this.Format} {this.CoverType} {this.Publisher} {this.PublishingYear}";
        }
    }
}
