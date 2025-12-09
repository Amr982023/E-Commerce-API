using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.Dtos.AddressDTOS
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string UnitNumber { get; set; }
        public string Street { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public bool IsDefault { get; set; }

        public int CountryId { get; set; }

        public int AccountId { get; set; }
    }

}
