using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.WebApi.Interfaces;

namespace CourseWork.WebApi.ViewModels
{
    public class OrderViewModel : IViewModel, ICanBeSerialised
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Уникальный идентификатор клиента, которому заказ принадлежит.
        /// </summary>
        public int ClientId { get; set; }
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
        /// Преобразовать модель в сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        public Entity ToEntity()
        {
            if (Basket.Products.Count != 0)
                return new Order(ClientId, Basket, Id, State, Date);
            else
                return new Order(ClientId, Id);
        }
    }
}
