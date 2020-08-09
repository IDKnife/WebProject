using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CourseWork.Services.Interfaces;
using CourseWork.WebApi.ViewModels;
using Microsoft.AspNetCore.Cors;

namespace CourseWork.WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("DefaultPolicy")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Получить список товаров.
        /// </summary>
        /// <returns>Список товаров.</returns>
        [HttpGet]
        [Route("Products/{name?}/{category?}")]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Products(string name = null, string category = null)
        {
            try
            {
                IList<Product> products;
                if ((name != null) || (category != null))
                    products = await _productService.GetProducts(name, category);
                else
                    products = await _productService.GetProducts();
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Добавить товар.
        /// </summary>
        /// <param name="product">Товар.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("AddProduct")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProduct([FromBody] ProductViewModel product)
        {
            var result = await _productService.AddProduct(product.ToEntity() as Product);
            if (result.Result)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Получить товар по Id.
        /// </summary>
        /// <returns>Товар.</returns>
        [HttpGet]
        [Route("GetProduct/{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Product(int id)
        {
            try
            {
                var product = await _productService.GetProduct(id);
                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Удалить товар.
        /// </summary>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("DeleteProduct/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (result.Result)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Обновить товар.
        /// </summary>
        /// <param name="product">Товар.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductViewModel product)
        {
            var result = await _productService.UpdateProduct(product.ToEntity() as Product);
            if (result.Result)
                return Ok();
            return BadRequest(result.MessageResult);
        }
    }
}