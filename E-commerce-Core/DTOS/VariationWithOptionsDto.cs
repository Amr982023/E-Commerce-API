using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTOS
{
    
        public class VariationWithOptionsDto
        {
            public int VariationId { get; set; }
            public required string VariationName { get; set; } // Color
            public List<VariationOptionsDto> Options { get; set; } = new();
        }
    
}
