using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CourseWork.Repositories.Implementations
{
    /// <summary>
    /// Представляет репозиторий продуктов.
    /// </summary>
    public class ProductRepository : MongoRepository<Product>, IProductRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProductRepository"/>.
        /// </summary>
        /// <param name="database">База данных.</param>
        public ProductRepository(IMongoDatabase database) : base(database)
        {
            Entities = database.GetCollection<Product>("Products");
        }

        /// <summary>
        /// Получить список продуктов.
        /// </summary>
        /// <returns>Список продуктов.</returns>
        public async Task<IList<Product>> GetEntities()
        {
            return await Entities.Find(FilterDefinition<Product>.Empty).ToListAsync();
        }

        /// <summary>
        /// Получить отфильтрованный список продуктов.
        /// </summary>
        /// <param name="name">Фильтр по наименованию.</param>
        /// <param name="category">Фильтр по категории.</param>
        /// <returns>Отфильтрованный список продуктов.</returns>
        public async Task<IList<Product>> GetEntities(string name, string category)
        {
            var filter1 = Builders<Product>.Filter.Regex("Name", new BsonRegularExpression(name));
            var filter2 = Builders<Product>.Filter.Eq("Category", category);
            if (category == null)
                return await Entities.Find(filter1).ToListAsync();
            else
                return await Entities.Find(filter2).ToListAsync();
        }

        /// <summary>
        /// Добавить продукт.
        /// </summary>
        /// <param name="entity">Продукт.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        public async Task AddEntity(Product entity)
        {
            await Entities.InsertOneAsync(entity);
        }

        /// <summary>
        /// Удалить продукт.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        public async Task DeleteEntity(string id)
        {
            var filter = Builders<Product>.Filter.Eq("_id", id);
            await Entities.DeleteOneAsync(filter);
        }


        /// <summary>
        /// Обновить продукт.
        /// </summary>
        /// <param name="entity">Продукт.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        public async Task UpdateEntity(Product entity)
        {
            var filter = Builders<Product>.Filter.Eq("_id", entity.Id);
            await Entities.ReplaceOneAsync(filter, entity);
        }

        /// <summary>
        /// Получить продукт.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Продукт.</returns>
        public async Task<Product> GetEntity(string id)
        {
            var filter = Builders<Product>.Filter.Eq("_id", id);
            return await Entities.Find(filter).FirstOrDefaultAsync();
        }
    }
}
