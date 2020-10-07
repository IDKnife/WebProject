using System;
using CourseWork.Models;
using CourseWork.AdditionalClasses.Interfaces;

namespace CourseWork.AdditionalClasses.ViewModels
{
    /// <summary>
    /// Представляет модель представления заказа.
    /// </summary>
    public class OrderViewModel : IViewModel, ICanBeSerialised
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор клиента, которому заказ принадлежит.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Корзина товаров заказа.
        /// </summary>
        public BasketViewModel Basket { get; set; }

        /// <summary>
        /// Дата заказа.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Статус заказа.
        /// </summary>
        public OrderState State { get; set; }

        /// <inheritdoc cref="ICanBeSerialised.ToEntity"/>
        public Entity ToEntity()
        {
            var basket = Basket.Products.Count != 0
                ? Basket.ToEntity()
                : new Basket();
            return new Order(Id, ClientId, basket, State, Date);
        }

        /// <inheritdoc cref="ICanBeSerialised.ToNewEntity"/>
        public Entity ToNewEntity()
            => new Order(ClientId);
    }
}
