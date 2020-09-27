using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Repositories.Interfaces;
using CourseWork.Services.Interfaces;
using MongoDB.Bson;

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

        public async Task<ServiceOperationResult> DeleteOrder(string id)
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

        public async Task<Order> GetOrder(string id)
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

        public async Task<ServiceOperationResult> AddProductToOrder(Product product, string orderId)
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

        public async Task<ServiceOperationResult> DeleteProductFromOrder(string productId, string orderId)
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

        public async Task<ServiceOperationResult> UpdateProductCountInOrder(string productId, int newCount, string orderId)
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

        public async Task<double> GetPriceOfOrder(string id)
        {
            try
            {
                var order = await _repository.GetEntity(id);
                if (order != null)
                    return order.GetPriceOfOrder();
                else
                    return 0;
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }

        /// <summary>
        /// Удалить пустые анонимные заказы кроме текущего, которые были созданы за день или более относительно текущей даты.
        /// </summary>
        /// <param name="id">Уникальный идентификатор текущего заказа.</param>
        /// <returns>Результат операции.</returns>
        public async Task<ServiceOperationResult> DeleteEmptyAnonymOrders(string id)
        {
            try
            {
                var orders = await _repository.GetEntities();
                if (orders != null)
                {
                    foreach (var order in orders)
                        if (order.ClientId == "anonym" && order.Basket.Products.Count == 0 && order.Id != id && order.Date < DateTime.Today)
                            await _repository.DeleteEntity(order.Id);
                    return new ServiceOperationResult(true, "Success");
                }
                return new ServiceOperationResult(false, $"Fail: List of orders is empty");
            }
            catch (Exception e)
            {
                //ToDo: логирование
                return new ServiceOperationResult(false, $"Fail: {e.Message}");
            }
        }
    }
}
