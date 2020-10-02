using CourseWork.Models;

namespace CourseWork.AdditionalClasses.Interfaces
{
    /// <summary>
    /// Интерфейс сериализуемого объекта.
    /// </summary>
    public interface ICanBeSerialised
    {
        /// <summary>
        /// Преобразовать в сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        Entity ToEntity();

        /// <summary>
        /// Преобразовать в новую сущность.
        /// </summary>
        /// <returns>Новая сущность.</returns>
        Entity ToNewEntity();
    }
}
