using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.Services.Interfaces
{
    public interface IClientService
    {
        Task<IList<Client>> GetClients();
        Task<ServiceOperationResult> AddClient(Client entity);
        Task<ServiceOperationResult> DeleteClient(int id);
        Task<ServiceOperationResult> UpdateClient(Client entity);
        Task<Client> GetClient(int id);
        Task<ServiceOperationResult> AddOrderToClientList(Order order, int clientId);
    }
}
