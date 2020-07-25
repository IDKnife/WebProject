using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : Entity
    {
        Task<IList<T>> GetEntities();
        Task AddEntity(T entity);
        Task DeleteEntity(int id);
        Task UpdateEntity(T entity);
        Task<T> GetEntity(int id);
    }
}
