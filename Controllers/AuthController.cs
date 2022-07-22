using JwtAuthenticationHomework.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace JwtAuthenticationHomework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class AuthController : ControllerBase
    {


        private readonly UserRespository userRepository;
        public AuthController(UserRespository userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public string Get(string username, string password)
        {
            var token = userRepository.GenerateToken(username, password);

            return (string)token;
        }

        [HttpPost]
        public bool Get(string token)
        {
            bool isValid = (bool)userRepository.ValidateToken(token, out var validatedToken, out var principal);
            if (isValid)
            {
                return true;

            }
            else
            {
                return false;


            }

        }
    }
}