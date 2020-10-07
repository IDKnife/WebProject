using System.Collections.Generic;

namespace CourseWork.Models
{
    /// <summary>
    /// Представляет модель клиента.
    /// </summary>
    public class Client : Entity
    {
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
        public List<Order> Orders { get; set; }

        /// <summary>
        /// Пароль клиента.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        ///  Инициализирует новый экземпляр класса <see cref="Client"/> с заданными значениями, кроме списка заказов.
        /// </summary>
        /// <param name="firstName">Имя клиента.</param>
        /// <param name="lastName">Фамилия клиента.</param>
        /// <param name="secondName">Отчество клиента.</param>
        /// <param name="phoneNumber">Номер телефона клиента.</param>
        /// <param name="email">Электронная почта клиента.</param>
        /// <param name="password">Пароль клиента.</param>
        /// <param name="access">Уровень доступа клиента.</param>
        public Client(
            string firstName,
            string lastName,
            string secondName,
            string phoneNumber,
            string email,
            string password,
            AccessLevel access)
        {
            FirstName = firstName;
            LastName = lastName;
            SecondName = secondName;
            Access = access;
            Orders = new List<Order>();
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Client"/> с заданными значениями.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="firstName">Имя клиента.</param>
        /// <param name="lastName">Фамилия клиента.</param>
        /// <param name="secondName">Отчество клиента.</param>
        /// <param name="phoneNumber">Номер телефона клиента.</param>
        /// <param name="email">Электронная почта клиента.</param>
        /// <param name="password">Пароль клиента.</param>
        /// <param name="orders">Список заказов клиента.</param>
        /// <param name="access">Уровень доступа клиента.</param>
        public Client(
            string id,
            string firstName,
            string lastName,
            string secondName,
            string phoneNumber,
            string email,
            string password,
            List<Order> orders,
            AccessLevel access) : base(id) 
        {
            FirstName = firstName;
            LastName = lastName;
            SecondName = secondName;
            Access = access;
            Orders = orders;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
        }
    }
}
