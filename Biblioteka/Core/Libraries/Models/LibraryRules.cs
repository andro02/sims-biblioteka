using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Libraries.Models
{
    public class LibraryRules : ISerializable
    {
        public int Id { get; set; }
        public int LibraryId { get; set; }
        public int ChildBookCopyLimit { get; set; }
        public int ChildBorrowDurationLimit { get; set; }
        public int PupilBookCopyLimit { get; set; }
        public int PupilBorrowDurationLimit { get; set; }
        public int StudentBookCopyLimit { get; set; }
        public int StudentBorrowDurationLimit { get; set; }
        public int AdultBookCopyLimit { get; set; }
        public int AdultBorrowDurationLimit { get; set; }
        public int PensionerBookCopyLimit { get; set; }
        public int PensionerBorrowDurationLimit { get; set; }

        public LibraryRules() 
        {
            this.Id = -1;
            this.LibraryId = -1;
            this.ChildBookCopyLimit = -1;
            this.ChildBorrowDurationLimit = -1;
            this.PupilBookCopyLimit = -1;
            this.PupilBorrowDurationLimit = -1;
            this.StudentBookCopyLimit = -1;
            this.StudentBorrowDurationLimit = -1;
            this.AdultBookCopyLimit = -1;
            this.AdultBorrowDurationLimit = -1;
            this.PensionerBookCopyLimit= -1;
            this.PensionerBorrowDurationLimit = -1;
        }

        public LibraryRules(int id, int libraryId, int childBookCopyLimit, int childBorrowDurationLimit, int pupilBookCopyLimit, int pupilBorrowDurationLimit, int studentBookCopyLimit, int studentBorrowDurationLimit, int adultBookCopyLimit, int adultBorrowDurationLimit, int pensionerBookCopyLimit, int pensionerBorrowDurationLimit)
        {
            this.Id = id;
            this.LibraryId = libraryId;
            this.ChildBookCopyLimit = childBookCopyLimit;
            this.ChildBorrowDurationLimit = childBorrowDurationLimit;
            this.PupilBookCopyLimit = pupilBookCopyLimit;
            this.PupilBorrowDurationLimit = pupilBorrowDurationLimit;
            this.StudentBookCopyLimit = studentBookCopyLimit;
            this.StudentBorrowDurationLimit = studentBorrowDurationLimit;
            this.AdultBookCopyLimit = adultBookCopyLimit;
            this.AdultBorrowDurationLimit = adultBorrowDurationLimit;
            this.PensionerBookCopyLimit = pensionerBookCopyLimit;
            this.PensionerBorrowDurationLimit = pensionerBorrowDurationLimit;
        }

        public virtual string[] ToCSV()
        {
            string[] csvValues = { this.Id.ToString(), this.LibraryId.ToString(), this.ChildBookCopyLimit.ToString(), this.ChildBorrowDurationLimit.ToString(), this.PupilBookCopyLimit.ToString(), this.PupilBorrowDurationLimit.ToString(), this.StudentBookCopyLimit.ToString(), this.StudentBorrowDurationLimit.ToString(), this.AdultBookCopyLimit.ToString(), this.AdultBorrowDurationLimit.ToString(), this.PensionerBookCopyLimit.ToString(), this.PensionerBorrowDurationLimit.ToString() };
            return csvValues;
        }

        public virtual void FromCSV(string[] values)
        {
            this.Id = int.Parse(values[0]);
            this.LibraryId = int.Parse(values[1]);
            this.ChildBookCopyLimit = int.Parse(values[2]);
            this.ChildBorrowDurationLimit = int.Parse(values[3]);
            this.PupilBookCopyLimit = int.Parse(values[4]);
            this.PupilBorrowDurationLimit = int.Parse(values[5]);
            this.StudentBookCopyLimit = int.Parse(values[6]);
            this.StudentBorrowDurationLimit = int.Parse(values[7]);
            this.AdultBookCopyLimit = int.Parse(values[8]);
            this.AdultBorrowDurationLimit = int.Parse(values[9]);
            this.PensionerBookCopyLimit = int.Parse(values[10]);
            this.PensionerBorrowDurationLimit = int.Parse(values[11]);
        }

        public override string ToString()
        {
            return $"{this.Id} {this.LibraryId} {this.ChildBookCopyLimit} {this.ChildBorrowDurationLimit} {this.PupilBookCopyLimit} {this.PupilBorrowDurationLimit} {this.StudentBookCopyLimit} {this.StudentBorrowDurationLimit} {this.AdultBookCopyLimit} {this.AdultBorrowDurationLimit} {this.PensionerBookCopyLimit} {this.PensionerBorrowDurationLimit}";
        }
    }
}
