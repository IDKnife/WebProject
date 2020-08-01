using System;
using System.Collections.Generic;
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

        public async Task AddOrder(Order entity)
        {
            try
            {
                await _repository.AddEntity(entity);
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }

        public async Task DeleteOrder(int id)
        {
            try
            {
                await _repository.DeleteEntity(id);
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }

        public async Task UpdateOrder(Order entity)
        {
            try
            {
                await _repository.UpdateEntity(entity);
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
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

        public async Task AddProductToBasket(Product product, int orderId)
        {
            try
            {
                var order = await _repository.GetEntity(orderId);
                order.Basket.Products.Add(new ProductAndCount(product, 1));
                await _repository.UpdateEntity(order);
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }

        public async Task DeleteProductFromBasket(int productId, int orderId)
        {
            try
            {
                var order = await _repository.GetEntity(orderId);
                var item = order.Basket.Products.Find(a => a.Product.Id == productId);
                order.Basket.Products.Remove(item);
                await _repository.UpdateEntity(order);
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }

        public async Task UpdateProductCountInBasket(int productId, int newCount, int orderId)
        {
            try
            {
                var order = await _repository.GetEntity(orderId);
                var item = order.Basket.Products.Find(a => a.Product.Id == productId);
                item.Count = newCount;
                await _repository.UpdateEntity(order);
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }

        public async Task<double> GetPriceOfBasket(int id)
        {
            try
            {
                var order = await _repository.GetEntity(id);
                double price = 0;
                foreach (var item in order.Basket.Products)
                    price += (item.Product.Price * item.Count);
                return price;
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }
    }
}
