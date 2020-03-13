using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class Basket
    {
        public Dictionary<Product, int> Products { get; set; }
        public Basket()
        {
            Products = new Dictionary<Product, int>();
        }
    }
}
