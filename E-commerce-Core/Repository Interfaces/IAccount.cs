using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IAccount : IGenericRepository<Account>
    {
        Task<Account> GetAccountByUsernameAsync(string username);

        Task<Account> GetAccountWithDetailsAsync(int id);

        Task<Account> AuthenticateUserAsync(string username, string password);
    }
}
