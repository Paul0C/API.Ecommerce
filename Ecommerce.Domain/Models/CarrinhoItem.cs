using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models
{
    public class CarrinhoItem
    {
        public int Id { get; set; }
        public int CarrinhoId { get; set; }
        public int ProdutoId { get; set; }
        public Carrinho Carrinho { get; set; }
        public Produto Produto { get; set; }
    }
}