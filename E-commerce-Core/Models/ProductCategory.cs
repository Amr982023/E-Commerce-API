using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }

        public ProductCategory Parent { get; set; }
        public ICollection<ProductCategory> Children { get; set; }


        public ICollection<Variation> Variations { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<PromotionCategory> PromotionCategories { get; set; }
    }

}
