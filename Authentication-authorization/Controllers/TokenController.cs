using System.Linq;
using System.Threading.Tasks;
using Authentication_authorization.Classes;
using CourseWork.AdditionalClasses.ViewModels;
using CourseWork.Models;
using CourseWork.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ICreateTokenService _createTokenService;
        private readonly IIdentificationService _identificationService;
        private readonly ILoggedRequestsService _loggedRequestsService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TokenController"/>.
        /// </summary>
        /// <param name="clientService">Сервис для работы с базой клиентов.</param>
        /// <param name="createTokenService">Сервис для создания токена авторизации.</param>
        /// <param name="identificationService">Сервис для идентификации клиента.</param>
        /// <param name="loggedRequestsService">Сервис для возврата логгированных ответов сервера.</param>
        public TokenController(IClientService clientService,
                               ICreateTokenService createTokenService,
                               IIdentificationService identificationService,
                               ILoggedRequestsService loggedRequestsService)
        {
            _clientService = clientService;
            _createTokenService = createTokenService;
            _identificationService = identificationService;
            _loggedRequestsService = loggedRequestsService;
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
            var identity = await _identificationService.CheckIdentity(inputData.Email, inputData.Password);
            if (identity == null)
                return _loggedRequestsService.BadLoggedRequest("Invalid username or password.");
            var responce = new
            {
                token = _createTokenService.CreateToken(identity.Claims),
                id = identity.Claims.First(a => a.Type == "Id").Value,
                access = identity.Claims.First(a => a.Type == "Access").Value
            };
            return Ok(responce.ToJson());
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
            var item = await _clientService.GetClients();
            if (item.First(a => a.Email == client.Email) != null)
                return _loggedRequestsService.BadLoggedRequest("This email already in system.");
            await _clientService.AddClient(client.ToNewEntity() as Client);
            return Ok();
        }
    }
}
