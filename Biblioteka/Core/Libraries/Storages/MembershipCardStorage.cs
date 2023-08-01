using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.Storages
{
    public class MembershipCardStorage
    {
        private const string StoragePath = "../../Data/membershipCards.csv";

        private readonly Serializer<MembershipCard> _serializer;

        public MembershipCardStorage()
        {
            _serializer = new Serializer<MembershipCard>();
        }

        public List<MembershipCard> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<MembershipCard> membershipCards)
        {
            _serializer.ToCSV(StoragePath, membershipCards);
        }
    }
}
