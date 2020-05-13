using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.Services.Interfaces
{
    public interface IProductService
    {
        Task<IList<Product>> GetProducts();
        Task<IList<Product>> GetProducts(string name, string category);
        Task AddProduct(Product entity);
        Task DeleteProduct(Product entity);
        Task UpdateProduct(Product entity);
        Task<Product> GetProduct(int id);
    }
}
