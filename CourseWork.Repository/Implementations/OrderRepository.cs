using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Repositories.Interfaces;
using MongoDB.Driver;

namespace CourseWork.Repositories.Implementations
{
    /// <summary>
    /// Представляет репозиторий заказов.
    /// </summary>
    public class OrderRepository : MongoRepository<Order>, IOrderRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OrderRepository"/>.
        /// </summary>
        /// <param name="database">База данных.</param>
        public OrderRepository(IMongoDatabase database) : base(database)
            => Entities = database.GetCollection<Order>("Orders");

        /// <summary>
        /// Получить список заказов.
        /// </summary>
        /// <returns>Список заказов.</returns>
        public async Task<IList<Order>> GetEntities()
            => await Entities.Find(EmptyFilter()).ToListAsync();

        /// <summary>
        /// Добавить заказ.
        /// </summary>
        /// <param name="entity">Заказ.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        public async Task AddEntity(Order entity)
            => await Entities.InsertOneAsync(entity);

        /// <summary>
        /// Удалить заказ.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        public async Task DeleteEntity(string id)
            => await Entities.DeleteOneAsync(IdFilter(id));

        /// <summary>
        /// Обновить заказ.
        /// </summary>
        /// <param name="entity">Заказ.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        public async Task UpdateEntity(Order entity)
            => await Entities.ReplaceOneAsync(IdFilter(entity.Id), entity);

        /// <summary>
        /// Получить заказ.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Заказ.</returns>
        public async Task<Order> GetEntity(string id)
            => await Entities.Find(IdFilter(id)).FirstOrDefaultAsync();
    }
}
