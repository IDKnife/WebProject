using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Repositories.Interfaces;
using CourseWork.Services.Interfaces;

namespace CourseWork.Services.Implementations
{
    /// <summary>
    /// Представляет сервис заказов.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OrderService"/>.
        /// </summary>
        /// <param name="repository">Репозиторий.</param>
        public OrderService(IOrderRepository repository)
            => _repository = repository;

        /// <inheritdoc cref="IOrderService.GetOrders"/>
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

        /// <inheritdoc cref="IOrderService.AddOrder"/>
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

        /// <inheritdoc cref="IOrderService.DeleteOrder"/>
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

        /// <inheritdoc cref="IOrderService.UpdateOrder"/>
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

        /// <inheritdoc cref="IOrderService.GetOrder"/>
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

        /// <inheritdoc cref="IOrderService.AddProductToOrder"/>
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

        /// <inheritdoc cref="IOrderService.DeleteProductFromOrder"/>
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

        /// <inheritdoc cref="IOrderService.UpdateProductCountInOrder"/>
        public async Task<ServiceOperationResult> UpdateProductCountInOrder(
            string productId,
            int newCount,
            string orderId)
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

        /// <inheritdoc cref="IOrderService.GetPriceOfOrder"/>
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

        /// <inheritdoc cref="IOrderService.DeleteEmptyAnonymOrders"/>
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
