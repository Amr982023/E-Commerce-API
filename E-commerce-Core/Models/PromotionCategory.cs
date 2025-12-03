using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class PromotionCategory
    {
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }

        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; }
    }

}
