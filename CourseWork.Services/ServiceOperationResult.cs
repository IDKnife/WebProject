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
        public bool IsSuccess { get; }

        /// <summary>
        /// Сообщение результата операции.
        /// </summary>
        public string MessageResult { get; }

        /// <summary>
        /// Вернуть успешный результат операции.
        /// </summary>
        /// <returns>Успешный результат операции.</returns>
        public static ServiceOperationResult Success()
            => new ServiceOperationResult(true);

        /// <summary>
        /// Вернуть неудавшийся результат операции.
        /// </summary>
        /// <param name="message">Сообщение, раскрывающее причину неудачи.</param>
        /// <returns>Неудавшийся результат операции.</returns>
        public static ServiceOperationResult Fail(string message)
            => new ServiceOperationResult(false, message);

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ServiceOperationResult"/> с заданными значениями.
        /// </summary>
        /// <param name="success">Значение результата операции.</param>
        /// <param name="messageResult">Сообщение результата операции.</param>
        private ServiceOperationResult(bool success, string messageResult = "")
        {
            IsSuccess = success;
            MessageResult = messageResult;
        }
    }
}
