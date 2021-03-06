﻿using System;
using CourseWork.Models;
using MongoDB.Driver;

namespace CourseWork.Repositories.Implementations
{
    /// <summary>
    /// Представляет репозиторий для работы с базой данный MongoDB.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public abstract class MongoRepository<T> : IDisposable
    where T : Entity
    {
        /// <summary>
        /// База данных.
        /// </summary>
        protected IMongoDatabase Database { get; set; }

        /// <summary>
        /// Список сущностей.
        /// </summary>
        protected IMongoCollection<T> Entities { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MongoRepository"/>.
        /// </summary>
        /// <param name="database"></param>
        public MongoRepository(IMongoDatabase database)
            => Database = database;

        /// <summary>
        /// Уничтожить данный экземпляр класса <see cref="MongoRepository"/>.
        /// </summary>
        public void Dispose()
        {
            Entities = null;
            Database = null;
        }

        /// <summary>
        /// Составить фильтр равенства уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Фильтр равенства уникальному идентификатору.</returns>
        protected FilterDefinition<T> IdFilter(string id)
            => Builders<T>.Filter.Eq("_id", id);

        /// <summary>
        /// Составить пустой фильтр.
        /// </summary>
        /// <returns>Пустой фильтр.</returns>
        protected FilterDefinition<T> EmptyFilter()
            => FilterDefinition<T>.Empty;
    }
}
