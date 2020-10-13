using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Services.Interfaces
{
    /// <summary>
    /// Интерфейс логгированных ответов сервера.
    /// </summary>
    public interface ILoggedRequestsService
    {
        /// <summary>
        /// Вернуть логгированный BadRequest.
        /// </summary>
        /// <param name="message">Сообщение, содержащее детали ошибки.</param>
        /// <returns>Логгированный BadRequest.</returns>
        BadRequestObjectResult BadLoggedRequest(string message);
    }
}
