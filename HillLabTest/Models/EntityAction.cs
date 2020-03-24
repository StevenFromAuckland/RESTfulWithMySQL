using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HillLabTest.Models
{
    public abstract class EntityAction<T> : IEntityAction<T> where T : class
    {
        protected ProductContext ProductContext { get; set; }

        public EntityAction(ProductContext productContext)
        {
            ProductContext = productContext;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.ProductContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await this.ProductContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task Create(T entity)
        {
            await this.ProductContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            this.ProductContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.ProductContext.Set<T>().Remove(entity);
        }
    }

}
