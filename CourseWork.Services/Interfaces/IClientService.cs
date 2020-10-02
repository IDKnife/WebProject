using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса клиентов.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Получить список клиентов.
        /// </summary>
        /// <returns>Список клиентов.</returns>
        Task<IList<Client>> GetClients();

        /// <summary>
        /// Добавить клиента.
        /// </summary>
        /// <param name="entity">Клиент.</param>
        /// <returns>Результат операции.</returns>
        Task<ServiceOperationResult> AddClient(Client entity);

        /// <summary>
        /// Удалить клиента.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Результат операции.</returns>
        Task<ServiceOperationResult> DeleteClient(string id);

        /// <summary>
        /// Обновить клиента.
        /// </summary>
        /// <param name="entity">Клиент.</param>
        /// <returns>Результат операции.</returns>
        Task<ServiceOperationResult> UpdateClient(Client entity);

        /// <summary>
        /// Получить клиента.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Клиент.</returns>
        Task<Client> GetClient(string id);

        /// <summary>
        /// Добавить заказ заданному клиенту.
        /// </summary>
        /// <param name="order">Заказ.</param>
        /// <param name="clientId">Уникальный идентификатор клиента.</param>
        /// <returns>Результат операции</returns>
        Task<ServiceOperationResult> AddOrderToClientList(Order order, string clientId);
    }
}
