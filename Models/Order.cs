using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CourseWork.Models
{
    public class Order : Entity
    {
        public string ClientId { get; set; }
        public Basket Basket { get; set; }
        public DateTime Date { get; set; }
        public OrderState State { get; set; }


        public double GetPriceOfOrder() => Basket.Products.Sum(item => item.Product.Price * item.Count);

        public void UpdateProductCountInOrder(string productId, int newCount) =>
            Basket.Products.Find(a => a.Product.Id == productId)
                .Count = newCount;

        public void DeleteProductFromOrder(string productId)
        {
            var item = Basket.Products.Find(a => a.Product.Id == productId);
            Basket.Products.Remove(item);
        }
        public void AddProductToOrder(Product product) => Basket.Products.Add(new ProductAndCount(product, 1));
        public Order(string clientId) 
        {
            ClientId = clientId;
            Basket = new Basket();
            State = OrderState.Forming;
            Date = DateTime.Now;
        }
        public Order(string id, string clientId, Basket basket, OrderState state, DateTime date) : base(id)
        {
            ClientId = clientId;
            Basket = basket;
            State = state;
            Date = date;
        }
    }
}
