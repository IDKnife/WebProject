using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task AddEntity(Product entity)
        {
            await Entities.InsertOneAsync(entity);
        }

        public async Task DeleteEntity(Product entity)
        {
            await Entities.DeleteOneAsync(new BsonDocument("_id", new ObjectId(entity.Id.ToString())));
        }

        public async Task UpdateEntity(Product entity)
        {
            await Entities.ReplaceOneAsync(new BsonDocument("_id", entity.Id.ToString()), entity);
        }

        public async Task<Product> GetEntity(int id)
        {
            return await Entities.Find(new BsonDocument("_id", new ObjectId(id.ToString()))).FirstOrDefaultAsync();
        }
    }
}
