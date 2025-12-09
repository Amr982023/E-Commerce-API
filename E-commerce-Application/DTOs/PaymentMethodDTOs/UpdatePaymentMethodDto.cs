using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.DTOs.PaymentMethodDTOs
{
    public class UpdatePaymentMethodDto
    {
        public int? PaymentTypeId { get; set; }
        public string? Provider { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }


}
