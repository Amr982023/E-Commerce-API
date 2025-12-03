using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; }
    }

}
