using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CourseWork.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using CourseWork.AdditionalClasses.ViewModels;

namespace CourseWork.WebApi.Controllers
{
    /// <summary>
    /// Представляет контроллер для работы с продуктами.
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("DefaultPolicy")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAccessService _accessService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProductController"/>.
        /// </summary>
        /// <param name="productService">Сервис для работы с базой продуктов.</param>
        /// <param name="accessService">Сервис для проверки уровня доступа клиента.</param>
        public ProductController(IProductService productService, IAccessService accessService)
        {
            _productService = productService;
            _accessService = accessService;
        }

        /// <summary>
        /// Получить список продуктов.
        /// </summary>
        /// <param name="name">Фильтр по наименованию.</param>
        /// <param name="category">Фильтр по категории.</param>
        /// <returns>Ответ сервера.</returns>
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
        /// Добавить продукт.
        /// </summary>
        /// <param name="product">Продукт.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Authorize]
        [Route("AddProduct")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProduct([FromBody] ProductViewModel product)
        {
            if (_accessService.IsUser(User))
                return BadRequest("No access");
            var result = await _productService.AddProduct(product.ToNewEntity() as Product);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Получить продукт.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpGet]
        [Route("GetProduct/{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Product(string id)
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
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Authorize]
        [Route("DeleteProduct/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            if (_accessService.IsUser(User))
                return BadRequest("No access");
            var result = await _productService.DeleteProduct(id);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.MessageResult);
        }

        /// <summary>
        /// Обновить продукт.
        /// </summary>
        /// <param name="product">Продукт.</param>
        /// <returns>Ответ сервера.</returns>
        [HttpPost]
        [Authorize]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductViewModel product)
        {
            if (_accessService.IsUser(User))
                return BadRequest("No access");
            var result = await _productService.UpdateProduct(product.ToEntity() as Product);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.MessageResult);
        }
    }
}