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

        public async Task DeleteOrder(Order entity)
        {
            try
            {
                await _repository.DeleteEntity(entity);
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
    }
}
