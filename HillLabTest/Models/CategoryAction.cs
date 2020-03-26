using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HillLabTestEntities;
using Microsoft.EntityFrameworkCore;

namespace HillLabTest.Models
{
    public class CategoryAction : EntityAction<Category>, ICategoryAction
    {
        public CategoryAction(ProductContext productContext)
            : base(productContext)
        {
        }
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            IEnumerable<Category> categories = await GetAll();
            if (categories != null)
                return categories.OrderBy(x => x.CategoryName);

            return null;
        }
        public async Task<Category> GetCategoryById(int categoryId)
        {
            var categories = await Find(x => x.CategoryId == categoryId);
            if (categories != null)
                return categories.FirstOrDefault();

            return null;
        }
        public async Task CreateCategory(Category category)
        {
            await Create(category);
        }
        public void UpdateCategory(Category category)
        {
            Update(category);
        }
        public void DeleteCategory(Category category)
        {
            Delete( category);
        }

    }
}