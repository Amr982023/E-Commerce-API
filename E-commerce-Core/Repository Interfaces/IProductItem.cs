using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.DTOS;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IProductItem : IGenericRepository<ProductItem>
    {
        Task<ProductItem> GetByIdWithDetailsAsync(int id);

        Task<IEnumerable<ProductItem>> GetByProductIdAsync(int productId);

        Task<IEnumerable<ProductItem>?> GetByOptionsAsync(int productId, IEnumerable<int> optionIds);

        Task<bool> IsInStockAsync(int productItemId, int qty);

        Task DecreaseStockAsync(int productItemId, int qty);

        Task IncreaseStockAsync(int productItemId, int qty);

        Task<decimal> GetCurrentPriceAsync(int productItemId);

        Task<IEnumerable<string>> GetImagesAsync(int productItemId);

        Task<bool> BelongsToProductAsync(int productItemId, int productId);

        Task<IEnumerable<VariationOption>> GetAvailableColorsAsync(int productId);

        Task<ProductItemStockDto> GetStockSummaryAsync(int productId);

        Task AddProductItemsAsync(IEnumerable<ProductItem> items);

    }
}
