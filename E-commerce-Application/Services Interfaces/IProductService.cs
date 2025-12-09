using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductDTOs;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IProductService
    {
        Task<ProductDetailsDto?> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> SearchAsync(string term, int limit = 20);
        Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<ProductDto>> GetBestSellingAsync(int limit = 10);
        Task<bool> ExistsAsync(int productId);

        Task<ProductDetailsDto?> GetProductWithDetailsAsync(int id);
    }

}
