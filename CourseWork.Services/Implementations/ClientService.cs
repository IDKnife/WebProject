using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Repositories.Interfaces;
using CourseWork.Services.Interfaces;

namespace CourseWork.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
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

        public async Task AddClient(Client entity)
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

        public async Task DeleteClient(int id)
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

        public async Task UpdateClient(Client entity)
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

        public async Task<Client> GetClient(int id)
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

        public async Task AddOrderToClientList(Order order, int clientId)
        {
            try
            {
                var client = await _repository.GetEntity(clientId);
                client.Orders.Add(order);
                await _repository.UpdateEntity(client);
            }
            catch (Exception)
            {
                //ToDo: логирование
                throw;
            }
        }
    }
}
