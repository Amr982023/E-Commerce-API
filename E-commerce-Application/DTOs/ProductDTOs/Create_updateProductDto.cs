using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductItemDTOs;
using E_commerce_Core.Models;

namespace E_commerce_Application.DTOs.ProductDTOs
{
    public class Create_updateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }

        public int CategoryId { get; set; }

        public ICollection<ProductItemDto> ProductItems { get; set; }
    }

}
