using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Navigation
        public ICollection<Account> Accounts { get; set; }
        public ICollection<ShopOrder> Orders { get; set; }
        public ICollection<UserReview> Reviews { get; set; }
    }

}
