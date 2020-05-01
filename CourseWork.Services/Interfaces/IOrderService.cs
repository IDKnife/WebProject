using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IList<Order>> GetOrders();
        Task AddOrder(Order entity);
        Task DeleteOrder(Order entity);
        Task UpdateOrder(Order entity);
        Task<Order> GetOrder(int id);
    }
}
