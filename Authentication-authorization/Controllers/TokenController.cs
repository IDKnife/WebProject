using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Services.Interfaces;
using CourseWork.WebApi.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;

namespace Authentication_authorization.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("DefaultPolicy")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly string _secureKey = "some_secure_for_safety";
        public TokenController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        [Route("GetToken/{email}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Identification(string email, [FromBody] string password)
        {
            var identity = await CheckIdentity(email, password);
            if (identity == null)
                return BadRequest(new { errorText = "Invalid username or password." });
            var jwt = new JwtSecurityToken(
                claims: identity.Claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secureKey)), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responce = new
            {
                token = encodedJwt,
                id = identity.Claims.First(a => a.Type == "Id").Value
            };
            return Ok(responce.ToJson());
        }

        private async Task<ClaimsIdentity> CheckIdentity(string email, string password)
        {
            var clients = await _clientService.GetClients();
            var client = clients.First(item => item.Email == email && item.Password == password);
            if (client != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("Access", client.Access.ToString()),
                    new Claim("Id", client.Id),
                    new Claim("Email", client.Email)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
                return claimsIdentity;
            }
            return null;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] ClientViewModel client)
        {
            var item = await _clientService.GetClient(client.Id);
            if (item != null)
                return BadRequest(new { errorText = "This user already exists." });
            await _clientService.AddClient(client.ToNewEntity() as Client);
            return Ok();
        }
    }
}
