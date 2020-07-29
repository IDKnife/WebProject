using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Basket
    {
        public List<ProductAndCount> Products { get; set; }
        public Basket()
        {
            Products = new List<ProductAndCount>();
        }

        public Basket(List<ProductAndCount> products)
        {
            Products = products;
        }
    }
}
