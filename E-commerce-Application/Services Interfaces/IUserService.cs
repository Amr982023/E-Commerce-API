using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.UserDTOs;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IUserService
    {
        Task<UserDetailsDto?> GetUserWithDetailsAsync(int userId);

        Task<UserDto?> GetByEmailAsync(string email);
        Task<UserDto?> GetByPhoneAsync(string phone);
        Task<UserDto?> GetByAccountIdAsync(int accountId);

        Task<IEnumerable<UserDto>> SearchUsersAsync(string? name, string? email, string? phone);
    }

}
