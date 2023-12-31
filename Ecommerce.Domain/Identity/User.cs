using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}