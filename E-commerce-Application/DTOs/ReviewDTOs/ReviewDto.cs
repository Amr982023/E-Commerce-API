using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.DTOs.ReviewDTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string? UserName { get; set; }

        public int ProductItemId { get; set; }
        public string? SKU { get; set; }
        public string? ProductName { get; set; }

        public int RatingValue { get; set; }
        public string? Comment { get; set; }
    }

}
