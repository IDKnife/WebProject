using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Services.Interfaces;
using CourseWork.WebApi.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("DefaultPolicy")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Получить список клиентов.
        /// </summary>
        /// <returns>Список клиентов.</returns>
        [HttpGet]
        [Route("Clients")]
        [ProducesResponseType(typeof(List<Client>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Clients()
        {
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
        /// Добавить клиента.
        /// </summary>
        /// <param name="сlient">Клиент.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("AddClient")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddClient([FromBody]ClientViewModel сlient)
        {
            try
            {
                await _clientService.AddClient(сlient.ToEntity() as Client);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}