using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.Models
{
    public class Reservation : ISerializable
    {

        public int Id { get; set; }
        public string ISBN { get; set; }
        public string ClientUsername { get; set; }
        public DateTime ReservedAt { get; set; }
        public bool IsActive { get; set; }

        public Reservation()
        {
            this.Id = -1;
            this.ISBN = string.Empty;
            this.ClientUsername = string.Empty;
            this.ReservedAt = DateTime.MinValue;
            this.IsActive = false;
        }

        public Reservation(int id, string isbn, string clientUsername, DateTime reservedAt, bool isActive)
        {
            this.Id=id;
            this.ISBN=isbn; 
            this.ClientUsername = clientUsername; 
            this.ReservedAt =reservedAt;
            this.IsActive = isActive;

        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.ISBN, this.ClientUsername, this.ReservedAt.ToString(), this.IsActive.ToString() };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.ISBN = values[1];
            this.ClientUsername = values[2];
            this.ReservedAt = DateTime.Parse(values[3]);
            this.IsActive = bool.Parse(values[4]);
           
        }

        public override string ToString()
        {
            return $"{this.Id} {this.ISBN} {this.ClientUsername} {this.ReservedAt} {this.IsActive}";
        }
    }
}
