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
        private MembershipCardDAO _membershipCards;

        public MembershipCardController()
        {
            _membershipCards = new MembershipCardDAO();
        }

        public List<MembershipCard> GetAllMembershipCards()
        {
            return _membershipCards.GetAll();
        }

        public MembershipCard? GetMembershipCardById(string clientUsername, int libraryId)
        {
            return _membershipCards.GetById(clientUsername, libraryId);
        }

        public bool IsAlreadyRegistered(string clientUsername, int libraryId)
        {
            MembershipCard? membershipCard = GetMembershipCardById(clientUsername, libraryId);
            if (membershipCard == null)
                return false;
            return true;
        }

        public void ExtendMembership(string clientUsername, int libraryId)
        {
            MembershipCard? membershipCard = GetMembershipCardById(clientUsername, libraryId);
            if (membershipCard == null)
                return;
            membershipCard.ValidUntil = membershipCard.ValidUntil.AddMonths(1);
            Update(membershipCard);
        }

        public void Create(MembershipCard membershipCard)
        {
            _membershipCards.Add(membershipCard);
        }

        public void Update(MembershipCard membershipCard) 
        {
            _membershipCards.Change(membershipCard);
        }

        public void Delete(MembershipCard membershipCard)
        {
            _membershipCards.Remove(membershipCard);
        }

        public void Subscribe(IObserver observer)
        {
            _membershipCards.Subscribe(observer);
        }
    }
}
