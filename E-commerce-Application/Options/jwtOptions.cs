using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.Options
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int Lifetime { get; set; }
        public string SigningKey { get; set; } = null!;
    }
}
