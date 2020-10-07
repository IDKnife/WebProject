namespace CourseWork.Models
{
    /// <summary>
    /// Представляет модель продукта.
    /// </summary>
    public class Product : Entity
    {
        /// <summary>
        /// Наименование товара.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Стоимость товара.
        /// </summary>
        public double Price { get; set; }

        //ToDo: Сделать категорию сущностью
        /// <summary>
        /// Категория товара.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Описание товара.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Product"/> с заданными значениями, кроме уникального идентификатора.
        /// </summary>
        /// <param name="name">Наименование товара.</param>
        /// <param name="price">Стоимость товара.</param>
        /// <param name="category">Категория товара.</param>
        /// <param name="description">Описание товара.</param>
        public Product(
            string name,
            double price,
            string category,
            string description)
        {
            Name = name;
            Price = price;
            Category = category;
            Description = description;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Product"/> с заданными значениями.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="name">Наименование товара.</param>
        /// <param name="price">Стоимость товара.</param>
        /// <param name="category">Категория товара.</param>
        /// <param name="description">Описание товара.</param>
        public Product(
            string id,
            string name,
            double price,
            string category,
            string description) : base(id)
        {
            Name = name;
            Price = price;
            Category = category;
            Description = description;
        }
    }
}
