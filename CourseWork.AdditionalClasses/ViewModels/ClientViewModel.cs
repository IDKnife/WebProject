using System.Collections.Generic;
using System.Linq;
using CourseWork.Models;
using CourseWork.AdditionalClasses.Interfaces;

namespace CourseWork.AdditionalClasses.ViewModels
{
    /// <summary>
    /// Представляет модель представления клиента.
    /// </summary>
    public class ClientViewModel : IViewModel, ICanBeSerialised
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя клиента.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия клиента.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество клиента.
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Уровень доступа клиента.
        /// </summary>
        public AccessLevel Access { get; set; }

        /// <summary>
        /// Номер телефона клиента.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Электронная почта клиента.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Список заказов клиента.
        /// </summary>
        public List<OrderViewModel> Orders { get; set; }

        /// <summary>
        /// Пароль клиента.
        /// </summary>
        public string Password { get; set; }

        /// <inheritdoc cref="ICanBeSerialised.ToEntity"/>
        public Entity ToEntity()
        {
            var orders = Orders.Any()
                ? Orders
                    .Select(o => o.ToEntity() as Order)
                    .ToList()
                : new List<Order>();
            return new Client(Id, FirstName, LastName, SecondName, PhoneNumber, Email, Password, orders, Access);
        }

        /// <inheritdoc cref="ICanBeSerialised.ToNewEntity"/>
        public Entity ToNewEntity()
            => new Client(FirstName, LastName, SecondName, PhoneNumber, Email, Password, Access);

    }
}
