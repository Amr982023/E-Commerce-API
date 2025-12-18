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
    public class VariationRepo : GenericRepository<Variation>, IVariation
    {
        public VariationRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddOptionAsync(int variationId, string optionValue)
        {
            await _context.VariationOptions.AddAsync(new VariationOption
            {
                VariationId = variationId,
                Value = optionValue
            });
        }

        public async Task<IEnumerable<VariationOption>> GetOptionsForProductVariationAsync(int productItemId, int variationId)
        {
            return await _context.VariationOptions
                .Where(vo => vo.VariationId == variationId &&
                             vo.Variation.Category.Products.Any(pcp => pcp.ProductItems.Any(pi => pi.Id == productItemId)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Variation>> GetVariationsForProductAsync(int productId)
        {
            return await _context.Variations
                .Where(v => v.Category.Products.Any(pcp => pcp.ProductItems.Any(pi => pi.Id == productId)))
                .Distinct()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<VariationWithOptionsDto>> GetVariationTreeForProductAsync(int productId)
        {
            return await _context.Variations
                .Where(v => v.Category.Products.Any(pcp => pcp.ProductItems.Any(pi => pi.Id == productId)))
                .Select(v => new VariationWithOptionsDto
                {
                    VariationId = v.Id,
                    VariationName = v.Name,
                    Options = v.Options.Select(o => new VariationOptionsDto
                    {
                        Id = o.Id,
                        Value = o.Value
                    }).ToList()
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Variation?> GetWithOptionsAsync(int variationId)
        {
            return await _context.Variations
                .Include(v => v.Options)
                .AsNoTracking()
                .SingleOrDefaultAsync(v => v.Id == variationId);
        }

        public Task<bool> IsVariationUsedAsync(int variationId)
        {
            return _context.Variations
                .Where(v => v.Id == variationId)
                .AnyAsync(v => v.Category.Products.Any(p => p.ProductItems.Any()));
        }

        public async Task RemoveOptionAsync(int variationOptionId)
        {
            await _context.VariationOptions
                .Where(vo => vo.Id == variationOptionId)
                .ExecuteDeleteAsync();
        }

        public async Task RenameVariationAsync(int variationId, string newName)
        {
            await _context.Variations
                .Where(v => v.Id == variationId)
                .ExecuteUpdateAsync(v => v.SetProperty(vr => vr.Name, newName));
        }
    }
}
