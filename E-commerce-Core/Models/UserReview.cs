using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class UserReview
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int OrderedProductId { get; set; }
        public OrderLine OrderLine { get; set; }

        public int RatingValue { get; set; }
        public string Comment { get; set; }
    }

}
