using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class Address
    {
        public int Id { get; set; }
        public required string UnitNumber { get; set; }
        public required string Street { get; set; }
        public required string Region { get; set; }
        public required string City { get; set; }
        public required string PostalCode { get; set; }
        public bool IsDefault { get; set; }

        public int CountryId { get; set; }
        public  Country? Country { get; set; }

        public int AccountId { get; set; }
        public Account? Account { get; set; }


        public ICollection<ShopOrder>? Orders { get; set; }
    }

}
