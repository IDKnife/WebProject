using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.AdditionalClasses.Interfaces;
using MongoDB.Bson;

namespace CourseWork.AdditionalClasses.ViewModels
{
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
        /// <summary>
        /// Преобразовать модель в сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        public Entity ToEntity()
        {
            if (Basket.Products.Count != 0)
                return new Order(Id, ClientId, Basket.ToEntity(), State, Date);
            return new Order(Id, ClientId, new Basket(), State, Date);
        }

        public Entity ToNewEntity() => new Order(ClientId);
    }
}
