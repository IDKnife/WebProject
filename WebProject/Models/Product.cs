using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public Product(int id, string name, double price, string category) : base(id)
        {
            Name = name;
            Price = price;
            Category = category;
        }
    }
}
