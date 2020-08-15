using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models;
using MongoDB.Bson;

namespace CourseWork.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IList<Order>> GetOrders();
        Task<ServiceOperationResult> AddOrder(Order entity);
        Task<ServiceOperationResult> DeleteOrder(string id);
        Task<ServiceOperationResult> UpdateOrder(Order entity);
        Task<Order> GetOrder(string id);
        Task<ServiceOperationResult> AddProductToOrder(Product product, string orderId);
        Task<ServiceOperationResult> DeleteProductFromOrder(string productId, string orderId);
        Task<ServiceOperationResult> UpdateProductCountInOrder(string productId, int newCount, string orderId);
        Task<double> GetPriceOfOrder(string id);
    }
}
