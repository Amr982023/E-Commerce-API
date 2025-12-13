using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTOS
{

    public class VariationOptionDto
    {
        public int Id { get; set; }
        
        public required string Value { get; set; } // Red, Blue...
    }
}
