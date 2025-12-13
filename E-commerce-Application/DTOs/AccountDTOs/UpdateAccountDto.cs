using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.Dtos.AccountDTOs
{
    public class UpdateAccountDto
    {
        public  short? UserRole { get; set; }
        public  string? Email { get; set; }
        public  string? Phone { get; set; }
    }

}
