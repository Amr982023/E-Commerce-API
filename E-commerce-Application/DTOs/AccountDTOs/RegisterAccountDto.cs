using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.Dtos.AccountDTOs
{
    public class RegisterAccountDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public short UserRole { get; set; }

        // User info
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

}
