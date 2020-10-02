using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Authentication_authorization.Classes;
using CourseWork.AdditionalClasses.ViewModels;
using CourseWork.Models;
using CourseWork.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;

namespace Authentication_authorization.Controllers
{
    /// <summary>
    /// Представляет контроллер для работы с токенами доступа.
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("DefaultPolicy")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly string _secureKey = "some_secure_for_safety";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TokenController"/>.
        /// </summary>
        /// <param name="clientService">Сервис для работы с базой клиентов.</param>
        public TokenController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Провести идентификацию клиента.
        /// </summary>
        /// <param name="inputData">Электронная почта и пароль клиента.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("GetToken")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Identification([FromBody] Identity inputData)
        {
            var identity = await CheckIdentity(inputData.Email, inputData.Password);
            if (identity == null)
                return BadRequest(new { errorText = "Invalid username or password." });
            var jwt = new JwtSecurityToken(
                claims: identity.Claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secureKey)), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responce = new
            {
                token = encodedJwt,
                id = identity.Claims.First(a => a.Type == "Id").Value,
                access = identity.Claims.First(a => a.Type == "Access").Value
            };
            return Ok(responce.ToJson());
        }

        /// <summary>
        /// Процесс идентификации клиента.
        /// </summary>
        /// <param name="email">Электронная почта клиента.</param>
        /// <param name="password">Пароль клиента.</param>
        /// <returns>Результат идентификации.</returns>
        private async Task<ClaimsIdentity> CheckIdentity(string email, string password)
        {
            var clients = await _clientService.GetClients();
            var client = clients.FirstOrDefault(item => item.Email == email && item.Password == password);
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

        /// <summary>
        /// Зарегистрировать нового клиента.
        /// </summary>
        /// <param name="client">Данные нового клиента</param>
        /// <returns>Ответ сервера.</returns>
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
