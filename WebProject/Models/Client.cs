using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class Client : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Client(int id, string firstName, string lastName) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
