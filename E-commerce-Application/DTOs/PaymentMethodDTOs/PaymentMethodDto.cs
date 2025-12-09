using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.DTOs.PaymentMethodDTOs
{
    public class PaymentMethodDto
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }

        public string Provider { get; set; }

        public string MaskedAccountNumber { get; set; }

        public DateTime ExpiryDate { get; set; }
        public bool IsDefault { get; set; }
        public bool IsExpired { get; set; }
    }


}
