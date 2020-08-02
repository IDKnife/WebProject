using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Repositories.Interfaces;
using CourseWork.Services.Interfaces;

namespace CourseWork.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task<IList<Order>> GetOrders()
        {
            try
            {
                return await _repository.GetEntities();
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }

        public async Task<ServiceOperationResult> AddOrder(Order entity)
        {
            try
            {
                await _repository.AddEntity(entity);
                return new ServiceOperationResult(true, "Success");
            }
            catch (Exception e)
            {
                //ToDo: логирование
                return new ServiceOperationResult(false, $"Fail: {e.Message}");
            }
        }

        public async Task<ServiceOperationResult> DeleteOrder(int id)
        {
            try
            {
                await _repository.DeleteEntity(id);
                return new ServiceOperationResult(true, "Success");
            }
            catch (Exception e)
            {
                //ToDo: логирование
                return new ServiceOperationResult(false, $"Fail: {e.Message}");
            }
        }

        public async Task<ServiceOperationResult> UpdateOrder(Order entity)
        {
            try
            {
                await _repository.UpdateEntity(entity);
                return new ServiceOperationResult(true, "Success");
            }
            catch (Exception e)
            {
                //ToDo: логирование
                return new ServiceOperationResult(false, $"Fail: {e.Message}");
            }
        }

        public async Task<Order> GetOrder(int id)
        {
            try
            {
                return await _repository.GetEntity(id);
            }
            catch (Exception)
            {
                //ToDo: логирование
                throw;
            }
        }

        public async Task<ServiceOperationResult> AddProductToOrder(Product product, int orderId)
        {
            try
            {
                var order = await _repository.GetEntity(orderId);
                order.AddProductToOrder(product);
                await _repository.UpdateEntity(order);
                return new ServiceOperationResult(true, "Success");
            }
            catch (Exception e)
            {
                //ToDo: логирование
                return new ServiceOperationResult(false, $"Fail: {e.Message}");
            }
        }

        public async Task<ServiceOperationResult> DeleteProductFromOrder(int productId, int orderId)
        {
            try
            {
                var order = await _repository.GetEntity(orderId);
                order.DeleteProductFromOrder(productId);
                await _repository.UpdateEntity(order);
                return new ServiceOperationResult(true, "Success");
            }
            catch (Exception e)
            {
                //ToDo: логирование
                return new ServiceOperationResult(false, $"Fail: {e.Message}");
            }
        }

        public async Task<ServiceOperationResult> UpdateProductCountInOrder(int productId, int newCount, int orderId)
        {
            try
            {
                var order = await _repository.GetEntity(orderId);
                order.UpdateProductCountInOrder(productId, newCount);
                await _repository.UpdateEntity(order);
                return new ServiceOperationResult(true, "Success");
            }
            catch (Exception e)
            {
                //ToDo: логирование
                return new ServiceOperationResult(false, $"Fail: {e.Message}");
            }
        }

        public async Task<double> GetPriceOfOrder(int id)
        {
            try
            {
                var order = await _repository.GetEntity(id);
                return order.GetPriceOfOrder();
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }
    }
}
