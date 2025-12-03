using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class ProductConfiguration
    {
        public int ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }

        public int VariationOptionId { get; set; }
        public VariationOption VariationOption { get; set; }
    }

}
