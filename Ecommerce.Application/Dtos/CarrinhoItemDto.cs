using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dtos
{
    public class CarrinhoItemDto
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int CarrinhoId { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public int PrecoTotal { get; set; }
    }
}