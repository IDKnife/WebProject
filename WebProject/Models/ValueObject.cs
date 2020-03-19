using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public abstract class ValueObject
    {
        public object Value { get; set; }

        public ValueObject(object value)
        {
            Value = value;
        }
    }
}
