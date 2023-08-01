using Biblioteka.Core.Users.Models;
using Biblioteka.Utilities.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Core.Users.Storages
{
    public class UserStorage
    {
        private const string LibrariansStoragePath = "../../Data/librarians.csv";
        private const string AdminsStoragePath = "../../Data/admins.csv";
        private const string ClientsStoragePath = "../../Data/clients.csv";

        private Serializer<User> _serializer;

        public UserStorage()
        {
            _serializer = new Serializer<User>();
        }

        public List<User> Load()
        {
            List<User> users = new List<User>();
            users.AddRange(_serializer.FromCSV(LibrariansStoragePath));
            users.AddRange(_serializer.FromCSV(AdminsStoragePath));
            users.AddRange(_serializer.FromCSV(ClientsStoragePath));
            return users;
        }

        public void Save(List<User> users)
        {
            int lastUserIndex = 0;
            User lastUser = users[0];
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].UserType != lastUser.UserType)
                {
                    switch (lastUser.UserType)
                    {
                        case UserType.Librarian:
                            {
                                _serializer.ToCSV(LibrariansStoragePath, users.GetRange(lastUserIndex, i - lastUserIndex));
                                lastUserIndex = i;
                                lastUser = users[i];
                                break;
                            }
                        case UserType.Admin:
                            {
                                _serializer.ToCSV(AdminsStoragePath, users.GetRange(lastUserIndex, i - lastUserIndex));
                                lastUserIndex = i;
                                lastUser = users[i];
                                break;
                            }
                        case UserType.Client:
                            {
                                _serializer.ToCSV(AdminsStoragePath, users.GetRange(lastUserIndex, i - lastUserIndex));
                                lastUserIndex = i;
                                lastUser = users[i];
                                break;
                            }
                    }
                }
            }
        }
    }
}
