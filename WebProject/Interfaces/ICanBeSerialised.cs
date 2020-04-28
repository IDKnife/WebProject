using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.WebApi.Interfaces
{
    public interface ICanBeSerialised
    {
        Entity ToEntity();
    }
}
