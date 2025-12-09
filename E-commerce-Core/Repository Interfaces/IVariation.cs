using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.DTOS;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IVariation:IGenericRepository<Variation>
    {
        Task<Variation?> GetWithOptionsAsync(int variationId);
        Task<IEnumerable<Variation>> GetVariationsForProductAsync(int productItemId);
        Task<IEnumerable<VariationOption>> GetOptionsForProductVariationAsync(int productId, int variationId);
        Task<bool> IsVariationUsedAsync(int variationId);
        Task RenameVariationAsync(int variationId, string newName);
        Task AddOptionAsync(int variationId, string optionValue);
        Task RemoveOptionAsync(int variationOptionId);
        Task<IEnumerable<VariationWithOptionsDto>> GetVariationTreeForProductAsync(int productId);
    }
}
