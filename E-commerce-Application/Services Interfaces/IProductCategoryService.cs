using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductCategoryDTOs;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<CategoryDto>> GetParentsAsync();
        Task<IEnumerable<CategoryDto>> GetChildrenAsync(int parentId);

        Task<bool> HasChildrenAsync(int categoryId);
        Task<bool> HasProductsAsync(int categoryId);

        Task<IEnumerable<CategoryDto>> SearchAsync(string term);

        // Tree (optional but useful)
        Task<IEnumerable<ProductCategoryTreeDto>> GetCategoryTreeAsync();
    }

}
