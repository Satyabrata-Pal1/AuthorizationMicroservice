using AuthorizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Service
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
