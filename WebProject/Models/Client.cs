using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class Client
    {
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Client(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
