using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.DTOs.UserDTOs
{
    public class UserDetailsDto : UserDto
    {
        public int OrdersCount { get; set; }
        public int ReviewsCount { get; set; }

        public int? LastOrderId { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public decimal? LastOrderTotalCost { get; set; }
    }

}
