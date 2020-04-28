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

        public async Task DeleteEntity(Client entity)
        {
            await Entities.DeleteOneAsync(new BsonDocument("_id", new ObjectId(entity.Id.ToString())));
        }

        public async Task UpdateEntity(Client entity)
        {
            await Entities.ReplaceOneAsync(new BsonDocument("_id", entity.Id.ToString()), entity);
        }

        public async Task<Client> GetEntity(int id)
        {
            return await Entities.Find(new BsonDocument("_id", new ObjectId(id.ToString()))).FirstOrDefaultAsync();
        }
    }
}
