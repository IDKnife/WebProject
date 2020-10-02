using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория.
    /// </summary>
    public interface IRepository<T> : IDisposable
        where T : Entity
    {
        /// <summary>
        /// Получить список сущностей.
        /// </summary>
        /// <returns>Список сущностей.</returns>
        Task<IList<T>> GetEntities();

        /// <summary>
        /// Добавить сущность.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        Task AddEntity(T entity);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        Task DeleteEntity(string id);

        /// <summary>
        /// Обновить сущность.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Задача, ожидающая выполнения.</returns>
        Task UpdateEntity(T entity);

        /// <summary>
        /// Получить сущность.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        Task<T> GetEntity(string id);
    }
}
