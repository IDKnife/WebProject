using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseWork.Services.Interfaces
{
    /// <summary>
    /// Интерфейс идентификации клиента.
    /// </summary>
    public interface IIdentificationService
    {
        /// <summary>
        /// Процесс идентификации клиента.
        /// </summary>
        /// <param name="email">Электронная почта клиента.</param>
        /// <param name="password">Пароль клиента.</param>
        /// <returns>Результат идентификации.</returns>
        Task<ClaimsIdentity> CheckIdentity(string email, string password);
    }
}
