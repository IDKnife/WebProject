using System.Collections.Generic;

namespace CourseWork.Models
{
    /// <summary>
    /// Представляет модель корзины.
    /// </summary>
    public class Basket
    {
        /// <summary>
        /// Список продуктов в корзине.
        /// </summary>
        public List<ProductAndCount> Products { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Basket"/>.
        /// </summary>
        public Basket()
        {
            Products = new List<ProductAndCount>();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Basket"/> с заданным списком продуктов.
        /// </summary>
        /// <param name="products">Список продуктов.</param>
        public Basket(List<ProductAndCount> products)
        {
            Products = products;
        }
    }
}
