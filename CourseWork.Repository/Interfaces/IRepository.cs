using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using CourseWork.Models;
using MongoDB.Driver;

namespace CourseWork.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : Entity
    {
        Task<IList<T>> GetEntities();
        Task AddEntity(T entity);
        Task DeleteEntity(T entity);
        Task UpdateEntity(T entity);
        Task<T> GetEntity(int id);
    }
}
