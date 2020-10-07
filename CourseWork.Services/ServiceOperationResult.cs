namespace CourseWork.Services.Interfaces
{
    /// <summary>
    /// Представляет результат операции.
    /// </summary>
    public class ServiceOperationResult
    {
        /// <summary>
        /// Значение результата операции.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Сообщение результата операции.
        /// </summary>
        public string MessageResult { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ServiceOperationResult"/> с заданными значениями.
        /// </summary>
        /// <param name="success">Значение результата операции.</param>
        /// <param name="messageResult">Сообщение результата операции.</param>
        public ServiceOperationResult(bool success, string messageResult)
        {
            Success = success;
            MessageResult = messageResult;
        }
    }
}
