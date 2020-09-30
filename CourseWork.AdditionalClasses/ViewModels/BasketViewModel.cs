using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.AdditionalClasses.ViewModels
{
    public class BasketViewModel
    {
        public List<ProductAndCountViewModel> Products { get; set; }
        public Basket ToEntity()
        {
            var products = new List<ProductAndCount>();
            foreach (var item in Products)
                products.Add(item.ToEntity());
            return new Basket(products);
        }

    }
}
