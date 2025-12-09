using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.DTOs.PromotionDTOs
{
    public class PromotionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal DiscountRate { get; set; }   // 0.15 = 15%
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
