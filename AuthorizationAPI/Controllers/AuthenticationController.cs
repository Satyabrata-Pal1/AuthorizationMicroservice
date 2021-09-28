using AuthorizationAPI.DTO;
using AuthorizationAPI.Repository;
using AuthorizationAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IUserRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }
        [HttpPost]
        public IActionResult Login(UserDto userData)
        {
            var user = _repository.GetUser(userData.Username.ToLower());
            if (user != null)
            {
                if (user.Password == userData.Password) return Ok(new { token = _tokenService.GenerateToken(user) });
                else Unauthorized();
            }
            return Unauthorized();
        }
    }
}
