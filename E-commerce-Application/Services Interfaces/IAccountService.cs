using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.AccountDTOs;
using E_commerce_Application.DTOs.AccountDTOs;
using E_commerce_Application.DTOs.AuthDTOs;
using E_commerce_Core.Models;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IAccountService
    {
        // Create
        Task<AccountDto> RegisterAsync(RegisterAccountDto model);

        // Read
        Task<AuthResponseDto> AuthenticateAsync(string username, string password);
        Task<AccountDto?> GetByIdAsync(int accountId);
        Task<AccountDto?> GetByUsernameAsync(string username);
        Task<AccountWithDetailsDto> GetWithDetailsAsync(int accountId);
        Task<IEnumerable<AccountDto>> GetAllAsync();
        Task<IEnumerable<AccountDto>> SearchAsync(string? username, string? email);

        // Update
        Task<bool> ChangePasswordAsync(int accountId, string oldPassword, string newPassword);
        Task<bool> UpdateAccountAsync(int accountId, UpdateAccountDto model);

        // Delete
        Task<bool> DeleteAccountAsync(int accountId);

        // Validation
        Task<bool> UsernameExistsAsync(string username);
    }

}
