using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork.Models
{
    public class ProductAndCount
    {
        public Product Product { get; set; }
        public int Count { get; set; }

        public ProductAndCount(Product product, int count)
        {
            Product = product;
            Count = count;
        }
    }
}
