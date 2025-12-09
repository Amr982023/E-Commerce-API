using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductItemDTOs;
using E_commerce_Core.DTOS;
using E_commerce_Core.Models;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IProductItemService
    {
        Task<ProductItemDetailsDto?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<ProductItemDto>> GetByProductIdAsync(int productId);
        Task<IEnumerable<ProductItemDto>?> GetByOptionsAsync(int productId, IEnumerable<int> optionIds);

        Task<bool> IsInStockAsync(int productItemId, int qty);
        Task<bool> DecreaseStockAsync(int productItemId, int qty);
        Task<bool> IncreaseStockAsync(int productItemId, int qty);

        Task<decimal> GetCurrentPriceAsync(int productItemId);
        Task<IEnumerable<string>> GetImagesAsync(int productItemId);

        Task<bool> BelongsToProductAsync(int productItemId, int productId);

        Task<IEnumerable<VariationOptionDto>> GetAvailableColorsAsync(int productId);

        Task<ProductItemStockDto> GetStockSummaryAsync(int productId);

        Task<bool> AddProductItemsAsync(IEnumerable<CreateProductItemDto> items);
    }

}
