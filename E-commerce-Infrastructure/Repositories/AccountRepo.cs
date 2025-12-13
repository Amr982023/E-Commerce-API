using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;
using E_commerce_Infrastructure.Repositories.Generic;
using E_commerce_Core.Interfaces;
using E_commerce_Core.Security;
using Microsoft.EntityFrameworkCore;
using E_commerce_Infrastructure.Services;


namespace E_commerce_Infrastructure.Repositories
{
    public class AccountRepo : GenericRepository<Account>, IAccount
    {
       
        public AccountRepo(ApplicationDbContext context) : base(context)
        {  
        }
        public override async Task<Account> AddAsync(Account entity)
        {
            await _context.Accounts.AddAsync(entity);
            clsSendMails.SendMessage(entity.User.Email, $"Welcome to E-Commerce Platform \nHello {entity.User.FullName},\n\nThank you for registering an account with us. We're excited to have you on board!\n\nBest regards,\nE-Commerce Team");
            return entity;
        }

        public async Task<Account> AuthenticateAsync(string username, string password)
        {
            // 1) Get user by username
            var account = await _context.Accounts
                .SingleOrDefaultAsync(a => a.UserName == username);

            if (account == null)
                return null;

            // 2) Verify password (HASH + SALT)
            bool isValid = PasswordHasher.Verify(password, account.Password);

            if (!isValid)
                return null;

            // 3) Success
            clsSendMails.SendMessage(account.User.Email, $"Login Alert \nHello {account.User.FullName},\n\nWe noticed a login to your account. If this was you, no further action is needed. If you did not log in, please reset your password immediately.\n\nBest regards,\nE-Commerce Team");
            return account;
        }

        public async Task<Account> GetAccountByUsernameAsync(string username)
        {
            return await _context.Accounts.SingleOrDefaultAsync(a => a.UserName == username);
        }

        public async Task<Account> GetAccountWithDetailsAsync(int id)
        {
            return await _context.Accounts
                .Include(a => a.User)
                .Include(a => a.PaymentMethods)
                .Include(a => a.ShoppingCart)
                .SingleOrDefaultAsync(a => a.Id == id);
        }

    }
}
