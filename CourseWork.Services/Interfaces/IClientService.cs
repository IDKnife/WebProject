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
        Task AddClient(Client entity);
        Task DeleteClient(Client entity);
        Task UpdateClient(Client entity);
        Task<Client> GetClient(int id);
    }
}
