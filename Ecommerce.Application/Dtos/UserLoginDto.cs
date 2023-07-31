using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dtos
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string PassWord { get; set; }
    }
}