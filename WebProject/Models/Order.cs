using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class Order : Entity
    {
        public Client Client { get; set; }
        public Basket Basket { get; set; }
        public DateTime Date { get; set; }
        public OrderState State { get; set; }
        public Order(Client client, int id) : base(id)
        {
            Client = client;
            Basket = new Basket();
            State = OrderState.Forming;
            Date = DateTime.Now;
        }
        public Order(Client client, Basket basket, int id, OrderState state, DateTime date) : base(id)
        {
            Client = client;
            Basket = basket;
            State = state;
            Date = date;
        }
    }
}
