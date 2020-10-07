using System.Collections.Generic;
using System.Linq;
using CourseWork.Models;

namespace CourseWork.AdditionalClasses.ViewModels
{
    /// <summary>
    /// Представляет модель представления корзины.
    /// </summary>
    public class BasketViewModel
    {
        /// <summary>
        /// Список продуктов и их количества.
        /// </summary>
        public List<ProductAndCountViewModel> Products { get; set; }

        /// <summary>
        /// Преобразовать в корзину.
        /// </summary>
        /// <returns>Корзина.</returns>
        public Basket ToEntity()
            => new Basket(Products
                .Select(p => p.ToEntity())
                .ToList());

    }
}
