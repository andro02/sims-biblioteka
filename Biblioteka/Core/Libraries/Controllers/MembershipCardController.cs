using Biblioteka.Core.Libraries.DAOs;
using Biblioteka.Core.Users.DAOs;
using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Libraries.Controllers
{
    public class MembershipCardController
    {
        private MembershipCardDAO _membershipCardss;

        public MembershipCardController()
        {
            _membershipCardss = new MembershipCardDAO();
        }

        public List<MembershipCard> GetAllMembershipCards()
        {
            return _membershipCardss.GetAll();
        }

        public MembershipCard? GetMembershipCardById(int id)
        {
            return _membershipCardss.GetById(id);
        }

        public bool IsAlreadyRegistered(string clientUsername, int libraryId)
        {
            foreach (MembershipCard membershipCard in GetAllMembershipCards())
            {
                if (membershipCard.ClientUsername == clientUsername && membershipCard.LibraryId == libraryId)
                    return true;
            }
            return false;
        }

        public void Create(MembershipCard membershipCards)
        {
            _membershipCardss.Add(membershipCards);
        }

        public void Delete(MembershipCard membershipCards)
        {
            _membershipCardss.Remove(membershipCards);
        }

        public void Subscribe(IObserver observer)
        {
            _membershipCardss.Subscribe(observer);
        }
    }
}
