using System.Collections.Generic;
using System.Security.Claims;

namespace CourseWork.Services.Interfaces
{
    /// <summary>
    /// Интерфейс создания токена авторизации.
    /// </summary>
    public interface ICreateTokenService
    {
        /// <summary>
        /// Создать токен авторизации.
        /// </summary>
        /// <param name="claims">Необходимые параметры клиента.</param>
        /// <returns>Закодированный токен аторизации.</returns>
        string CreateToken(IEnumerable<Claim> claims);
    }
}
