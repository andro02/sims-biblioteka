using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.Models
{
    public class MembershipCard : ISerializable
    {
        public int Id { get; set; }
        public string ClientUsername { get; set; }
        public int LibraryId { get; set; }
        public DateTime ValidUntil { get; set; }

        public MembershipCard()
        {
            this.Id = -1;
            this.ClientUsername = string.Empty;
            this.LibraryId = -1;
            ValidUntil = DateTime.MinValue;
        }

        public MembershipCard(int id, string clientUsername, int libraryId, DateTime validUntil)
        {
            this.Id = id;
            this.ClientUsername = clientUsername;
            this.LibraryId = libraryId;
            this.ValidUntil = validUntil;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.ClientUsername, this.LibraryId.ToString(), this.ValidUntil.ToString() };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.ClientUsername = values[1];
            this.LibraryId = int.Parse(values[2]);
            this.ValidUntil = DateTime.Parse(values[3]);
        }

        public override string ToString()
        {
            return $"{this.Id} {this.ClientUsername} {this.LibraryId} {this.ValidUntil}";
        }
    }
}
