﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Repositories.Interfaces;
using CourseWork.Services.Interfaces;

namespace CourseWork.Services.Implementations
{
    /// <summary>
    /// Представляет сервис клиентов.
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly IOrderRepository _orderRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClientService"/>.
        /// </summary>
        /// <param name="repository">Репозиторий клиентов.</param>
        /// <param name="orderRepository">Репозиторий заказов.</param>
        public ClientService(IClientRepository repository, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _repository = repository;
        }

        /// <inheritdoc cref="IClientService.GetClients"/>
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

        /// <inheritdoc cref="IClientService.AddClient"/>
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

        /// <inheritdoc cref="IClientService.DeleteClient"/>
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

        /// <inheritdoc cref="IClientService.UpdateClient"/>
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

        /// <inheritdoc cref="IClientService.GetClient"/>
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

        /// <inheritdoc cref="IClientService.AddOrderToClientList"/>
        public async Task<ServiceOperationResult> AddOrderToClientList(Order order, string clientId)
        {
            try
            {
                var client = await _repository.GetEntity(clientId);
                order.ClientId = clientId;
                order.State = OrderState.Payed;
                await _orderRepository.UpdateEntity(order);
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
