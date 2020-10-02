using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Repositories.Interfaces;
using MongoDB.Driver;

namespace CourseWork.Repositories.Implementations
{
    /// <summary>
    /// Представляет репозиторий клиентов.
    /// </summary>
    public class ClientRepository : MongoRepository<Client>, IClientRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClientRepository"/>.
        /// </summary>
        /// <param name="database">База данных.</param>
        public ClientRepository(IMongoDatabase database) : base(database)
        {
            Entities = database.GetCollection<Client>("Clients");
        }

        /// <summary>
        /// Получить список клиентов.
        /// </summary>
        /// <returns>Список клиентов.</returns>
        public async Task<IList<Client>> GetEntities()
        {
            return await Entities.Find(FilterDefinition<Client>.Empty).ToListAsync();
        }

        /// <summary>
        /// Добавить клиента.
        /// </summary>
        /// <param name="entity">Клиент.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        public async Task AddEntity(Client entity)
        {
            await Entities.InsertOneAsync(entity);
        }

        /// <summary>
        /// Удалить клиента.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        public async Task DeleteEntity(string id)
        {
            var filter = Builders<Client>.Filter.Eq("_id", id);
            await Entities.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Обновить клиента.
        /// </summary>
        /// <param name="entity">Клиент.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        public async Task UpdateEntity(Client entity)
        {
            var filter = Builders<Client>.Filter.Eq("_id", entity.Id);
            await Entities.ReplaceOneAsync(filter, entity);
        }

        /// <summary>
        /// Получить клиента.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Клиент.</returns>
        public async Task<Client> GetEntity(string id)
        {
            var filter = Builders<Client>.Filter.Eq("_id", id);
            return await Entities.Find(filter).FirstOrDefaultAsync();
        }
    }
}
