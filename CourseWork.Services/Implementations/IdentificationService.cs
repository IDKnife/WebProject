using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseWork.Services.Interfaces;

namespace CourseWork.Services.Implementations
{
    /// <summary>
    /// Представляет процесс идентификации клиента.
    /// </summary>
    public class IdentificationService : IIdentificationService
    {
        private readonly IClientService _clientService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="IdentificationService"/>.
        /// </summary>
        /// <param name="clientService">Сервис для работы с базой клиентов.</param>
        public IdentificationService(IClientService clientService)
            => _clientService = clientService;

        /// <inheritdoc cref="IIdentificationService.CheckIdentity(string, string)"/>
        public async Task<ClaimsIdentity> CheckIdentity(string email, string password)
        {
            var clients = await _clientService.GetClients();
            var client = clients.FirstOrDefault(item => item.Email == email && item.Password == password);
            if (client == null)
                return null;
            var claims = new List<Claim>
            {
                new Claim("Access", client.Access.ToString()),
                new Claim("Id", client.Id),
                new Claim("Email", client.Email)
            };
            return new ClaimsIdentity(claims, "Token");
        }
    }
}
