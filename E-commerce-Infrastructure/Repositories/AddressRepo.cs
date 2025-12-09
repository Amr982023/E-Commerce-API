using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Interfaces;
using E_commerce_Core.Models;
using E_commerce_Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Infrastructure.Repositories
{
    public class AddressRepo : GenericRepository<Address>, IAddress
    {
        public AddressRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Address>> GetAddressesByAccountIdAsync(int accountId)
        {
            return await _context.Addresses.Where(a => a.AccountId == accountId).ToListAsync();               
        }

        public async Task<Address> GetAddressWithDetailsAsync(int id)
        {
            return await _context.Addresses
                .Include(a => a.Account)
                .Include(a => a.Country)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Address> GetDefaultAddressAsync(int accountId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.AccountId == accountId && a.IsDefault);
        }

        public async Task SetDefaultAddressAsync(int accountId, int addressId)
        {
            // Remove defaults
            await _context.Addresses
                .Where(a => a.AccountId == accountId && a.IsDefault)
                .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDefault, false));

            // Set new default
            await _context.Addresses
                .Where(a => a.Id == addressId && a.AccountId == accountId)
                .ExecuteUpdateAsync(a => a.SetProperty(x => x.IsDefault, true));
        }

    }
}
