
using AuthorizationAPI.Models;
using System.Collections.Generic;

namespace AuthorizationAPI.Repository
{
    public interface IUserRepository
    {
        User GetUser(string username);

    }
}
