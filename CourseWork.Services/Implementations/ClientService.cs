using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Repositories.Interfaces;
using CourseWork.Services.Interfaces;
using MongoDB.Bson;

namespace CourseWork.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly IOrderRepository _order_repository;

        public ClientService(IClientRepository repository, IOrderRepository order_repository)
        {
            _order_repository = order_repository;
            _repository = repository;
        }
        public async Task<IList<Client>> GetClients()
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

        public async Task<ServiceOperationResult> AddClient(Client entity)
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

        public async Task<ServiceOperationResult> DeleteClient(string id)
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

        public async Task<ServiceOperationResult> UpdateClient(Client entity)
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

        public async Task<Client> GetClient(string id)
        {
            try
            {
                return await _repository.GetEntity(id);
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }

        public async Task<ServiceOperationResult> AddOrderToClientList(Order order, string clientId)
        {
            try
            {
                var client = await _repository.GetEntity(clientId);
                order.ClientId = clientId;
                await _order_repository.UpdateEntity(order);
                client.Orders.Add(order);
                await _repository.UpdateEntity(client);
                return new ServiceOperationResult(true, "Success");
            }
            catch (Exception e)
            {
                //ToDo: логирование
                return new ServiceOperationResult(false, $"Fail: {e.Message}");
            }
        }
    }
}
