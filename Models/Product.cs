using MongoDB.Bson;

namespace CourseWork.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        //ToDo: Сделать категорию сущностью
        public string Category { get; set; }
        public string Description { get; set; }

        public Product(string name, double price, string category, string description)
        {
            Name = name;
            Price = price;
            Category = category;
            Description = description;
        }

        public Product(string id, string name, double price, string category, string description) : base(id)
        {
            Name = name;
            Price = price;
            Category = category;
            Description = description;
        }
    }
}
