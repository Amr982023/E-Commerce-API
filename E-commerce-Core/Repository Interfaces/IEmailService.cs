using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Repository_Interfaces
{
    public interface IEmailService
    {
        Task SendMessageAsync(string toEmail, string Message);
    }

}
