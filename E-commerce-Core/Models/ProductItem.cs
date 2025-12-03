using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class ProductItem
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public int QtyInStock { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public ICollection<ProductConfiguration> Configurations { get; set; }
        public ICollection<ShoppingCartItem> CartItems { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }

    }

}
