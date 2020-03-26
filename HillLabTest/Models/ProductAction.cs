using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HillLabTestEntities;
using Microsoft.EntityFrameworkCore;

namespace HillLabTest.Models
{
    public class ProductAction : EntityAction<Product>, IProductAction
    {
        public ProductAction(ProductContext productContext)
            : base(productContext)
        {
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            IEnumerable<Product> products = await GetAll();
            if (products != null)
                return products.OrderBy(x => x.ProductName);

            return null;
        }
        public async Task<Product> GetProductById(int productId)
        {
            var categories = await Find(x => x.ProductId == productId);
            if (categories != null)
                return categories.FirstOrDefault();

            return null;
        }
        public async Task CreateProduct(Product product)
        {
            await Create(product);
        }
        public void UpdateProduct(Product product)
        {
            Update(product);
        }
        public void DeleteProduct(Product product)
        {
            Delete(product);
        }

        public async Task<IEnumerable<Product>> GetProductsInCategory(int categoryId)
        {
            var products = await Find(x => x.CategoryId == categoryId);
            if (products != null)
                return products.ToList();

            return null;
        }
    }
}
