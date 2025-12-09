using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IUser : IGenericRepository<User>
    {
        Task<User?> GetUserWithDetailsAsync(int userId);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByPhoneAsync(string phone);
        Task<User?> GetByAccountIdAsync(int accountId);
        Task<IEnumerable<User>> SearchUsersAsync(string? name, string? email, string? phone);
        Task<int> GetOrdersCountAsync(int userId);
        Task<int> GetReviewsCountAsync(int userId);
        Task<ShopOrder?> GetLastOrderForUserAsync(int userId);

    }
}
