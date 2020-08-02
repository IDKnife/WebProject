using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.Services.Interfaces
{
    public interface IProductService
    {
        Task<IList<Product>> GetProducts();
        Task<IList<Product>> GetProducts(string name, string category);
        Task<ServiceOperationResult> AddProduct(Product entity);
        Task<ServiceOperationResult> DeleteProduct(int id);
        Task<ServiceOperationResult> UpdateProduct(Product entity);
        Task<Product> GetProduct(int id);
    }
}
