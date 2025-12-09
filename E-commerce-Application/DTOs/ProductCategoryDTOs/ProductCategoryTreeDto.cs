using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.DTOs.ProductCategoryDTOs
{
    public class ProductCategoryTreeDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }

        public List<ProductCategoryTreeDto> Children { get; set; } = new();
    }

}
