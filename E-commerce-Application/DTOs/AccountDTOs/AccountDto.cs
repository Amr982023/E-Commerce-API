using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.Dtos.AccountDTOs
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public short UserRole { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
    }

}
