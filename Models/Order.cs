using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Order : Entity
    {
        public int ClientId { get; set; }
        public Basket Basket { get; set; }
        public DateTime Date { get; set; }
        public OrderState State { get; set; }

        public Order(int clientId, int id) : base(id)
        {
            ClientId = clientId;
            Basket = new Basket();
            State = OrderState.Forming;
            Date = DateTime.Now;
        }
        public Order(int clientId, Basket basket, int id, OrderState state, DateTime date) : base(id)
        {
            ClientId = clientId;
            Basket = basket;
            State = state;
            Date = date;
        }
    }
}
