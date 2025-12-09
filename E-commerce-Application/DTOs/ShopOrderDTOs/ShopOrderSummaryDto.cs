using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.DTOs.ShopOrderDTOs
{
    public class ShopOrderSummaryDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int OrderStatusId { get; set; }
        public string? StatusName { get; set; }

        public decimal OrderTotalCost { get; set; }
        public DateTime OrderDate { get; set; }
    }

}
