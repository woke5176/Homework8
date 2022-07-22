
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Text;

using System.Security.Claims;
using System.Security.Principal;
using JwtAuthenticationHomework.Models;

namespace JwtAuthenticationHomework.Services
{
    public class UserRespository
    {
        private static readonly List<User> users;
        const string ISSUER = "issuing-website-url";
        const string AUDIENCE = "target-website-url";
        const string key = "225e46a6fa5340b1a87b988e4e4326900c8bebf8668648579e91441d1158daf48eb60ad8d035492f8cc8846624eed68c2ee34e08db774af2bd94fb7b99f64ba7";

        private static readonly SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));



        private static string generatedToken = null;


        static UserRespository()
        {
            users = new List<User>()
            {
                new User{UserName = "Vabv" , Password ="ok"},
                new User{UserName = "Sarita" , Password ="200"},
               


            };

        }
        public UserRespository()
        {

        }

        internal object GenerateToken(string username, string password)
        {
            //throw new NotImplementedException();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secToken = new JwtSecurityToken(
                signingCredentials: credentials,
                issuer: ISSUER,
                audience: AUDIENCE,
                claims: new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.UniqueName, password)
                }
            );

            var handler = new JwtSecurityTokenHandler();

            generatedToken = handler.WriteToken(secToken);
            return handler.WriteToken(secToken);
        }



        internal object ValidateToken(string generatedToken, out SecurityToken? validatedToken, out IPrincipal? principal)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters()
            {
                
                ValidateLifetime = true,

               
                ValidateAudience = true,

                ValidateIssuer = true,

             
                ValidIssuer = ISSUER,

              
                ValidAudience = AUDIENCE,

                IssuerSigningKey = securityKey
            };

            try
            {
                principal = tokenHandler.ValidateToken(generatedToken, validationParameters, out validatedToken);

                return true ;
            }
            catch (SecurityTokenException tokenException)
            {

                validatedToken = null;
                principal = null;

                return false;
            }
        }
    }
}