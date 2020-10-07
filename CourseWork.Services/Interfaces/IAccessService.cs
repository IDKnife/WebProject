using System.Security.Claims;

namespace CourseWork.Services.Interfaces
{
    /// <summary>
    /// Интерфейс проверки уровня доступа.
    /// </summary>
    public interface IAccessService
    {
        /// <summary>
        /// Проверить является ли клиент админом.
        /// </summary>
        /// <param name="user">Данные пользователя.</param>
        /// <returns>Результат проверки.</returns>
        bool IsAdmin(ClaimsPrincipal user);

        /// <summary>
        /// Проверить является ли клиент простым пользователем.
        /// </summary>
        /// <param name="user">Данные пользователя.</param>
        /// <returns>Результат проверки.</returns>
        bool IsUser(ClaimsPrincipal user);
    }
}
