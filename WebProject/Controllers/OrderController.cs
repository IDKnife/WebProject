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
    /// Представляет контроллер для работы с заказами.
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("DefaultPolicy")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAccessService _accessService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OrderController"/>.
        /// </summary>
        /// <param name="orderService">Сервис для работы с базой заказов.</param>
        /// <param name="accessService">Сервис для проверки уровня доступа клиента.</param>
        public OrderController(IOrderService orderService, IAccessService accessService)
        {
            _orderService = orderService;
            _accessService = accessService;
        }

        /// <summary>
        /// Получить список заказов.
        /// </summary>
        /// <returns>Ответ сервера.</returns>
        [HttpGet]
        [Authorize]
        [Route("Orders")]
        [ProducesResponseType(typeof(List<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Orders()
        {
            if (!_accessService.IsAdmin(User))
                return BadRequest("No access");
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
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpGet]
        [Route("GetOrder/{id}")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Order(string id)
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
        /// <param name="order">Заказ.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("AddOrder")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrder([FromBody] OrderViewModel order)
        {
            var newOrder = order.ToNewEntity() as Order;
            var result = await _orderService.AddOrder(newOrder);
            if (result.IsSuccess)
                return Ok(newOrder.Id);
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Добавить продукт в корзину.
        /// </summary>
        /// <param name="product">Продукт.</param>
        /// <param name="orderId">Уникальный идентификатор заказа.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("AddProductToOrder/{orderId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProductToOrder([FromBody] ProductViewModel product, string orderId)
        {
            var result = await _orderService.AddProductToOrder(product.ToEntity() as Product, orderId);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Удалить продукт из корзины.
        /// </summary>
        /// <param name="orderId">Уникальный идентификатор заказа.</param>
        /// <param name="productId">Уникальный идентификатор продукта.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("{orderId}/DeleteProduct/{productId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProductFromOrder(string orderId, string productId)
        {
            var result = await _orderService.DeleteProductFromOrder(productId, orderId);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Обновить количество единиц продукта в корзине.
        /// </summary>
        /// <param name="newCount">Новое количество единиц продукта.</param>
        /// <param name="productId">Уникальный идентификатор продукта.</param>
        /// <param name="orderId">Уникальный идентификатор заказа.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("{orderId}/UpdateProductCount/{productId}/{newCount}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductCountInOrder(string orderId,
                                                                   string productId,
                                                                   int newCount)
        {
            var result = await _orderService.UpdateProductCountInOrder(productId, newCount, orderId);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Получить цену корзины.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Цена корзины.</returns>
        [HttpGet]
        [Route("GetPriceOfOrder/{id}")]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPriceOfOrder(string id)
        {
            try
            {
                var price = await _orderService.GetPriceOfOrder(id);
                return Ok(price);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Удалить заказ.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Authorize]
        [Route("DeleteOrder/{id}")]
        [EnableCors("DefaultPolicy")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            if (!_accessService.IsAdmin(User))
                return BadRequest("No access");
            var result = await _orderService.DeleteOrder(id);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Обновить заказ.
        /// </summary>
        /// <param name="order">Заказ.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Authorize]
        [Route("UpdateOrder")]
        [EnableCors("DefaultPolicy")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderViewModel order)
        {
            if (!_accessService.IsAdmin(User))
                return BadRequest("No access");
            var result = await _orderService.UpdateOrder(order.ToEntity() as Order);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Удалить пустые анонимные заказы кроме текущего, которые были созданы за день или более относительно текущей даты.
        /// </summary>
        /// <param name="id">Уникальный идентификатор текущего заказа.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Authorize]
        [Route("DeleteEmptyAnonymOrders/{id}")]
        [EnableCors("DefaultPolicy")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEmptyAnonymOrders(string id)
        {
            if (!_accessService.IsAdmin(User))
                return BadRequest("No access");
            var result = await _orderService.DeleteEmptyAnonymOrders(id);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.MessageResult);
        }
    }
}