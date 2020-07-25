using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Models;
using CourseWork.Repositories.Interfaces;
using CourseWork.Services.Interfaces;

namespace CourseWork.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<Product>> GetProducts()
        {
            try
            {
                return await _repository.GetEntities();
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }

        public async Task<IList<Product>> GetProducts(string name, string category)
        {
            try
            {
                return await _repository.GetEntities(name, category);
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }

        public async Task AddProduct(Product entity)
        {
            try
            {
                await _repository.AddEntity(entity);
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                await _repository.DeleteEntity(id);
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }

        public async Task UpdateProduct(Product entity)
        {
            try
            {
                await _repository.UpdateEntity(entity);
            }
            catch (Exception e)
            {
                //ToDo: логирование
                throw;
            }
        }

        public async Task<Product> GetProduct(int id)
        {
            try
            {
                return await _repository.GetEntity(id);
            }
            catch (Exception)
            {
                //ToDo: логирование
                throw;
            }
        }
    }
}
