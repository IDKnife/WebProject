using CourseWork.Models;

namespace CourseWork.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория продуктов.
    /// </summary>
    public interface IProductRepository : IRepository<Product>, IProductRepositoryFilters
    {
    }
}
