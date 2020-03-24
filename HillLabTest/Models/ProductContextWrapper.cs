using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HillLabTest.Models
{
    public class ProductContextWrapper : IProductContextWrapper
    {
        private ProductContext productContext;
        private ICategoryAction category;
        private IProductAction product;

        public ICategoryAction Category
        {
            get
            {
                if (category == null)
                {
                    category = new CategoryAction(productContext);
                }

                return category;
            }
        }

        public IProductAction Product
        {
            get
            {
                if (product == null)
                {
                    product = new ProductAction(productContext);
                }

                return product;
            }
        }


        public ProductContextWrapper(ProductContext productContext)
        {
            this.productContext = productContext;
        }

        public async Task<int> Save()
        {
            return await productContext.SaveChangesAsync();
        }
    }
}
