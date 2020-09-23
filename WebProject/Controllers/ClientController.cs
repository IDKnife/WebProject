﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Services.Interfaces;
using CourseWork.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
        [Authorize]
        [Route("Clients")]
        [ProducesResponseType(typeof(List<Client>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Clients()
        {
            if (User.Claims.First(a => a.Type == "Access").Value != "Admin")
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
        /// <returns>Клиент.</returns>
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
            var result = await _clientService.AddClient(сlient.ToNewEntity() as Client);
            if (result.Result)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Удалить клиента.
        /// </summary>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("DeleteClient/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteClient(string id)
        {
            var result = await _clientService.DeleteClient(id);
            if (result.Result)
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
            if (result.Result)
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
            if (result.Result)
                return Ok();
            return BadRequest(result.MessageResult);
        }
    }
}