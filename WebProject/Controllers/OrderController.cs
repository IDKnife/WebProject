﻿using System;
using System.Collections.Generic;
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
        /// Получить заказ.
        /// </summary>
        /// <param name="id">Уникальный идентификатор заказа.</param>
        /// <returns>Заказ.</returns>
        [HttpGet]
        [Route("GetOrder/{id}")]
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
            var result = await _orderService.AddOrder(order.ToEntity() as Order);
            if (result.Result)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Добавить продукт в корзину.
        /// </summary>
        /// <param name="product">Продукт</param>
        /// <param name="orderId">Уникальный идентификатор заказа</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("AddProductToOrder/{orderId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProductToOrder([FromBody] ProductViewModel product, int orderId)
        {
            var result = await _orderService.AddProductToOrder(product.ToEntity() as Product, orderId);
            if (result.Result)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Удалить продукт из корзины.
        /// </summary>
        /// <param name="orderId">Уникальный идентификатор заказа</param>
        /// <param name="productId">Уникальный идентификатор продукта</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("{orderId}/DeleteProduct/{productId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProductFromOrder(int orderId, int productId)
        {
            var result = await _orderService.DeleteProductFromOrder(productId, orderId);
            if (result.Result)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Обновить количество единиц продукта в корзине.
        /// </summary>
        /// <param name="newCount">Новое количество единиц продукта</param>
        /// <param name="productId">Уникальный идентификатор продукта</param>
        /// <param name="orderId">Уникальный идентификатор заказа</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("{orderId}/UpdateProductCount/{productId}/{newCount}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductCountInOrder(int orderId, int productId, int newCount)
        {
            var result = await _orderService.UpdateProductCountInOrder(productId, newCount, orderId);
            if (result.Result)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Получить цену корзины.
        /// </summary>
        /// <param name="id">Уникальный идентификатор заказа.</param>
        /// <returns>Цена корзины.</returns>
        [HttpGet]
        [Route("GetPriceOfOrder/{id}")]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPriceOfOrder(int id)
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
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("DeleteOrder/{id}")]
        [EnableCors("DefaultPolicy")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderService.DeleteOrder(id);
            if (result.Result)
                return Ok();
            return BadRequest(result.MessageResult);
        }
    }
}