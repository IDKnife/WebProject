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
        Task<ServiceOperationResult> AddOrder(Order entity);
        Task<ServiceOperationResult> DeleteOrder(int id);
        Task<ServiceOperationResult> UpdateOrder(Order entity);
        Task<Order> GetOrder(int id);
        Task<ServiceOperationResult> AddProductToOrder(Product product, int orderId);
        Task<ServiceOperationResult> DeleteProductFromOrder(int productId, int orderId);
        Task<ServiceOperationResult> UpdateProductCountInOrder(int productId, int newCount, int orderId);
        Task<double> GetPriceOfOrder(int id);
    }
}
