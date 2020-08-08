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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Authentication_authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly string _secureKey;
        private readonly IClientService _clientService;
        public TokenController(IClientService clientService)
        {
            _clientService = clientService;
            _secureKey = "some_secure";
        }

        [HttpPost]
        [Route("GetToken")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Identification(string email, string password)
        {
            var identity = await CheckIdentity(email, password);
            if (identity == null)
                return BadRequest(new {errorText = "Invalid username or password."});
            var jwt = new JwtSecurityToken(
                claims: identity.Claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secureKey)), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return Ok(encodedJwt);
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
                    new Claim("Id", client.Id.ToString()),
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
                return BadRequest(new {errorText = "This user already exists."});
            await _clientService.AddClient(client.ToEntity() as Client);
            return Ok();
        }
    }
}
