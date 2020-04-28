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
    public class OrderRepository : MongoRepository<Order>, IOrderRepository
    {
        public OrderRepository(IMongoDatabase database) : base(database)
        {
            Entities = database.GetCollection<Order>("Clients");
        }

        public async Task<IList<Order>> GetEntities()
        {
            return await Entities.Find(FilterDefinition<Order>.Empty).ToListAsync();
        }

        public async Task AddEntity(Order entity)
        {
            await Entities.InsertOneAsync(entity);
        }

        public async Task DeleteEntity(Order entity)
        {
            await Entities.DeleteOneAsync(new BsonDocument("_id", new ObjectId(entity.Id.ToString())));
        }

        public async Task UpdateEntity(Order entity)
        {
            await Entities.ReplaceOneAsync(new BsonDocument("_id", entity.Id.ToString()), entity);
        }

        public async Task<Order> GetEntity(int id)
        {
            return await Entities.Find(new BsonDocument("_id", new ObjectId(id.ToString()))).FirstOrDefaultAsync();
        }
    }
}
