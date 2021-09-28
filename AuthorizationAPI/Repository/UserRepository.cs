using AuthorizationAPI.Models;
using System.Collections.Generic;

namespace AuthorizationAPI.Repository
{
    public class UserRepository :IUserRepository
    {
        private List<User> users;
        public UserRepository()
        {
            users = new List<User>()
            {
                new User { Id = 1, Username = "agent", Password ="agent"}
            };
        }
        public User GetUser(string username)
        {
            return users.Find(user => user.Username == username);
        }
        
    }
}
