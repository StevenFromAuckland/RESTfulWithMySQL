using HillLabTestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HillLabTest.Models
{
 
    public interface IProductAction : IEntityAction<Product>
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int productId);
        Task CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);

        Task<IEnumerable<Product>> GetProductsInCategory(int categoryId);
    }
 
}
