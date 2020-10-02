using CourseWork.Models;

namespace CourseWork.AdditionalClasses.ViewModels
{
    /// <summary>
    /// Представляет модель представления заказанного продукта и количество его единиц.
    /// </summary>
    public class ProductAndCountViewModel
    {
        /// <summary>
        /// Продукт.
        /// </summary>
        public ProductViewModel Product { get; set; }

        /// <summary>
        /// Количество единиц продукта.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Преобразовать в заказанный продукт и количество его единиц.
        /// </summary>
        /// <returns>Заказанный продукт и количество его единиц.</returns>
        public ProductAndCount ToEntity()
        {
            return new ProductAndCount(Product.ToEntity() as Product, Count);
        }

    }
}
