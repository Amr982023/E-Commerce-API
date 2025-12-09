using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IProductConfiguration : IGenericRepository<ProductConfiguration>
    {
        Task<IEnumerable<ProductConfiguration>> GetByProductItemAsync(int productItemId);

        Task<IEnumerable<VariationOption>> GetOptionsForProductAsync(int productId);

        Task<bool> ExistsAsync(int productItemId, int variationOptionId);

        Task<IEnumerable<ProductItem>> GetProductItemsByOptionAsync(int variationOptionId);

        Task<ProductItem?> GetProductItemByOptionsAsync(int productId, IEnumerable<int> optionIds);

        Task RemoveConfigurationsForProductItemAsync(int productItemId);

        Task AddConfigurationsAsync(int productItemId, IEnumerable<int> optionIds);

    }
}
