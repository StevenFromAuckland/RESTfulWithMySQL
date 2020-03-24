using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HillLabTest.Models
{
    public interface IProductContextWrapper
    {
        ICategoryAction Category { get; }
        IProductAction Product { get; }
        Task<int> Save();
    }
}
