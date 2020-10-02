using System;
using System.Linq;

namespace CourseWork.Models
{
    /// <summary>
    /// Представляет модель заказа.
    /// </summary>
    public class Order : Entity
    {
        /// <summary>
        /// Уникальный идентификатор клиента, которому заказ принадлежит.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Корзина товаров заказа.
        /// </summary>
        public Basket Basket { get; set; }

        /// <summary>
        /// Дата заказа.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Статус заказа.
        /// </summary>
        public OrderState State { get; set; }

        /// <summary>
        /// Получить итоговую стоимость заказа.
        /// </summary>
        /// <returns>Стоимость заказа.</returns>
        public double GetPriceOfOrder() => Basket.Products.Sum(item => item.Product.Price * item.Count);

        public void UpdateProductCountInOrder(string productId, int newCount) =>
            Basket.Products.Find(a => a.Product.Id == productId)
                .Count = newCount;

        /// <summary>
        /// Удалить продукт из заказа.
        /// </summary>
        /// <param name="productId">Уникальный идентификатор продукта.</param>
        public void DeleteProductFromOrder(string productId)
        {
            var item = Basket.Products.Find(a => a.Product.Id == productId);
            Basket.Products.Remove(item);
        }

        /// <summary>
        /// Добавить продукт в заказ.
        /// </summary>
        /// <param name="product">Продукт.</param>
        public void AddProductToOrder(Product product) => Basket.Products.Add(new ProductAndCount(product, 1));

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Order"/> для заданного клиента.
        /// </summary>
        /// <param name="clientId">Уникальный идентификатор клиента.</param>
        public Order(string clientId) 
        {
            ClientId = clientId;
            Basket = new Basket();
            State = OrderState.Forming;
            Date = DateTime.Now;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Order"/> с заданными значениями.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="clientId">Уникальный идентификатор клиента.</param>
        /// <param name="basket">Корзина товаров заказа.</param>
        /// <param name="state">Статус заказа.</param>
        /// <param name="date">Дата заказа.</param>
        public Order(string id, string clientId, Basket basket, OrderState state, DateTime date) : base(id)
        {
            ClientId = clientId;
            Basket = basket;
            State = state;
            Date = date;
        }
    }
}
