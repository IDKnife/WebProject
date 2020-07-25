﻿using CourseWork.Models;
using CourseWork.WebApi.Interfaces;

namespace CourseWork.WebApi.ViewModels
{
    /// <summary>
    /// Модель данных товара.
    /// </summary>
    public class ProductViewModel : IViewModel, ICanBeSerialised
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public int Id { get; set; }
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
        /// <summary>
        /// Преобразовать модель в сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        public Entity ToEntity()
        {
            return new Product(Id, Name, Price, Category, Description);
        }
    }
}
