using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.OrderLineDTOs;

namespace E_commerce_Application.DTOs.ShopOrderDTOs
{
    public class ShopOrderDetailsDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string? UserFullName { get; set; }

        public int OrderStatusId { get; set; }
        public string? StatusName { get; set; }

        public decimal OrderTotalCost { get; set; }
        public DateTime OrderDate { get; set; }

        public int PaymentMethodId { get; set; }
        public string? PaymentMethodName { get; set; }

        public int ShippingMethodId { get; set; }
        public string? ShippingMethodName { get; set; }

        public int ShippingAddressId { get; set; }
        public string? ShippingAddressSummary { get; set; }

        public IEnumerable<OrderLineDto> Lines { get; set; } = new List<OrderLineDto>();
    }

}
