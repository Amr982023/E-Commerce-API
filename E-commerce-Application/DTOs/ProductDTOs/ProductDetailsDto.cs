using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductItemDTOs;

namespace E_commerce_Application.DTOs.ProductDTOs
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public IEnumerable<ProductItemDto> ProductItems { get; set; }
    }

}
