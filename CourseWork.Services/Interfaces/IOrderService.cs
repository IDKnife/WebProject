using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса заказов.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Получить список заказов.
        /// </summary>
        /// <returns>Список заказов.</returns>
        Task<IList<Order>> GetOrders();

        /// <summary>
        /// Добавить заказ.
        /// </summary>
        /// <param name="entity">Заказ.</param>
        /// <returns>Результат операции.</returns>
        Task<ServiceOperationResult> AddOrder(Order entity);

        /// <summary>
        /// Удалить заказ.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Результат операции.</returns>
        Task<ServiceOperationResult> DeleteOrder(string id);

        /// <summary>
        /// Обновить заказ.
        /// </summary>
        /// <param name="entity">Заказ.</param>
        /// <returns>Результат операции.</returns>
        Task<ServiceOperationResult> UpdateOrder(Order entity);

        /// <summary>
        /// Получить заказ.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Заказ.</returns>
        Task<Order> GetOrder(string id);

        /// <summary>
        /// Добавить продукт в заказ.
        /// </summary>
        /// <param name="product">Продукт.</param>
        /// <param name="orderId">Уникальный идентификатор заказа.</param>
        /// <returns>Результат операции.</returns>
        Task<ServiceOperationResult> AddProductToOrder(Product product, string orderId);

        /// <summary>
        /// Удалить продукт из заказа.
        /// </summary>
        /// <param name="productId">Уникальный идентификатор продукта.</param>
        /// <param name="orderId">Уникальный идентификатор заказа.</param>
        /// <returns>Результат операции.</returns>
        Task<ServiceOperationResult> DeleteProductFromOrder(string productId, string orderId);

        /// <summary>
        /// Обновить количество единиц продукта в заказе.
        /// </summary>
        /// <param name="productId">Уникальный идентификатор продукта.</param>
        /// <param name="newCount">Новое значение количества единиц товара.</param>
        /// <param name="orderId">Уникальный идентификатор заказа.</param>
        /// <returns>Результат операции.</returns>
        Task<ServiceOperationResult> UpdateProductCountInOrder(string productId,
                                                               int newCount,
                                                               string orderId);

        /// <summary>
        /// Получить итоговую стоимость заказа.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Итоговая стоимость заказа.</returns>
        Task<double> GetPriceOfOrder(string id);

        /// <summary>
        /// Удалить пустые анонимные заказы, датирующиеся не текущим днем, и исключая из списка удаления текущий заказ.
        /// </summary>
        /// <param name="id">Уникальный идентификатор текущего заказа</param>
        /// <returns>Результат операции.</returns>
        Task<ServiceOperationResult> DeleteEmptyAnonymOrders(string id);
    }
}
