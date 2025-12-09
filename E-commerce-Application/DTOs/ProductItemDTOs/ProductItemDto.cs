using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.DTOs.ProductItemDTOs
{
    public class ProductItemDto
    {
        public int Id { get; set; }
        public string SKU { get; set; }

        public decimal Price { get; set; }
        public int QtyInStock { get; set; }

        public string ProductImage { get; set; }
    }

}
