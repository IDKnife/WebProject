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
    public abstract class MongoRepository<T> : IDisposable
    where T : Entity
    {
        protected IMongoDatabase Database { get; set; }
        protected IMongoCollection<T> Entities { get; set; }
        public MongoRepository(IMongoDatabase database)
        {
            Database = database;
        }

        public void Dispose()
        {
            Entities = null;
            Database = null;
        }
    }
}
