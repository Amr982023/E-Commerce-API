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
    public class VariationOptionRepo : GenericRepository<VariationOption>, IVariationOption
    {
        public VariationOptionRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<VariationOptionDto>> GetOptionsDtoForVariationAsync(int variationId)
        {
            return await _context.VariationOptions
                .Where(vo => vo.VariationId == variationId)
                .Select(vo => new VariationOptionDto
                {
                    Id = vo.Id,
                    Value = vo.Value
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<VariationOption>> GetOptionsForProductAsync(int productId, int variationId)
        {
            return await _context.VariationOptions
                .Where(vo => vo.VariationId == variationId &&
                             vo.Variation.Category.Products.Any(p => p.Id == productId))
                .ToListAsync();

        }

        public async Task<IEnumerable<VariationOption>> GetOptionsForProductItemAsync(int productItemId)
        {
            return await _context.VariationOptions
                .Where(pc => pc.Variation.Category.Products.Any(p => p.ProductItems.Any(pi => pi.Id == productItemId)))
                .ToListAsync();
        }

        public async Task<IEnumerable<VariationOption>> GetOptionsForVariationAsync(int variationId)
        {
            return await _context.VariationOptions
                .Where(vo => vo.VariationId == variationId)
                .ToListAsync();
        }

        public Task<bool> IsOptionUsedAsync(int optionId)
        {
            return _context.ProductConfigurations
                .AnyAsync(pc => pc.VariationOptionId == optionId);
        }

        public Task<bool> OptionExistsAsync(int variationId, string value)
        {
            return _context.VariationOptions
                .AnyAsync(vo => vo.VariationId == variationId && vo.Value == value);
        }
    }
}
