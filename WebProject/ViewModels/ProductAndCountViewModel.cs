using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.WebApi.ViewModels
{
    public class ProductAndCountViewModel
    {
        public ProductViewModel Product { get; set; }
        public int Count { get; set; }
        public ProductAndCount ToEntity()
        {
            return new ProductAndCount(Product.ToEntity() as Product, Count);
        }

    }
}
