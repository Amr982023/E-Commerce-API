using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.DTOs.PromotionDTOs
{
    public class ApplyPromotionResultDto
    {
        public decimal OriginalPrice { get; set; }
        public decimal DiscountRate { get; set; }   // 0.15 = 15%
        public decimal DiscountAmount { get; set; }
        public decimal FinalPrice { get; set; }
    }

}
