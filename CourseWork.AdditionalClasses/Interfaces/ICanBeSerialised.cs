using CourseWork.Models;

namespace CourseWork.AdditionalClasses.Interfaces
{
    public interface ICanBeSerialised
    {
        Entity ToEntity();
        Entity ToNewEntity();
    }
}
