using HillLabTestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HillLabTest.Models
{
    public interface ICategoryAction : IEntityAction<Category>
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);

    }

}
