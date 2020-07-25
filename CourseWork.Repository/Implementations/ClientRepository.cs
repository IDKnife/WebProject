using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CourseWork.Repositories.Implementations
{
    public class ClientRepository : MongoRepository<Client>, IClientRepository
    {
        public ClientRepository(IMongoDatabase database) : base(database)
        {
            Entities = database.GetCollection<Client>("Clients");
        }

        public async Task<IList<Client>> GetEntities()
        {
            return await Entities.Find(FilterDefinition<Client>.Empty).ToListAsync();
        }

        public async Task AddEntity(Client entity)
        {
            await Entities.InsertOneAsync(entity);
        }

        public async Task DeleteEntity(int id)
        {
            var filter = Builders<Client>.Filter.Eq("_id", id);
            await Entities.DeleteOneAsync(filter);
        }

        public async Task UpdateEntity(Client entity)
        {
            var filter = Builders<Client>.Filter.Eq("_id", entity.Id);
            await Entities.ReplaceOneAsync(filter, entity);
        }

        public async Task<Client> GetEntity(int id)
        {
            var filter = Builders<Client>.Filter.Eq("_id", id);
            return await Entities.Find(filter).FirstOrDefaultAsync();
        }
    }
}
