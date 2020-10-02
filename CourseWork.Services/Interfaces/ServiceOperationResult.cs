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
        public bool Result { get; }

        /// <summary>
        /// Сообщение результата операции.
        /// </summary>
        public string MessageResult { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ServiceOperationResult"/> с заданными значениями.
        /// </summary>
        /// <param name="result">Значение результата операции.</param>
        /// <param name="messageResult">Сообщение результата операции.</param>
        public ServiceOperationResult(bool result, string messageResult)
        {
            Result = result;
            MessageResult = messageResult;
        }
    }
}
