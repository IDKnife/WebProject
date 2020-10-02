using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс фильтров поиска для репозитория продуктов.
    /// </summary>
    public interface IProductRepositoryFilters
    {
        /// <summary>
        /// Получить отфильтрованный по заданным критериям список продуктов.
        /// </summary>
        /// <param name="name">Наименование продукта.</param>
        /// <param name="category">Категория продукта.</param>
        /// <returns>Список продуктов.</returns>
        Task<IList<Product>> GetEntities(string name, string category);
    }
}
