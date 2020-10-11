using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.AdditionalClasses.ViewModels;
using CourseWork.Models;
using CourseWork.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.WebApi.Controllers
{
    /// <summary>
    /// Представляет контроллер для работы с клиентами.
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("DefaultPolicy")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IAccessService _accessService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClientController"/>.
        /// </summary>
        /// <param name="clientService">Сервис для работы с базой клиентов.</param>
        /// <param name="accessService">Сервис для проверки уровня доступа клиента.</param>
        public ClientController(IClientService clientService, IAccessService accessService)
        {
            _clientService = clientService;
            _accessService = accessService;
        }

        /// <summary>
        /// Получить список клиентов.
        /// </summary>
        /// <returns>Ответ сервера.</returns>
        [HttpGet]
        [Authorize]
        [Route("Clients")]
        [ProducesResponseType(typeof(List<Client>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Clients()
        {
            if (!_accessService.IsAdmin(User))
                return BadRequest("No access");
            try
            {
                var clients = await _clientService.GetClients();
                return Ok(clients);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Получить клиента.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpGet]
        [Authorize]
        [Route("GetClient/{id}")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClient(string id)
        {
            try
            {
                var client = await _clientService.GetClient(id);
                return Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Удалить клиента.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Authorize]
        [Route("DeleteClient/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteClient(string id)
        {
            if (!_accessService.IsAdmin(User))
                return BadRequest("No access");
            var result = await _clientService.DeleteClient(id);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Добавить заказ в список заказов клиента.
        /// </summary>
        /// <param name="order">Заказ.</param>
        /// <param name="clientId">Уникальный идентификатор клиента.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Authorize]
        [Route("AddOrderToList/{clientId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrderToClientList([FromBody] OrderViewModel order, string clientId)
        {
            var result = await _clientService.AddOrderToClientList(order.ToEntity() as Order, clientId);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Обновить клиента.
        /// </summary>
        /// <param name="сlient">Клиент.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Authorize]
        [Route("UpdateClient")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClient([FromBody] ClientViewModel сlient)
        {
            var result = await _clientService.UpdateClient(сlient.ToEntity() as Client);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.MessageResult);
        }
    }
}