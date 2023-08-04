using Biblioteka.Core.Books.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Books.Storages
{
    public class DeadlineExtensionRequestStorage
    {
        private const string StoragePath = "../../Data/deadlineExtensionRequests.csv";

        private readonly Serializer<DeadlineExtensionRequest> _serializer;

        public DeadlineExtensionRequestStorage()
        {
            _serializer = new Serializer<DeadlineExtensionRequest>();
        }

        public List<DeadlineExtensionRequest> Load()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Save(List<DeadlineExtensionRequest> deadlineExtensionRequests)
        {
            _serializer.ToCSV(StoragePath, deadlineExtensionRequests);
        }
    }
}
