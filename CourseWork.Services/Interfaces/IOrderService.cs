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
        Task DeleteOrder(int id);
        Task UpdateOrder(Order entity);
        Task<Order> GetOrder(int id);
        Task AddProductToBasket(Product product, int orderId);
        Task DeleteProductFromBasket(int productId, int orderId);
        Task UpdateProductCountInBasket(int productId, int newCount, int orderId);
        Task<double> GetPriceOfBasket(int id);
    }
}
