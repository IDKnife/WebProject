using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.WebApi.Interfaces;

namespace CourseWork.WebApi.ViewModels
{
    public class ClientViewModel : IViewModel, ICanBeSerialised
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public int Id { get; set; }
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
        /// Электронный почтовый адрес клиента.
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
        /// Преобразовать модель в сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        public Entity ToEntity()
        {
            return new Client(Id, FirstName, LastName, SecondName, PhoneNumber, Email, Password);
        }
    }
}
