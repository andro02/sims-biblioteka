using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.Models
{
    public  class Notification : ISerializable
    {
        public int Id { get; set; }
        public string ClientUsername { get; set; }
        public string ISBN { get; set; }

        public Notification()
        {
            this.Id = -1;
            this.ClientUsername = string.Empty;
            this.ISBN = String.Empty;
        }

        public Notification(int id, string clientUsername, string isbn)
        {
            this.Id = id;
            this.ClientUsername = clientUsername; ;
            this.ISBN = isbn;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.ClientUsername, this.ISBN};
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.ClientUsername = values[1];
            this.ISBN = values[2];
        }

        public override string ToString()
        {
            return $"{this.Id} {this.ClientUsername} {this.ISBN}";
        }

    }
}
