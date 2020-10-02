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
        {
            Entities = database.GetCollection<Order>("Orders");
        }

        /// <summary>
        /// Получить список заказов.
        /// </summary>
        /// <returns>Список заказов.</returns>
        public async Task<IList<Order>> GetEntities()
        {
            return await Entities.Find(FilterDefinition<Order>.Empty).ToListAsync();
        }

        /// <summary>
        /// Добавить заказ.
        /// </summary>
        /// <param name="entity">Заказ.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        public async Task AddEntity(Order entity)
        {
            await Entities.InsertOneAsync(entity);
        }

        /// <summary>
        /// Удалить заказ.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        public async Task DeleteEntity(string id)
        {
            var filter = Builders<Order>.Filter.Eq("_id", id);
            await Entities.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Обновить заказ.
        /// </summary>
        /// <param name="entity">Заказ.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        public async Task UpdateEntity(Order entity)
        {
            var filter = Builders<Order>.Filter.Eq("_id", entity.Id);
            await Entities.ReplaceOneAsync(filter, entity);
        }

        /// <summary>
        /// Получить заказ.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Заказ.</returns>
        public async Task<Order> GetEntity(string id)
        {
            var filter = Builders<Order>.Filter.Eq("_id", id);
            return await Entities.Find(filter).FirstOrDefaultAsync();
        }
    }
}
