using Biblioteka.Core.Libraries.DAOs;
using Biblioteka.Core.Libraries.Models;
using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Libraries.Controllers
{
    public class LibraryRulesController
    {
        private LibraryRulesDAO _libraryRules;

        public LibraryRulesController()
        {
            _libraryRules = new LibraryRulesDAO();
        }

        public List<LibraryRules> GetAllLibraryRules()
        {
            return _libraryRules.GetAll();
        }

        public LibraryRules? GetLibraryRulesByLibraryId(int libraryId)
        {
            return _libraryRules.GetByLibraryId(libraryId);
        }

        public int GetBookCopyLimit(int libraryId, ClientType clientType)
        {
            LibraryRules libraryRules = GetLibraryRulesByLibraryId(libraryId);

            switch(clientType)
            {
                case ClientType.Child: return libraryRules.ChildBookCopyLimit;
                case ClientType.Pupil: return libraryRules.PupilBookCopyLimit;
                case ClientType.Student: return libraryRules.StudentBookCopyLimit;
                case ClientType.Adult: return libraryRules.AdultBookCopyLimit;
                case ClientType.Pensioner: return libraryRules.PensionerBookCopyLimit;
                default: return -1;
            }
        }

        public int GetBorrowDurationLimit(int libraryId, ClientType clientType)
        {
            LibraryRules libraryRules = GetLibraryRulesByLibraryId(libraryId);

            switch (clientType)
            {
                case ClientType.Child: return libraryRules.ChildBorrowDurationLimit;
                case ClientType.Pupil: return libraryRules.PupilBorrowDurationLimit;
                case ClientType.Student: return libraryRules.StudentBorrowDurationLimit;
                case ClientType.Adult: return libraryRules.AdultBorrowDurationLimit;
                case ClientType.Pensioner: return libraryRules.PensionerBorrowDurationLimit;
                default: return -1;
            }
        }

        public void Create(LibraryRules libraryRules)
        {
            _libraryRules.Add(libraryRules);
        }

        public void Delete(LibraryRules libraryRules)
        {
            _libraryRules.Remove(libraryRules);
        }

        public void Subscribe(IObserver observer)
        {
            _libraryRules.Subscribe(observer);
        }
    }
}
