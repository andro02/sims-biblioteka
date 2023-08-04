using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.Models
{
    public class DeadlineExtensionRequest : ISerializable
    {
        public int Id { get; set; }
        public int BorrowId { get; set; }
        public DateTime SentAt { get; set; }

        public DeadlineExtensionRequest()
        {
            this.Id = -1;
            this.BorrowId = -1;
            this.SentAt = DateTime.MinValue;
        }

        public DeadlineExtensionRequest(int id, int borrowId, DateTime sentAt)
        {
            this.Id = id;
            this.BorrowId = borrowId;
            this.SentAt = sentAt;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.BorrowId.ToString(), this.SentAt.ToString() };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.BorrowId = int.Parse(values[1]);
            this.SentAt = DateTime.Parse(values[2]);
        }

        public override string ToString()
        {
            return $"{this.Id} {this.BorrowId} {this.SentAt}";
        }
    }
}
