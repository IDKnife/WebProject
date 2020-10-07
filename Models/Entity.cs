using MongoDB.Bson;

namespace CourseWork.Models
{
    /// <summary>
    /// Представляет модель сущности.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///  Инициализирует новый экземпляр класса <see cref="Entity"/> и создает для него новый уникальный идентификатор.
        /// </summary>
        public Entity()
            => Id = ObjectId.GenerateNewId().ToString();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Entity"/> с заданным уникальным идентификатором.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        public Entity(string id)
            => Id = id;
    }
}
