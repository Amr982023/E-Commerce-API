using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.AddressDTOS;
using E_commerce_Application.DTOs.PaymentMethodDTOs;
using E_commerce_Application.DTOs.ShoppingCartDTOs;
using E_commerce_Application.DTOs.UserDTOs;

namespace E_commerce_Application.DTOs.AccountDTOs
{
    public class AccountWithDetailsDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public short UserRole { get; set; }

        // User Info
        public UserDto User { get; set; } = null!;

        // Addresses
        public List<AddressDto> Addresses { get; set; } = new();

        // Payment Methods
        public List<PaymentMethodDto> PaymentMethods { get; set; } = new();

        // Shopping Cart
        public CartDto ShoppingCart { get; set; } = null!;
    }
}
