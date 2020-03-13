using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class Order
    {
        public Client Client { get; set; }
        public Basket Basket { get; set; }
        public int Id { get; }
        public DateTime Date { get; set; }
        public OrderState State { get; set; }
        public Order(Client client, int id)
        {
            Client = client;
            Basket = new Basket();
            Id = id;
            State = OrderState.Forming;
            Date = DateTime.Now;
        }
        public Order(Client client, Basket basket, int id, OrderState state, DateTime date)
        {
            Client = client;
            Basket = basket;
            Id = id;
            State = state;
            Date = date;
        }
    }
}
