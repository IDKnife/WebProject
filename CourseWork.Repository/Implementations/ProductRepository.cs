using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CourseWork.Repositories.Implementations
{
    public class ProductRepository : MongoRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDatabase database) : base(database)
        {
            Entities = database.GetCollection<Product>("Products");
        }

        public async Task<IList<Product>> GetEntities()
        {
            return await Entities.Find(FilterDefinition<Product>.Empty).ToListAsync();
        }

        public async Task<IList<Product>> GetEntities(string name, string category)
        {
            var filter1 = Builders<Product>.Filter.Regex("Name", new BsonRegularExpression(name));
            var filter2 = Builders<Product>.Filter.Eq("Category", category);
            if (category == null)
                return await Entities.Find(filter1).ToListAsync();
            else
                return await Entities.Find(filter2).ToListAsync();
        }

        public async Task AddEntity(Product entity)
        {
            await Entities.InsertOneAsync(entity);
        }

        public async Task DeleteEntity(int id)
        {
            var filter = Builders<Product>.Filter.Eq("_id", id);
            await Entities.DeleteOneAsync(filter);
        }

        public async Task UpdateEntity(Product entity)
        {
            var filter = Builders<Product>.Filter.Eq("_id", entity.Id);
            await Entities.ReplaceOneAsync(filter, entity);
        }

        public async Task<Product> GetEntity(int id)
        {
            var filter = Builders<Product>.Filter.Eq("_id", id);
            return await Entities.Find(filter).FirstOrDefaultAsync();
        }
    }
}
