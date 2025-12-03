using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public string Email { get; set; }

        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }

        public ICollection<ProductItem> ProductItems { get; set; }
    }

}
