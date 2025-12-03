using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public int PaymentTypeId { get; set; }
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string Provider { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsDefault { get; set; }

        public PaymentType PaymentType { get; set; }
        public Account Account { get; set; }

        public ICollection<ShopOrder> ShopOrders { get; set; }
    }
}
