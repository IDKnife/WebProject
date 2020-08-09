using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork.Models
{
    public class Client : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName { get; set; }
        public AccessLevel Access { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Order> Orders { get; set; }
        public string Password { get; private set; }

        public void ChangePassword(string newPassword, string oldPassword)
        {
            if (oldPassword == Password)
                Password = newPassword;
        }

        public Client(int id, string firstName, string lastName, string secondName, string phoneNumber, string email, string password, AccessLevel access) : base(id)
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
        public Client(int id, string firstName, string lastName, string secondName, string phoneNumber, string email, string password, List<Order> orders, AccessLevel access) : base(id)
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
