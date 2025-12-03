using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductItemId { get; set; }
        public int Qty { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
        public ProductItem ProductItem { get; set; }
    }

}
