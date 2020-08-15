using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models;
using MongoDB.Bson;

namespace CourseWork.Services.Interfaces
{
    public interface IClientService
    {
        Task<IList<Client>> GetClients();
        Task<ServiceOperationResult> AddClient(Client entity);
        Task<ServiceOperationResult> DeleteClient(string id);
        Task<ServiceOperationResult> UpdateClient(Client entity);
        Task<Client> GetClient(string id);
        Task<ServiceOperationResult> AddOrderToClientList(Order order, string clientId);
    }
}
