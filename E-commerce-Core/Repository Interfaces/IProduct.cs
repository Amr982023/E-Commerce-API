using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IProduct : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> SearchProductsAsync(string term, int limit = 20);

        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

        Task<IEnumerable<Product>> GetBestSellingProductsAsync(int limit = 10);

        Task<bool> ProductExistsAsync(int productId);


        public Task<Product> GetProductWithDetails(int id);
    }
}
