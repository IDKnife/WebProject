using CourseWork.Models;

namespace CourseWork.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>, IProductRepositoryFilters
    {
    }
}
