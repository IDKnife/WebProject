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
            Entities = database.GetCollection<Order>("Orders");
        }

        public async Task<IList<Order>> GetEntities()
        {
            return await Entities.Find(FilterDefinition<Order>.Empty).ToListAsync();
        }

        public async Task AddEntity(Order entity)
        {
            await Entities.InsertOneAsync(entity);
        }

        public async Task DeleteEntity(int id)
        {
            var filter = Builders<Order>.Filter.Eq("_id", id);
            await Entities.DeleteOneAsync(filter);
        }

        public async Task UpdateEntity(Order entity)
        {
            var filter = Builders<Order>.Filter.Eq("_id", entity.Id);
            await Entities.ReplaceOneAsync(filter, entity);
        }

        public async Task<Order> GetEntity(int id)
        {
            var filter = Builders<Order>.Filter.Eq("_id", id);
            return await Entities.Find(filter).FirstOrDefaultAsync();
        }
    }
}
