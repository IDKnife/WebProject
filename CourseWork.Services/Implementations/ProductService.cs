using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Repositories.Interfaces;
using CourseWork.Services.Interfaces;

namespace CourseWork.Services.Implementations
{
    /// <summary>
    /// Представляет сервис продуктов.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProductService"/>.
        /// </summary>
        /// <param name="repository">Репозиторий.</param>
        public ProductService(IProductRepository repository)
            => _repository = repository;

        /// <summary>
        /// Получить список продуктов.
        /// </summary>
        /// <returns>Список продуктов.</returns>
        public async Task<IList<Product>> GetProducts()
            => await _repository.GetEntities();

        /// <summary>
        /// Получить отфильтрованный список продуктов.
        /// </summary>
        /// <param name="name">Фильтр по наименованию.</param>
        /// <param name="category">Фильтр по категории.</param>
        /// <returns>Отфильтрованный список продуктов.</returns>
        public async Task<IList<Product>> GetProducts(string name, string category)
            => await _repository.GetEntities(name, category);

        /// <inheritdoc cref="IProductService.AddProduct"/>
        public async Task<ServiceOperationResult> AddProduct(Product entity)
        {
            try
            {
                await _repository.AddEntity(entity);
                return ServiceOperationResult.Success();
            }
            catch (Exception e)
            {
                return ServiceOperationResult.Fail(e.ToString());
            }
        }

        /// <inheritdoc cref="IProductService.DeleteProduct"/>
        public async Task<ServiceOperationResult> DeleteProduct(string id)
        {
            try
            {
                await _repository.DeleteEntity(id);
                return ServiceOperationResult.Success();
            }
            catch (Exception e)
            {
                return ServiceOperationResult.Fail(e.ToString());
            }
        }

        /// <inheritdoc cref="IProductService.UpdateProduct"/>
        public async Task<ServiceOperationResult> UpdateProduct(Product entity)
        {
            try
            {
                await _repository.UpdateEntity(entity);
                return ServiceOperationResult.Success();
            }
            catch (Exception e)
            {
                return ServiceOperationResult.Fail(e.ToString());
            }
        }

        /// <inheritdoc cref="IProductService.GetProduct"/>
        public async Task<Product> GetProduct(string id)
            => await _repository.GetEntity(id);
    }
}
