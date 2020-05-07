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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Получить список заказов.
        /// </summary>
        /// <returns>Список заказов.</returns>
        [HttpGet]
        [Route("Orders")]
        [ProducesResponseType(typeof(List<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Orders()
        {
            try
            {
                var orders = await _orderService.GetOrders();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Добавить заказ.
        /// </summary>
        /// <param name="order">заказ.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("AddOrder")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrder([FromBody]OrderViewModel order)
        {
            try
            {
                await _orderService.AddOrder(order.ToEntity() as Order);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}