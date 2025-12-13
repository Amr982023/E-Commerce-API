using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductItemDTOs;
using E_commerce_Application.DTOs.ReviewDTOs;
using E_commerce_Application.DTOs.ShopOrderDTOs;

namespace E_commerce_Application.DTOs.OrderLineDTOs
{
    public class OrderLineWithDetailsDto
    {
        public int Id { get; set; }

        public int ProductItemId { get; set; }
        public required ProductItemDto ProductItem { get; set; }

        public int ShopOrderId { get; set; }
        public required ShopOrderSummaryDto ShopOrder { get; set; }

        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal LineTotal { get; set; }

        public List<ReviewDto>? UserReviews { get; set; }
    }

}
