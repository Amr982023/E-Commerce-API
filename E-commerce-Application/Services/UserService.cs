using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.UserDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Models;
using Mapster;

namespace E_commerce_Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Get user with details (counts + last order)
        public async Task<UserDetailsDto?> GetUserWithDetailsAsync(int userId)
        {
            var user = await _uow.Users.GetUserWithDetailsAsync(userId);
            if (user == null)
                return null;

            var dto = new UserDetailsDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = !string.IsNullOrWhiteSpace(user.FullName)
                    ? user.FullName
                    : $"{user.FirstName} {user.LastName}",
                Phone = user.Phone,
                Email = user.Email
            };

            // Orders / Reviews count
            dto.OrdersCount = await _uow.Users.GetOrdersCountAsync(userId);
            dto.ReviewsCount = await _uow.Users.GetReviewsCountAsync(userId);

            // Last order info
            var lastOrder = await _uow.Users.GetLastOrderForUserAsync(userId);
            if (lastOrder != null)
            {
                dto.LastOrderId = lastOrder.Id;
                dto.LastOrderDate = lastOrder.OrderDate;
                dto.LastOrderTotalCost = lastOrder.OrderTotalCost;
            }

            return dto;
        }

        // Get by email
        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var user = await _uow.Users.GetByEmailAsync(email);
            return user == null ? null : user.Adapt<UserDto>();
        }

        // Get by phone
        public async Task<UserDto?> GetByPhoneAsync(string phone)
        {
            var user = await _uow.Users.GetByPhoneAsync(phone);
            return user == null ? null : user.Adapt<UserDto>();
        }

        // Get by accountId
        public async Task<UserDto?> GetByAccountIdAsync(int accountId)
        {
            var user = await _uow.Users.GetByAccountIdAsync(accountId);
            return user == null ? null : user.Adapt<UserDto>();
        }

        // Search
        public async Task<IEnumerable<UserDto>> SearchUsersAsync(string? name, string? email, string? phone)
        {
            var users = await _uow.Users.SearchUsersAsync(name, email, phone);
            return users.Adapt<IEnumerable<UserDto>>();
        }

    }

}
