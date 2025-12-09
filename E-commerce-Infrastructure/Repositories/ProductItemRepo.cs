using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.DTOS;
using E_commerce_Core.Interfaces;
using E_commerce_Core.Models;
using E_commerce_Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Infrastructure.Repositories
{
    public class ProductItemRepo : GenericRepository<ProductItem>, IProductItem
    {
        public ProductItemRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddProductItemsAsync(IEnumerable<ProductItem> items)
        {
            await _context.ProductItems.AddRangeAsync(items);
        }

        public async Task<bool> BelongsToProductAsync(int productItemId, int productId)
        {
            return await _context.ProductItems
                .AnyAsync(pi => pi.Id == productItemId && pi.ProductId == productId);
        }

        public  async Task DecreaseStockAsync(int productItemId, int qty)
        {
            var item = await _context.ProductItems.FindAsync(productItemId);
            if (item != null)
            {
                item.QtyInStock -= qty;
                _context.ProductItems.Update(item);
            }     
        }

        public async Task<IEnumerable<VariationOption>> GetAvailableColorsAsync(int productId)
        {
            return await _context.ProductConfigurations
                .Where(pc => pc.ProductItem.ProductId == productId)
                .Where(pc => pc.VariationOption.Variation.Name == "Color")
                .Select(pc => pc.VariationOption)
                .Distinct()
                .ToListAsync();
        }

        public async Task<ProductItem> GetByIdWithDetailsAsync(int id)
        {
            return await _context.ProductItems
        .Include(pi => pi.Product)
        .Include(pi => pi.Configurations)
            .ThenInclude(pc => pc.VariationOption)
                .ThenInclude(vo => vo.Variation)
        .AsSplitQuery()  
        .FirstOrDefaultAsync(pi => pi.Id == id);
        }

        public async Task<IEnumerable<ProductItem>?> GetByOptionsAsync(int productId, IEnumerable<int> optionIds)
        {
            return await _context.ProductItems
                .Where(pi => pi.ProductId == productId)
                .Where(pi => pi.Configurations
                    .Select(pc => pc.VariationOptionId)
                    .OrderBy(id => id)
                    .SequenceEqual(optionIds.OrderBy(id => id)))
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductItem>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductItems
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();
        }

        public async Task<decimal> GetCurrentPriceAsync(int productItemId)
        {
            return await _context.ProductItems
                .Where(pi => pi.Id == productItemId)
                .Select(pi => pi.Price)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<string>> GetImagesAsync(int productItemId)
        {
            return await _context.ProductItems
                .Where(pi => pi.Id == productItemId)
                .Select(pi => pi.ProductImage)
                .ToListAsync();
        }

        public async Task<ProductItemStockDto> GetStockSummaryAsync(int productId)
        {
            var items = await _context.ProductItems
       .Where(pi => pi.ProductId == productId)
       .ToListAsync();

            if (!items.Any())
                return null;

            return new ProductItemStockDto
            {
                ProductId = productId,
                MaxPrice = items.Max(pi => pi.Price),
                MinPrice = items.Min(pi => pi.Price),
                TotalStock = items.Sum(pi => pi.QtyInStock)
            };   
        }

        public async Task IncreaseStockAsync(int productItemId, int qty)
        {
            var item = await _context.ProductItems.FindAsync(productItemId);
            if (item != null)
            {
                item.QtyInStock += qty;
                _context.ProductItems.Update(item);
            }
        }

        public async Task<bool> IsInStockAsync(int productItemId, int qty)
        {
            return await _context.ProductItems.AnyAsync(pi => pi.Id == productItemId && pi.QtyInStock >= qty);
        }

    }
}
