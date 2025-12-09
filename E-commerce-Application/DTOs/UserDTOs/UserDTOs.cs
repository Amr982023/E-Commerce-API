using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.DTOs.UserDTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
    }

}
