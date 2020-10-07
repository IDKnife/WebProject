using CourseWork.Models;
using CourseWork.AdditionalClasses.Interfaces;

namespace CourseWork.AdditionalClasses.ViewModels
{
    /// <summary>
    /// Представляет модель представления продукта.
    /// </summary>
    public class ProductViewModel : IViewModel, ICanBeSerialised
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Наименование товара.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Стоимость товара.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Категория товара.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Описание товара.
        /// </summary>
        public string Description { get; set; }

        /// <inheritdoc cref="ICanBeSerialised.ToEntity"/>
        public Entity ToEntity()
            => new Product(Id, Name, Price, Category, Description);

        /// <inheritdoc cref="ICanBeSerialised.ToNewEntity"/>
        public Entity ToNewEntity()
            => new Product(Name, Price, Category, Description);
    }
}
