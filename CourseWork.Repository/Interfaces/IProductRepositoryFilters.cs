using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.Repositories.Interfaces
{
    public interface IProductRepositoryFilters
    {
        Task<IList<Product>> GetEntities(string name, string category);
    }
}
