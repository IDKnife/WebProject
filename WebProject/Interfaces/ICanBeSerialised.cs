using CourseWork.Models;

namespace CourseWork.WebApi.Interfaces
{
    public interface ICanBeSerialised
    {
        Entity ToEntity();
        Entity ToNewEntity();
    }
}
