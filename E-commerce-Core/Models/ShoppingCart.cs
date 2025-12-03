using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public ICollection<ShoppingCartItem> Items { get; set; }
    }

}
