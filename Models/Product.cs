using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public Product(int id, string name, double price, string category, string description) : base(id)
        {
            Name = name;
            Price = price;
            Category = category;
            Description = description;
        }
    }
}
