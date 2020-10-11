using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CourseWork.Models.Core;
using CourseWork.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CourseWork.Services.Implementations
{
    /// <summary>
    /// Представляет процесс создания токена авторизации.
    /// </summary>
    public class CreateTokenService : ICreateTokenService
    {

        /// <inheritdoc cref="ICreateTokenService.CreateToken"/>
        public string CreateToken(IEnumerable<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Contstants.SecureKey)), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
