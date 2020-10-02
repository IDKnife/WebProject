namespace CourseWork.Models
{
    /// <summary>
    /// Представляет модель величины.
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Значение величины.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ValueObject"/> с заданным значением.
        /// </summary>
        /// <param name="value">Значение величины.</param>
        public ValueObject(object value)
        {
            Value = value;
        }
    }
}
