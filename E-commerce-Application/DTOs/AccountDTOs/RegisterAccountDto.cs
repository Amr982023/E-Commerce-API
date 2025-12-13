using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Application.Dtos.AccountDTOs
{
    public class RegisterAccountDto
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public short UserRole { get; set; }

        // User info
        [Required]
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [Phone]
        public required string Phone { get; set; }
        [EmailAddress]
        public required string Email { get; set; }


        // Address info
        public required string UnitNumber { get; set; }
        public required string Street { get; set; }
        public required string Region { get; set; }
        public required string City { get; set; }
        public required string PostalCode { get; set; }
        public bool IsAddressDefault { get; set; }

        public int CountryId { get; set; }

        // Payment Method info
        public int PaymentTypeId { get; set; }
        public int AccountId { get; set; }
        public required string AccountNumber { get; set; }
        public required string Provider { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsPaymentMethodDefault { get; set; }

 
    }

}
