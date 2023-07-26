using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Identity;

namespace Ecommerce.Domain.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<CarrinhoItem> Items { get; set; }
    }
}