using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса продуктов.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Получить список продуктов.
        /// </summary>
        /// <returns>Список продуктов.</returns>
        Task<IList<Product>> GetProducts();

        /// <summary>
        /// Получить отфильтрованный список продуктов.
        /// </summary>
        /// <param name="name">Фильтр по наименованию.</param>
        /// <param name="category">Фильтр по категории.</param>
        /// <returns>Отфильтрованный список продуктов.</returns>
        Task<IList<Product>> GetProducts(string name, string category);

        /// <summary>
        /// Добавить продукт.
        /// </summary>
        /// <param name="entity">Продукт.</param>
        /// <returns>Результат операции.</returns>
        Task<ServiceOperationResult> AddProduct(Product entity);

        /// <summary>
        /// Удалить продукт.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Результат операции.</returns>
        Task<ServiceOperationResult> DeleteProduct(string id);

        /// <summary>
        /// Обновить продукт.
        /// </summary>
        /// <param name="entity">Продукт.</param>
        /// <returns>Результат операции.</returns>
        Task<ServiceOperationResult> UpdateProduct(Product entity);

        /// <summary>
        /// Получить продукт.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Продукт.</returns>
        Task<Product> GetProduct(string id);
    }
}
