using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CourseWork.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CourseWork.Services.Implementations
{
    /// <summary>
    /// Представляет процесс создания токена аторизации.
    /// </summary>
    public class CreateTokenService : ICreateTokenService
    {
        private readonly string _secureKey = "some_secure_for_safety";

        /// <inheritdoc cref="ICreateTokenService.CreateToken"/>
        public string CreateToken(IEnumerable<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secureKey)), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
