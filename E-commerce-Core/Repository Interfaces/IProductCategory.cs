using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IProductCategory : IGenericRepository<ProductCategory>
    {
        Task<IEnumerable<ProductCategory>> GetParentCategoriesAsync();
        Task<IEnumerable<ProductCategory>> GetChildrenAsync(int parentId);
        Task<bool> HasChildrenAsync(int categoryId);
        Task<bool> HasProductsAsync(int categoryId);
        Task<IEnumerable<ProductCategory>> SearchAsync(string term);

    }
}
