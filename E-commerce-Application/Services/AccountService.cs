using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.AccountDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Models;
using E_commerce_Core.Security;
using Mapster;

namespace E_commerce_Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;

        public AccountService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // REGISTER
        public async Task<AccountDto> RegisterAsync(RegisterAccountDto model)
        {
            // Check unique username,Phone, Email
            if (await UsernameExistsAsync(model.UserName))
                throw new Exception("Username already exists.");
            if (await _uow.Users.GetByPhoneAsync(model.Phone)!= null)
                throw new Exception("Phone already exists.");
            if (await _uow.Users.GetByEmailAsync(model.Email)!=null)
                throw new Exception("Email already exists.");

            // Create user
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Email = model.Email
            };
            await _uow.Users.AddAsync(user);
            await _uow.CompleteAsync();

            var hashedPassword = PasswordHasher.Hash(model.Password);

            // Create account
            var account = new Account
            {
                UserName = model.UserName,
                Password = hashedPassword,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                UserRole = model.UserRole
            };

            await _uow.Accounts.AddAsync(account);
            await _uow.CompleteAsync();

           
            return account.Adapt<AccountDto>();
        }

        // AUTHENTICATE (LOGIN)
        public async Task<AccountDto> AuthenticateAsync(string username, string password)
        {
            var account = await _uow.Accounts.GetAccountByUsernameAsync(username);
            if (account == null)
                return null;

            // verify the input password against stored hashed password
            if (!PasswordHasher.Verify(password, account.Password))
                return null;

            return account.Adapt<AccountDto>();
        }

        // GET METHODS
        public async Task<AccountDto> GetByIdAsync(int accountId)
        {
            var acc = await _uow.Accounts.GetByIdAsync(accountId);
            return acc == null ? null : acc.Adapt<AccountDto>();
        }

        public async Task<AccountDto> GetByUsernameAsync(string username)
        {
            var acc = await _uow.Accounts.GetAccountByUsernameAsync(username);
            return acc == null ? null : acc.Adapt<AccountDto>();
        }

        public async Task<AccountDto> GetWithDetailsAsync(int accountId)
        {
            var acc = await _uow.Accounts.GetAccountWithDetailsAsync(accountId);
            return acc == null ? null : acc.Adapt<AccountDto>();
        }

        public async Task<IEnumerable<AccountDto>> GetAllAsync()
        {
            var accounts = await _uow.Accounts.GetAllAsync();
            return accounts.Adapt<List<AccountDto>>();
        }

        public async Task<IEnumerable<AccountDto>> SearchAsync(string? username, string? email)
        {
            var results = await _uow.Accounts.FindAllAsync(
                acc =>
                    (string.IsNullOrEmpty(username) || acc.UserName.Contains(username)) &&
                    (string.IsNullOrEmpty(email) || acc.User.Email.Contains(email))
            );

            return results.Adapt<List<AccountDto>>();
        }

        // UPDATE ACCOUNT INFO
        public async Task<bool> UpdateAccountAsync(int accountId, UpdateAccountDto model)
        {
            var acc = await _uow.Accounts.GetByIdAsync(accountId);
            if (acc == null)
                return false;

            if (!string.IsNullOrEmpty(model.Email))
                acc.User.Email = model.Email;

            if (!string.IsNullOrEmpty(model.Phone))
                acc.User.Phone = model.Phone;

            if (model.UserRole.HasValue)
                acc.UserRole = model.UserRole.Value;

            _uow.Accounts.Update(acc);
            await _uow.CompleteAsync();
            return true;
        }

        // CHANGE PASSWORD   
        public async Task<bool> ChangePasswordAsync(int accountId, string oldPassword, string newPassword)
        {
            var account = await _uow.Accounts.GetByIdAsync(accountId);
            if (account == null)
                return false;

            if (!PasswordHasher.Verify(oldPassword, account.Password))
                return false;

            account.Password = PasswordHasher.Hash(newPassword);

            _uow.Accounts.Update(account);
            await _uow.CompleteAsync();

            return true;
        }

     
        // DELETE
        public async Task<bool> DeleteAccountAsync(int accountId)
        {
            var account = await _uow.Accounts.GetByIdAsync(accountId);
            if (account == null)
                return false;

            _uow.Accounts.Delete(account);
            await _uow.CompleteAsync();
            return true;
        }

        // VALIDATION
        public async Task<bool> UsernameExistsAsync(string username)
        {
            return (await _uow.Accounts.GetAccountByUsernameAsync(username)) != null;
        }
       
    }

}
