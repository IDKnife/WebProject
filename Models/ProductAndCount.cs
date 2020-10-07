namespace CourseWork.Models
{
    /// <summary>
    /// Представляет модель заказанного продукта и количество его единиц.
    /// </summary>
    public class ProductAndCount
    {
        /// <summary>
        /// Продукт.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Количество единиц продукта.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProductAndCount"/> с заданными значениями.
        /// </summary>
        /// <param name="product">Продукт.</param>
        /// <param name="count">Количество единиц продукта.</param>
        public ProductAndCount(Product product, int count)
        {
            Product = product;
            Count = count;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProductAndCount"/> с заданным продуктом в количестве 1 единица.
        /// </summary>
        /// <param name="product">Продукт.</param>
        public ProductAndCount(Product product)
        {
            Product = product;
            Count = 1;
        }
    }
}
