using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.DTOs.ProductConfigurationDTOs
{
    public class ProductConfigurationDto
    {
        public int ProductItemId { get; set; }
        public int VariationOptionId { get; set; }
        public string? VariationName { get; set; }
        public string? OptionValue { get; set; }
    }

}
