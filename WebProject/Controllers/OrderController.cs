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
using MongoDB.Bson;

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
        /// Получить заказ.
        /// </summary>
        /// <returns>Заказ.</returns>
        [HttpGet]
        [Route("GetOrder/{id?}")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Order(int id)
        {
            try
            {
                var order = await _orderService.GetOrder(id);
                return Ok(order);
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
        public async Task<IActionResult> AddOrder([FromBody] OrderViewModel order)
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

        /// <summary>
        /// Добавить продукт в корзину.
        /// </summary>
        /// <param name="product">Продукт</param>
        /// <param name="orderId">Уникальный идентификатор заказа</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("AddProductToBasket/{orderId?}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProductToBusket([FromBody] ProductViewModel product, int orderId)
        {
            try
            {
                
                await _orderService.AddProductToBasket(product.ToEntity() as Product, orderId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Удалить продукт из корзины.
        /// </summary>
        /// <param name="productId">Уникальный идентификатор продукта</param>
        /// <param name="orderId">Уникальный идентификатор заказа</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("DeleteProductFromBasket/{orderId?}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProductFromBasket([FromBody] int productId, int orderId)
        {
            try
            {
                await _orderService.DeleteProductFromBasket(productId, orderId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Обновить количество единиц продукта в корзине.
        /// </summary>
        /// <param name="productIdAndNewCount">Массив, содержащий уникальный идентификатор продукта и его новое количество единиц</param>
        /// <param name="orderId">Уникальный идентификатор заказа</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("UpdateProductCountInBasket/{orderId?}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductCountInBasket([FromBody] int[] productIdAndNewCount, int orderId)
        {
            try
            {
                await _orderService.UpdateProductCountInBasket(productIdAndNewCount[0], productIdAndNewCount[1], orderId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Удалить заказ.
        /// </summary>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("DeleteOrder")]
        [EnableCors("DefaultPolicy")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                await _orderService.DeleteOrder(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}